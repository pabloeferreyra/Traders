using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Traders.Data;
using Traders.Models;
using Traders.Services;

namespace Traders.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FuturesReportController : ControllerBase
    {
        private readonly ILogger<FuturesReportController> _logger;
        private readonly IMailService _service;
        private readonly ApplicationDbContext _context;
        public FuturesReportController(ILogger<FuturesReportController> logger, IMailService service,
            ApplicationDbContext context)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> ExportReport()
        {
            var futures = _context.Futures.Where(f => f.StartDate.AddMonths(6) > DateTime.Today).ToArray();
            var clientIds = futures.Select(c => c.ClientId).Distinct();
            var clientEmails = _context.Clients.Where(c => clientIds.Contains(c.Id)).Select(e => e.Email).ToArray();
            DateTime date = new DateTime();
            var startMonth = new DateTime(date.Year, date.Month, 1);
            var finishMonth = startMonth.AddMonths(1).AddDays(-1);
            var movements = await _context.FuturesUpdates.Where(m => m.ModifDate >= startMonth && m.ModifDate <= finishMonth).OrderBy(m => m.ModifDate).ToListAsync();
            


            foreach (var f in futures)
            {
                f.Client = _context.Clients.Where(cl => cl.Id == f.ClientId).FirstOrDefault();
                f.FinishDate = f.StartDate.AddMonths(6);
                if (movements.Count > 0)
                {
                    decimal fuGain = 0;

                    foreach (var fu in movements)
                    {
                        fuGain += ((f.Capital * (fu.Gain / 100)) / (f.Participation.Percentage / 100));
                    }

                    f.FinalResult += fuGain;
                }
                else
                {
                    f.FinalResult = f.Capital;
                }
                var percentage = f.FinalResult / f.Capital * 100;
                var participation = _context.Participations.Where(p => p.Id == f.ParticipationId).Select(p => p.Percentage).FirstOrDefault();
                var emailTxt = "Hola!<br /> Este es un resumen mensual del estado de tu cuenta en Bitcoin Santa Fe, tomado al día de la fecha:" + DateTime.Today.ToShortDateString() + "<br />"
                + "Contrato Nro: " + f.ContractNumber + "<br />"
                + "Capital inicial: " + f.Capital.ToString() + "<br />"
                + "Rendimiento Mensual: " + percentage + "%<br />"
                + "Participacion: " + participation + "<br />"
                + "Capital acumulado en USDT: " + f.FinalResult + "<br />"
                + "Fecha de proximo retiro: " + f.FinishDate + "<br />"
                + "<br />"
                + "Este mensaje es automático, por favor no respondas. Por cualquier consulta comunicate con nosotros por los medios habituales. <br />"
                + "<p>* La información contenida en este correo es privada y confidencial.Si cree que lo ha recibido por error, por favor, notifíquelo al remitente.</p>";
;
                var MailRequest = new MailRequestViewModel()
                {
                    ToEmail = f.Client.Email,
                    Subject = "Reporte de contrato Nro: " + f.ContractNumber + " BTC Santa Fe",
                    Body = emailTxt

                };
                await _service.SendEmailAsync(MailRequest);
            }
            
            return NoContent();
        }

    }
}
