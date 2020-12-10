using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traders.Models
{
    public class BankAccountsViewModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Decimal Amount { get; set; }
    }
}
