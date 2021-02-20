using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class RetireFuturesViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "Numero de contrato")]
        public int ContractNumber { get; set; }
        [Display(Name = "Fecha Retiro"), Required]
        public DateTime RetireDate { get; set; }
        [Display(Name = "Capital"), Required]
        [Column(TypeName = "decimal(30,10)")]
        public decimal Capital { get; set; }
        [Display(Name = "Retira")]
        [Column(TypeName = "decimal(30,10)")]
        public decimal RetireCapital { get; set; }
    }
}
