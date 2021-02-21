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
using static Traders.Settings.ClientsTypes;

namespace Traders.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FuturesReportController : ControllerBase
    {
        #region consts
        private const string br = "<br />";
        private const string startMessage = "Hola!"+ br + "Este es un resumen mensual del estado de tu cuenta en Bitcoin Santa Fe, tomado al día de la fecha:";
        private const string finishMessage = br
                    + "Este mensaje es automático, por favor no respondas. Por cualquier consulta comunicate con nosotros por los medios habituales."+ br 
                    + "<p>* La información contenida en este correo es privada y confidencial.Si cree que lo ha recibido por error, por favor, notifíquelo al remitente.</p>";
        private const string MensRent = "Rendimiento Mensual: ";
        private const string usdTConst = "Capital acumulado en USDT: ";
        #endregion
        private readonly IEmailSender _service;
        private readonly IFuturesServices _futuresServices;
        private readonly IMovementsServices _movementServices;
        public FuturesReportController(IEmailSender service,
            IFuturesServices futuresServices,
            IMovementsServices movementsServices)
        {
            _service = service;
            _futuresServices = futuresServices;
            _movementServices = movementsServices;
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> ExportReport()
        {
           var futures = await _futuresServices.GetAllContracts();
            DateTime date = DateTime.Today;
            foreach (var f in futures)
            {
                var participation = _futuresServices.GetParticipation(f.ParticipationId);
                string emailTxt = startMessage
                                  + DateTime.Today.ToShortDateString()
                                  + br
                                  + "Contrato Nro: "
                                  + f.ContractNumber
                                  + br
                                  + "Capital inicial: "
                                  + f.Capital.ToString()
                                  + br;
                string signature = "Fecha de proximo retiro: " + f.FinishDate + br
                    + finishMessage;
                if (f.Client.Code == (int)SpecialClients.Uno)
                {
                    emailTxt = emailTxt + usdTConst + f.FinalResult + br
                    + "Recuerde que el acumulado se ve afectado por los contratos de renta fija"
                    + signature; 
                }
                else
                {
                    if (f.FixRent)
                    {
                        emailTxt = emailTxt + MensRent + f.FixRentPercentage + "%" + br
                        + signature;
                    }
                    else
                    {
                        decimal percentage = (((f.FinalResult - f.Capital) /f.Capital) * 100);
                        emailTxt = emailTxt + MensRent + percentage + "%" + br
                        + "Participacion: " + participation + br
                        + usdTConst + f.FinalResult + br
                        + signature;
                    }
                }
                var MailRequest = new MailRequestViewModel()
                {
                    ToEmail = f.Client.Email,
                    Subject = "Reporte de contrato Nro: " + f.ContractNumber + " BTC Santa Fe",
                    Body = emailTxt

                };
                await _service.SendEmailAsync(MailRequest);
            }

            return Ok();
        }


        [HttpGet]
        [Route("clean")]
        public async Task<IActionResult> CleanMovements()
        {
            var ret = await _movementServices.RemoveMovements();
            if (ret != 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}
