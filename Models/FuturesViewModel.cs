using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class FuturesViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Display(Name = "Numero de contrato")]
        public int ContractNumber { get; set; }

        [Display(Name = "Fecha Inicio"), Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Cliente")]
        public ClientsViewModel Client { get; set; }
        public Guid? ClientId { get; set; }

        [Display(Name = "Participacion")]
        public Guid? ParticipationId { get; set; }
        [Display(Name = "Participacion")]
        public ParticipationViewModel Participation { get; set; }
        [Display(Name = "Fecha Fin")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }

        [Display(Name = "Plazo")]
        public int Term { get; set; }

        [Display(Name = "Capital"), Required]
        [Column(TypeName = "decimal(30,10)")]
        public decimal Capital { get; set; }

        [Display(Name = "Renta Fija?")]
        public bool FixRent { get; set; }
        
        [Display(Name = "Renta Fija")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal? FixRentPercentage { get; set; }

        [Display(Name = "Referido")]
        public string RefeerCode { get; set; }

        [Display(Name = "Referido")]
        public Guid? Refeer { get; set; }

        public bool Retire { get; set; }

        [Display(Name = "Resultado")]
        [Column(TypeName = "decimal(30,10)")]
        public decimal FinalResult { get; set; }

        [Column(TypeName = "decimal(30,10)")]
        public decimal LastGain { get; set; }

        [Display(Name = "Moneda de inicio"), Required]
        public string StartCurrency { get; set; }
        
        [Display(Name = "Moneda de retiro"), Required]
        public string RetireCurrency { get; set; }
        public ICollection<FuturesUpdateViewModel> FuturesUpdates { get; set; }

    }
}
