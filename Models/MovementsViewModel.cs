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

    }
}
