using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Traders.Models
{
    public class ClientDiversityViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "Cliente"), Required]
        public int ClientCode { get; set; }

        [Display(Name = "Monto BTC"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public decimal AmmountBTC { get; set; }

        [Display(Name = "Monto ETH"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public decimal AmmountETH { get; set; }

        [Display(Name = "Monto USDT"), Required]
        [Column(TypeName = "decimal(10,8)")]
        public decimal AmmountUSDT { get; set; }

        [Display(Name = "Activo"), Required]
        public bool Active { get; set; }
    }
}
