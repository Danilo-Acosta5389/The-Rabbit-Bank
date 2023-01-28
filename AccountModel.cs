using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public class BankAccountModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public decimal balance { get; set; }

        public double interest_rate { get; set; }

        public string currency_name { get; set; }

        public double currency_exchange_rate { get; set; }
    }
}
