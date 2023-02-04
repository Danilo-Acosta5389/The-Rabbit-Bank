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

        public bool locked_user { get; set; }

        //public List<AccountModel> accounts { get; set; }

        public List<AccountModel> accounts
        {
            get
            {
                return DBAccess.GetUserAccounts(id);
            }
        }

    }
}
    