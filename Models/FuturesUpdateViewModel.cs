using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class FuturesUpdateViewModel
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        [Display(Name = "Contrato"), Required]
        public FuturesViewModel Contract { get; set; }
        [Display(Name = "Fecha Evento"), Required]
        public DateTime ModifDate { get; set; }
        [Display(Name = "Ganancia Porcentaje"), Required]
        [Column(TypeName = "decimal(4,2)")]
        public Decimal Gain { get; set; }
        [Display(Name = "Ganancia total")]
        [Column(TypeName = "decimal(10,2)")]
        public Decimal GainFinal { get; set; }
    }
}
