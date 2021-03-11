using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class ClientsViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Display(Name = "Email"), Required]
        public string Email { get; set; }

        [Display(Name = "Nombre"), Required]
        public string Name { get; set; }

        [Display(Name = "Dni/Cuit"), Required]
        public string Dni { get; set; }

        [Display(Name = "Fecha Nacimiento"), Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Direccion"), Required]
        public string Address { get; set; }

        [Display(Name = "Ciudad"), Required]
        public string City { get; set; }

        [Display(Name = "Provincia"), Required]
        public string Province { get; set; }

        [Display(Name = "Pais"), Required]
        public string Country { get; set; }

        [Display(Name = "Celular")]
        public long Phone { get; set; }

        [Display(Name = "Billetera Virtual 1")]
        public string VirtualWallet1 { get; set; }

        [Display(Name = "Billetera Virtual 2")]
        public string VirtualWallet2 { get; set; }

        [Display(Name = "PEP"), Required]
        public bool Pep { get; set; }
        public ICollection<FuturesViewModel> Futures { get; set; }
        [Display(Name = "Contratos")]
        public int Contracts { get; set; }
    }
}
