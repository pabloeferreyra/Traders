using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class MovementsViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Display(Name = "Fecha y hora"), Required]
        [DataType(DataType.DateTime)]
        public DateTime DateMov { get; set; }
        
        [Display(Name ="Usuario"), Required]
        public string UserGuid { get; set; }

        public Decimal AmountIn { get; set; }
        public Guid BadgeGuidIn { get; set; }
        public Guid BankAccountGuidIn { get; set; }

        public Decimal AmountOut { get; set; }
        public Guid BadgeGuidOut { get; set; }
        public Guid BankAccountGuidOut { get; set; }

        [Display(Name = "Divisa"), Required]
        public BadgesViewModel Badges { get; set; }

        [Display(Name = "Cuenta"), Required]
        public BankAccountsViewModel BankAccounts { get; set; }

    }
}
