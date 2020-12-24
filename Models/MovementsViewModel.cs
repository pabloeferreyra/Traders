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
        
        [Display(Name = "Monto Compra"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountIn { get; set; }
        [Display(Name = "Divisa Compra"), Required]
        public Guid BadgeGuidIn { get; set; }
        [Display(Name = "Cuenta Compra"), Required]
        public Guid BankAccountGuidIn { get; set; }


        [Display(Name = "Monto venta")]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountInS { get; set; }
        [Display(Name = "Divisa Compra"), Required]
        public Guid BadgeGuidInS { get; set; }
        [Display(Name = "Cuenta Compra"), Required]
        public Guid BankAccountGuidInS { get; set; }

        [Display(Name = "Monto Venta"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountOut { get; set; }
        [Display(Name = "Divisa Venta"), Required]
        public Guid BadgeGuidOut { get; set; }
        [Display(Name = "Cuenta Venta"), Required]
        public Guid BankAccountGuidOut { get; set; }

        [Display(Name = "Monto Venta")]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountOutS { get; set; }
       
        [Display(Name = "Divisa Venta")]
        public Guid BadgeGuidOutS { get; set; }
        
        [Display(Name = "Cuenta Venta")]
        public Guid BankAccountGuidOutS { get; set; }
        
        [Display(Name = "Divisa")]
        public BadgesViewModel Badges { get; set; }
        
        [Display(Name = "Cuenta")]
        public BankAccountsViewModel BankAccounts { get; set; }
        
        [Display(Name = "Divisa Secundaria")]
        public BadgesViewModel BadgesS { get; set; }
        
        [Display(Name = "Cuenta Secundaria")]
        public BankAccountsViewModel BankAccountsS { get; set; }

        [Display(Name = "Comision"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal Comission { get; set; }

        public DateTime DateOperation { get; set; }

    }
}
