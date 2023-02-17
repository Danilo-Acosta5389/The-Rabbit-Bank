using System;
namespace rabbit_bank
{
    public class UserModel
    {
        public int id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string pin_code { get; set; }

        public int role_id { get; set; }

        public int branch_id { get; set; }

        public bool is_admin { get; set; }

        public bool is_client { get; set; }
        public bool blocked_user { get; set; }
        public int attempts { get; set; }


        public List<AccountModel> accounts
        {
            get
            {
                return DBAccess.GetUserAccounts(id);
            }
        }

        //private DateTime accounts_timestamp;

        //public List<AccountModel> accounts { get; set; }

        //public List<AccountModel> GetAccounts(bool immediate = false)
        //{
        //    var ts = new TimeSpan(DateTime.UtcNow.Ticks - accounts_timestamp.Ticks);
        //    double delta = Math.Abs(ts.TotalSeconds);
        //    //Console.WriteLine("delta: " + delta);
        //    //accounts_timestamp = DateTime.UtcNow;
        //    if (delta > 45 | immediate)
        //    {
        //        //Console.WriteLine("Cache expired");
        //        accounts_timestamp = DateTime.UtcNow;
        //        accounts = DBAccess.GetUserAccounts(id);
        //        return accounts;
        //    }
        //    return accounts;
        //}

    }
}
    