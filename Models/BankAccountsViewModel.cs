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
        public Guid Id { get; set; }
        [Display(Name = "Nombre"), Required]
        public String Name { get; set; }
        [Column(TypeName = "decimal(10,8)")]
        [Display(Name = "Balance")]
        public Decimal Amount { get; set; }
        public ICollection<MovementsViewModel> Movements { get; set; }
    }
}
