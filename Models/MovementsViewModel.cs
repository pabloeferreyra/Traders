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

        #region PrimerMovimiento
        [Display(Name = "Monto Ingreso"), Required]
        [Column(TypeName = "decimal(30,10)")]
        public decimal AmountIn { get; set; }

        [Display(Name = "Moneda Ingreso")]
        public string BadgeIn { get; set; }

        [Display(Name = "Moneda Ingreso")]
        public Guid BankAccountGuidIn { get; set; }

        [Display(Name = "Monto Egreso"), Required]
        [Column(TypeName = "decimal(30,10)")]
        public decimal AmountOut { get; set; }

        [Display(Name = "Divisa Egreso")]
        public string BadgeOut { get; set; }

        [Display(Name = "Moneda Egreso")]
        public Guid BankAccountGuidOut { get; set; }

        [Display(Name = "Moneda")]
        public BankAccountsViewModel BankAccounts { get; set; }
        #endregion

        #region segundoMovimiento
        [Display(Name = "Monto Ingreso")]
        [Column(TypeName = "decimal(30,10)")]
        public decimal AmountInS { get; set; }

        [Display(Name = "Moneda Ingreso")]
        public string BadgeInS { get; set; }

        [Display(Name = "Moneda Ingreso")]
        public Guid BankAccountGuidInS { get; set; }

        [Display(Name = "Monto Egreso")]
        [Column(TypeName = "decimal(30,10)")]
        public decimal AmountOutS { get; set; }

        [Display(Name = "Moneda Egreso")]
        public string BadgeOutS { get; set; }

        [Display(Name = "Moneda Egreso")]
        public Guid BankAccountGuidOutS { get; set; }

        [Display(Name = "Cuenta Secundaria")]
        public BankAccountsViewModel BankAccountsS { get; set; }
        #endregion

        [Display(Name ="Movimiento Correlativo")]
        public Guid? CorrelationId { get; set; }

        [Display(Name = "Comision"), Required]
        [Column(TypeName = "decimal(30,10)")]
        public decimal Comission { get; set; }
        
        [Display(Name = "Moneda Comision"), Required]
        public Guid ComissionBadgeId { get; set; }
        
        public BankAccountsViewModel ComissionBadge { get; set; }

        public DateTime DateOperation { get; set; }

    }
}
