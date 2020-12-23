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
        
        [Display(Name = "Monto venta"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountIn { get; set; }
        
        public Guid BadgeGuidIn { get; set; }
       
        public Guid BankAccountGuidIn { get; set; }


        [Display(Name = "Monto venta")]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountInS { get; set; }

        public Guid BadgeGuidInS { get; set; }

        public Guid BankAccountGuidInS { get; set; }

        [Display(Name = "Monto compra"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountOut { get; set; }
        public Guid BadgeGuidOut { get; set; }
        public Guid BankAccountGuidOut { get; set; }

        [Display(Name = "Monto compra Segundo")]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountOutS { get; set; }

        public Guid BadgeGuidOutS { get; set; }

        public Guid BankAccountGuidOutS { get; set; }

        [Display(Name = "Divisa"), Required]
        public BadgesViewModel Badges { get; set; }

        [Display(Name = "Cuenta"), Required]
        public BankAccountsViewModel BankAccounts { get; set; }
        
        [Display(Name = "Divisa")]
        public BadgesViewModel BadgesS { get; set; }

        [Display(Name = "Cuenta")]
        public BankAccountsViewModel BankAccountsS { get; set; }

        [Display(Name = "Comision"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal Comission { get; set; }

        public DateTime DateOperation { get; set; }

    }
}
