using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaimViewModel>();
        }

        public string UserId { get; set; }
        public List<UserClaimViewModel> Claims { get; set; }
    }
}
