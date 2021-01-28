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

        [Column(TypeName = "decimal(10,8)")]
        [Display(Name = "Balance"), Required]
        public Decimal Amount { get; set; }
        public ICollection<MovementsViewModel> Movements { get; set; }
    }
}
