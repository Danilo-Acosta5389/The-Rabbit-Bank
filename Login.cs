using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public class Login
    {
        public static void LoginTry(string first_Name, int pin_Code)
        {
            bool loginRunning = true;
            while (loginRunning)
            {
                List<BankUserModel> checkedUsers = DBdataAccess.CheckLogin(first_Name, pin_Code);

                if (checkedUsers.Count < 1)
                {
                    Console.WriteLine("Login failed, please try again");
                    loginRunning = false;
                }
                foreach (BankUserModel user in checkedUsers)
                {
                    user.accounts = DBdataAccess.GetUserAccounts(user.id);
                    Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                    Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                    //Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                    Console.WriteLine($"User account list length: {user.accounts.Count}");
                    if(user.is_admin == true)
                    {
                        Console.WriteLine("is admin"); 
                    }
                    else
                    {
                        Console.WriteLine("is client");
                    }

                    if (user.accounts.Count > 0)
                    {
                        foreach (BankAccountModel account in user.accounts)
                        {
                            Console.WriteLine($"ID: {account.id} Account name: {account.name} Balance: {account.balance}");
                            Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                        }
                    }
                    Console.WriteLine("Do you wish to exit? Y/N");
                    string yesNo = Console.ReadLine();

                    if (yesNo.ToLower() == "y")
                    {
                        loginRunning = false;
                    }
                    else if (yesNo.ToLower() == "n")
                    {
                        continue;
                    }
                }
            }
        }

        
    }
}
