using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        [Display(Name = "Monto Ingreso"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountIn { get; set; }
        [Display(Name = "Moneda Ingreso"), Required]
        public string BadgeIn { get; set; }
        [Display(Name = "Moneda Ingreso"), Required]
        public Guid BankAccountGuidIn { get; set; }

        [Display(Name = "Monto Egreso")]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountInS { get; set; }
        [Display(Name = "Moneda Ingreso"), Required]
        public string BadgeInS { get; set; }
        [Display(Name = "Moneda Ingreso"), Required]
        public Guid BankAccountGuidInS { get; set; }

        [Display(Name = "Monto Egreso"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountOut { get; set; }
        [Display(Name = "Divisa Egreso"), Required]
        public string BadgeOut { get; set; }
        [Display(Name = "Moneda Egreso"), Required]
        public Guid BankAccountGuidOut { get; set; }

        [Display(Name ="Movimiento Correlativo")]
        public Guid? CorrelationId { get; set; }

        [Display(Name = "Monto Egreso")]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal AmountOutS { get; set; }
       
        [Display(Name = "Moneda Egreso")]
        public string BadgeOutS { get; set; }
        
        [Display(Name = "Moneda Egreso")]
        public Guid BankAccountGuidOutS { get; set; }
        
        [Display(Name = "Moneda")]
        public BankAccountsViewModel BankAccounts { get; set; }
         
        [Display(Name = "Cuenta Secundaria")]
        public BankAccountsViewModel BankAccountsS { get; set; }

        [Display(Name = "Comision"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public Decimal Comission { get; set; }
        
        [Display(Name = "Moneda Comision"), Required]
        public Guid ComissionBadgeId { get; set; }
        
        public BankAccountsViewModel ComissionBadge { get; set; }

        public DateTime DateOperation { get; set; }

    }
}
