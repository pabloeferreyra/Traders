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
        [Display(Name = "Cliente"), Required]
        public Guid ClientId { get; set; }
        [Display(Name = "Fecha Inicio"), Required]
        public DateTime StartDate { get; set; }
        
        public ClientsViewModel Client { get; set; }
        [Display(Name = "Participacion"), Required]
        public Guid ParticipationId { get; set; }
        public ParticipationViewModel Participation { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime FinishDate { get; set; }
        [Display(Name = "Capital"), Required]
        [Column(TypeName = "decimal(10,2)")]
        public Decimal Capital { get; set; }
        [Display(Name = "Resultado Final")]
        [Column(TypeName = "decimal(12,2)")]
        public Decimal FinalResult { get; set; }

        public ICollection<FuturesUpdateViewModel> FuturesUpdates { get; set; }

    }
}
