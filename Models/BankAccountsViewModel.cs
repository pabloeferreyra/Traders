using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class BankAccountsViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "Moneda"), Required]
        public string Currency { get; set; }

        [Display(Name = "Balance"), Required]
        [Column(TypeName = "decimal(30,10)")]
        public decimal Amount { get; set; }
        public ICollection<MovementsViewModel> Movements { get; set; }
    }
}
