using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{

    public class Login
    {
        public static void LoginTry(int first_Name, int pin_Code)
        {
            int capInput = first_Name;
            //Above is converting firstName input to match the way it is capitalized in DB.
            //first_name becomes capInput. If 'john' was passed into first_name, then capInput will give 'John'.

            List<UserModel> realUser = DBAccess.CheckUsername(capInput);
            UserModel specificUser = null;
            if (realUser.Count > 0)
            {
                specificUser = realUser[0];
            }

            bool loginRunning = true;
            while (loginRunning)
            {
                List<UserModel> checkedUsers = DBAccess.CheckLogin(capInput, pin_Code);
                Console.WriteLine();
                if (specificUser == null)
                {
                    Console.WriteLine("Login failed, please try again");
                    Console.ReadLine();
                    loginRunning = false;
                    break;
                }
                else if (specificUser.id == capInput)
                {
                    if (pin_Code != int.Parse(specificUser.pin_code))
                    {
                        DBAccess.subtractAttempt(specificUser);
                        Console.WriteLine("Wrong login, please try again");
                        Console.ReadLine();
                        loginRunning = false;
                    }
                    else if (checkedUsers.Count < 1)
                    {
                        Console.WriteLine($"Your input was {capInput} {pin_Code}");
                        Console.WriteLine("Login failed, please try again");
                        Console.ReadLine();
                        loginRunning = false;
                    }
                }
                foreach (UserModel user in checkedUsers)
                {
                    if (user.blocked_user == true)
                    {
                        Console.WriteLine("Your account is locked, contact a admin!");
                        Console.ReadLine();
                        loginRunning = false;
                        break;
                    }
                    else
                    {
                        DBAccess.resetAttempts(user);
                        //user.accounts = DBAccess.GetUserAccounts(user.id);
                        //Console.WriteLine($"User ID: {user.id}");
                        //Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                        //Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                        //Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                        //Console.WriteLine($"User account list length: {user.accounts.Count}");

                        //if (user.accounts.Count > 0)
                        //{
                        //    foreach (AccountModel account in user.accounts)
                        //    {
                        //        Console.WriteLine($"ID: {account.id} Account name: {account.name} Balance: {account.balance}");
                        //        Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                        //    }
                        //}

                        //Console.WriteLine();

                        if (user.role_id == 1)
                        {
                            MenuOptions.AdminLoginMenu(user);
                        }
                        else if (user.role_id == 2)
                        {
                            MenuOptions.UserLoginMenu(user);
                        }
                        //else if(user.is_blocked)  //Blocked
                        //{
                        //    UserBlockedScreen();
                        //}


                        Console.WriteLine("Do you wish to exit? Y/N");
                        string yesNo = Console.ReadLine();

                        if (yesNo.ToLower() == "y")
                        {
                            loginRunning = false;
                        }
                        else if (user.is_client)
                        {
                            MenuOptions.UserLoginMenu(user);
                            loginRunning = false;
                        }
                    }
                }
            }
        }
    }
}