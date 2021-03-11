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
        private const string finishMessage = br
                    + "Este mensaje es automático, por favor no responda. Por cualquier consulta comunicate con nosotros por los medios habituales."+ br 
                    + "<p>* La información contenida en este correo es privada y confidencial.Si cree que lo ha recibido por error, por favor, notifíquelo al remitente.</p>"
                    + "<p><b>Bitcoin Santa Fe</b></p>";
        private const string MensRent = "Interés: ";
        private const string usdTConst = "Capital acumulado al último cierre: ";
        private const string emailTitle = "Reporte de contratos BTC Santa Fe";
        private const string usdTxt = "USD";
        #endregion
        private readonly IEmailSender _service;
        private readonly IFuturesServices _futuresServices;
        private readonly IMovementsServices _movementServices;
        private readonly IClientServices _clientServices;
        public FuturesReportController(IEmailSender service,
            IFuturesServices futuresServices,
            IMovementsServices movementsServices,
            IClientServices clientServices)
        {
            _service = service;
            _futuresServices = futuresServices;
            _movementServices = movementsServices;
            _clientServices = clientServices;
        }

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> ExportReport()
        {
            var clients = await _clientServices.GetAllClients();

            for (int c = 0; c < clients.Count(); c++)
            {
                string emailHead = "Hola " 
                                    + clients[c].Name 
                                    + "," 
                                    + br 
                                    + "Este es un resumen de tu cuenta en Bitcoin Santa Fe al día " 
                                    + DateTime.Today.ToShortDateString()
                                    + br;
                string emailTxt = emailHead;
                foreach (var f in clients[c].Futures)
                {
                    emailTxt = emailHead
                                      + DateTime.Today.ToShortDateString()
                                      + br
                                      + "Cuenta: "
                                      + f.ContractNumber
                                      + br
                                      + "Capital inicial: "
                                      + String.Format("{0:.00}", f.Capital) + usdTxt
                                      + br;
                    string signature = "Fecha de proximo retiro: " + f.FinishDate.ToShortDateString() + br;
                        
                    if (f.ContractNumber == (int)SpecialClients.Uno)
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
                            decimal percentage = (((f.FinalResult - f.Capital) / f.Capital) * 100);
                            emailTxt = emailTxt + MensRent + String.Format("{0:.00}", percentage) + "%" + br
                            + usdTConst + String.Format("{0:.00}", f.FinalResult) + usdTxt + br
                            + signature;
                        }
                    }
                    
                }

                emailTxt += finishMessage;
                var MailRequest = new MailRequestViewModel()
                {
                    ToEmail = clients[c].Email,
                    Subject = emailTitle,
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
