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
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        [Display(Name = "Fecha Inicio"), Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "Cliente"), Required]
        public ClientsViewModel Client { get; set; }
        [Display(Name = "Participacion"), Required]
        public int Participation { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime FinishDate { get; set; }
        [Display(Name = "Capital"), Required]
        [Column(TypeName = "decimal(10,2)")]
        public Decimal Capital { get; set; }
        [Display(Name = "Resultado Final")]
        [Column(TypeName = "decimal(12,2)")]
        public Decimal FinalResult { get; set; }

    }
}
