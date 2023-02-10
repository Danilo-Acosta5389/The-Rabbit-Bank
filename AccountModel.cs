using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public class AccountModel
    {
        public int id { get; set; }

        public int user_id { get; set; }

        public string name { get; set; }

        public decimal balance { get; set; } = 0;

        public double interest_rate { get; set; } = 0;

        public string currency_name { get; set; }
        public int currency_id { get; set; } = 1;
        public decimal exchange_rate { get; set; }

        public double currency_exchange_rate { get; set; }
    }

    public class bank_currency
    {
        public double exchange_rate { get; set; }
        public string name { get; set; }
    }
}
