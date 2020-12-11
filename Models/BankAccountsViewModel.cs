using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class BankAccountsViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Nombre"), Required]
        public String Name { get; set; }
        public Decimal Amount { get; set; }
        public ICollection<MovementsViewModel> Movements { get; set; }
    }
}
