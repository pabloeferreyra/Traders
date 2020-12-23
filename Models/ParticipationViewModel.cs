using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class ParticipationViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Percentage { get; set; }
        public string Name { get; set; }
        public ICollection<FuturesViewModel> Futures { get; set; }
    }
}
