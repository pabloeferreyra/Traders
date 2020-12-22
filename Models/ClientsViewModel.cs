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

        [Display(Name = "Codigo"), Required]
        public int Code { get; set; }

        [Display(Name = "Email"), Required]
        public string Email { get; set; }
        public ICollection<FuturesViewModel> Futures { get; set; }
    }
}
