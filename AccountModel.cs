using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        public class bank_currency
        {
            public double exchange_rate { get; set; }
            public string name { get; set; }
        }

        private DateTime transactions_timestamp;

        public List<TransactionModel> transactions { get; set; }

        public List<TransactionModel> GetTransactionsByAccountId(int account_id, bool immediate = false)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - transactions_timestamp.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            //Console.WriteLine("delta: " + delta);
            //accounts_timestamp = DateTime.UtcNow;
            if (delta > 25 | immediate)
            {
                //Console.WriteLine("Cache expired");
                transactions_timestamp = DateTime.UtcNow;
                transactions = DBAccess.GetTransactionByAccountId(account_id);
                return transactions;
            }
            return transactions;
        }
    }
}
