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

    public static class GlobalItems
    {
        public static TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
        public static List<int> accountsList = new List<int>();
        public static List<string> accountNameList = new List<string>();
        public static List<string> currencyNameList = new List<string>();
        public static List<decimal> balanceList = new List<decimal>();
    }

    public class Login
    {
        public static void LoginTry(string first_Name, int pin_Code)
        {
            string capInput = GlobalItems.currentTextInfo.ToTitleCase(first_Name.ToLower());
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
                else if (specificUser.first_name == capInput)
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
                        Console.WriteLine($"Welcome back {user.first_name}.");
                        Console.WriteLine($"User ID: {user.id}");
                        if (user.role_id == 1)
                        {
                            Console.WriteLine($"[Admin account]");
                        }
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

                        if (user.is_admin)
                        {
                            AdminLoginMenu(user);
                            loginRunning = false;
                        }
                        else if (user.is_client)
                        {
                            UserLoginMenu(user);
                            loginRunning = false;
                        }

                        //Console.WriteLine("Do you wish to exit? Y/N");
                        //string yesNo = Console.ReadLine();

                        //if (yesNo.ToLower() == "y")
                        //{
                        //    loginRunning = false;
                        //}
                        //else if (yesNo.ToLower() == "n")
                        //{
                        //    continue;
                        //}
                    }
                }
            }
        }



        static void UserLoginMenu(UserModel userIndex)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" Make your choice with 1-6 ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("1. See your accounts and balances" +
                    "\n2. Transfer money" +
                    "\n3. Add a new account [NOT WORKING]" +
                    "\n4. Make a bank loan [NOT WORKING]" +
                    "\n5. Transaction history [NOT WORKING]" +
                    "\n6. Logout");
                Console.Write("--> ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        AccountsAndBalances(userIndex);
                        break;

                    case "2":
                        RunTransferMoney(userIndex);
                        break;
                    case "3":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för användare att lägga till nytt konto (list med olika konton: ();
                        //ToDo: sparkonto med ränta
                        break;

                    case "4":
                        // ToDo: skapa ett valutakonto i annan valuta än SEK.
                        Console.WriteLine("");
                        break;
                    case "5":
                        //ToDo: skapa en funktion för användare att låna pengar
                        Console.WriteLine("");
                        break;

                    case "6":
                        //Logout
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a number between 1 and 6.");
                        continue;
                }
            }
        }


        static void AdminLoginMenu(UserModel userIndex)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" Make your choice with 1-8 ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("1. See accounts and balances " +
                    "\n2. Transfer money" +
                    "\n3. Add a new account [NOT WORKING]" +
                    "\n4. Make a bank loan [NOT WORKING]" +
                    "\n5. Transaction history [NOT WORKING]" +
                    "\n6. Set exchange rate [NOT WORKING]" +
                    "\n7. Create new user" +
                    "\n8. Logout ");
                Console.Write("--> ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        AccountsAndBalances(userIndex);
                        break;

                    case "2":
                        RunTransferMoney(userIndex);
                        break;

                    case "3":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för användare att lägga till nytt konto (list med olika konton: ();
                        //ToDo: sparkonto med ränta
                        break;

                    case "4":
                        // ToDo: skapa ett valutakonto i annan valuta än SEK.
                        Console.WriteLine("");
                        break;

                    case "5":
                        //ToDo: skapa en funktion för användare att låna pengar
                        Console.WriteLine("");
                        break;

                    case "6":
                        // ToDo: Skapa en funktion för användare att se överföringshistorik
                        Console.WriteLine("");
                        break;

                    case "7":
                        CreateNewUser();
                        break;

                    case "8":
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a number between 1 and 8.");
                        continue;
                }
            }
        }


        static void CreateNewUser()
        {
            string yesNo;
            bool createUserRun = true;
            while (createUserRun)
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" Create new user ");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write("Please enter FirstName: ");
                    string firstName = Console.ReadLine();
                    string capFirstName = GlobalItems.currentTextInfo.ToTitleCase(firstName.ToLower());

                    Console.Write("Please enter LastName: ");
                    string lastName = Console.ReadLine();
                    string capLastName = GlobalItems.currentTextInfo.ToTitleCase(lastName.ToLower());

                    Console.Write("Please enter 4 digit PinCode: ");
                    string pinCode = Console.ReadLine();

                    //!!!!!=====================TESTING CODE BELOW=============!!!!!
                    //string pinCode;
                    //do
                    //{
                    //    Console.Write("Please enter 4 digit PinCode: ");
                    //    pinCode = Console.ReadLine();


                    //    if (pinCode.Length != 4)
                    //    {
                    //        Console.WriteLine("Error, You must enter a 4 digit PIN to continue!");
                    //        Console.Write("Do you wish to exit? Y/N: ");
                    //        yesNo = Console.ReadLine();
                    //        if (yesNo.ToLower() == "y")
                    //        {
                    //            createUserRun = false;
                    //        }
                    //        else if (yesNo.ToLower() == "n")
                    //        {
                    //            createUserRun = false;
                    //        }
                    //    }
                    //} while (pinCode.Length != 4);


                    Console.Write("Enter Role Id (1 for admin, 2 for client): ");
                    int roleId = int.Parse(Console.ReadLine());

                    Console.Write("Enter branchId (1 for default branch): ");
                    int branchId = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"FirstName: {capFirstName}");
                    Console.WriteLine($"LastName: {capLastName}");
                    Console.WriteLine($"Pin: {pinCode}");
                    Console.WriteLine($"Role ID: {roleId}");
                    Console.WriteLine($"Branch ID: {branchId}");
                    Console.Write("\nIs this correct? Y/N --> ");
                    yesNo = Console.ReadLine();
                    if (yesNo.ToLower() == "y")
                    {
                        UserModel newUser = new UserModel
                        {
                            first_name = capFirstName,
                            last_name = capLastName,
                            pin_code = pinCode,
                            role_id = roleId,
                            branch_id = branchId
                        };
                        DBAccess.SaveBankUser(newUser);
                        Console.WriteLine("\nUser has successfully been created!");
                        Thread.Sleep(1000);
                    }
                    else if (yesNo.ToLower() == "n")
                    {
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.Write("\nError occured. Would you like to exit? Y/N: --> ");
                    //string yesNo = Console.ReadLine();
                    yesNo = Console.ReadLine();
                    if (yesNo.ToLower() == "y")
                    {
                        createUserRun = false;
                    }
                    else if (yesNo.ToLower() == "n")
                    {
                        continue;
                    }
                }
            }
        }

        public static void RunTransferMoney(UserModel userIndex) //THIS IS ONLY FOR RUNNING TransferMoney()
        {
            bool appRunning = true;
            while (appRunning)
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" Transfer Money ");
                    Console.ResetColor();
                    Console.WriteLine("\n");

                    Console.WriteLine("1. Transfer between own accounts.");
                    Console.WriteLine("2. Transfer to others account. ");
                    Console.WriteLine("3. Return to menu");
                    Console.Write("--> ");
                    bool success;
                    int options = int.Parse(Console.ReadLine());
                    switch (options)
                    {
                        case 1:
                            success = TransferOwnAccounts(userIndex);
                            if (success)
                            {
                                //AccountsAndBalances(userIndex);
                                Console.Write("\nPlease press ENTER to continue ");
                                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                Console.WriteLine();
                            }
                            break;
                        case 2:
                            success = TransferOthersAccounts(userIndex);
                            if (success)
                            {
                                Console.Write("\nPlease press ENTER to continue");
                                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                                Console.WriteLine();
                            }
                            break;
                        case 3:
                            appRunning = false;
                            break;
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine("Error");
                    Console.WriteLine("Do you wish to exit?");
                    Console.Write("Y/N --> ");
                    string yesNo = Console.ReadLine();
                    if (yesNo.ToLower() == "y")
                    {
                        break;
                    }
                    else if (yesNo.ToLower() == "n")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input");
                    }
                }
            }
        }

        public static bool TransferOthersAccounts(UserModel userIndex)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" Transfer to others account ");
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    List<AccountModel> tempAccount = userIndex.accounts;

                    for (int i = 0; i < tempAccount.Count; i++)
                    {
                        GlobalItems.accountNameList.Add(tempAccount[i].name);
                        GlobalItems.accountsList.Add(tempAccount[i].id);
                        GlobalItems.currencyNameList.Add(tempAccount[i].currency_name);
                        Console.WriteLine($"{i + 1}. {tempAccount[i].name}");
                        if (tempAccount[i].currency_name == "SEK")
                        {
                            Console.WriteLine($"Balance: {tempAccount[i].balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                        }
                        else if (tempAccount[i].currency_name == "USD")
                        {
                            Console.WriteLine($"Balance: {tempAccount[i].balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("\nPlease input FROM acount");
                    Console.Write("Account number here --> ");
                    int fromAccount = int.Parse(Console.ReadLine()) - 1;
                    int fromAccountID = GlobalItems.accountsList[fromAccount];
                    string fromAccountName = GlobalItems.accountNameList[fromAccount];

                    Console.WriteLine("\nPlease input RECIEVING account number/id");
                    Console.Write("Account id here --> ");
                    int toAccount = int.Parse(Console.ReadLine());

                    Console.Write("\nPlease input amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    Console.WriteLine($"\nFrom {fromAccountName}");
                    Console.WriteLine($"To account number/id: {toAccount}");
                    if (GlobalItems.currencyNameList[fromAccount] == "SEK")
                    {
                        Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                    }
                    else if (GlobalItems.currencyNameList[fromAccount] == "USD")
                    {
                        Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                    }


                    Console.Write("\nIs this correct? Y/N: ");
                    string yesNo = Console.ReadLine();

                    if (yesNo.ToLower() == "y")
                    {
                        return DBAccess.TransferMoney(userIndex.id, 0, fromAccountID, toAccount, amount);
                    }
                    else if (yesNo.ToLower() == "n")
                    {
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("\nError");
                    Console.WriteLine("Do you wish to exit?");
                    Console.Write("Y/N --> ");
                    string yesNo = Console.ReadLine();
                    if (yesNo.ToLower() == "y")
                    {
                        return false;
                    }
                    else if(yesNo.ToLower() == "n")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input");
                    }
                }
            }
        }


        public static bool TransferOwnAccounts(UserModel userIndex)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" Transfer between own accounts ");
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    List<AccountModel> tempAccount = userIndex.accounts;

                    // Below we iterate through the logged in persons bank accounts in DB
                    //Each account is added to a List. The list index is showed to the user.

                    for (int i = 0; i < userIndex.accounts.Count; i++)
                    {
                        //GlobalItems.accountsList.Add(userIndex.accounts[i].id);
                        //GlobalItems.balanceList.Add(userIndex.accounts[i].balance);
                        Console.WriteLine($"Account number/ID: {userIndex.accounts[i].id}");
                        Console.WriteLine($"Account name: {userIndex.accounts[i].name}");
                        //Console.WriteLine($"Balance: {tempAccount[i].balance}");
                        if (userIndex.accounts[i].currency_name == "SEK")
                        {
                            Console.WriteLine($"Balance: {userIndex.accounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                        }
                        else if (tempAccount[i].currency_name == "USD")
                        {
                            Console.WriteLine($"Balance: {userIndex.accounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                        }
                        Console.WriteLine();
                    }

                    Console.Write("\nPlease input from account: ");
                    int fromAccount = int.Parse( Console.ReadLine());
                    //int fromAccountID = GlobalItems.accountsList[fromAccount];
                    
                    Console.Write("Please input to account: ");
                    int toAccount = int.Parse(Console.ReadLine());
                    //int toAccountID = GlobalItems.accountsList[toAccount];

                    Console.Write("Please input amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    //decimal newAmount = GlobalItems.balanceList[amount];


                    return DBAccess.TransferMoney(userIndex.id, userIndex.id, fromAccount, toAccount, amount);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nError");
                    Console.WriteLine("Do you wish to exit?");
                    Console.Write("Y/N --> ");
                    string yesNo = Console.ReadLine();

                    if (yesNo.ToLower() == "y")
                    {
                        return false;
                    }
                    else if (yesNo.ToLower() == "n")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input");
                    }


                    //=========TESTING CODE BELOW==================

                    //Console.ReadKey(true).Key != ConsoleKey.Enter
                    //if (yesNo.ToLower() == "y")

                    //ConsoleKey keyPressed;
                    //ConsoleKeyInfo yesNo = Console.ReadKey();
                    //keyPressed = yesNo.Key;

                    //if (keyPressed == ConsoleKey.Y)
                    //{
                    //    return false;
                    //}
                    //else if (keyPressed == ConsoleKey.N)
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //    Console.WriteLine("\nInvalid input");
                    //}
                }
            }
        }


        static void AccountsAndBalances(UserModel userIndex)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Accounts and balances ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            //CultureInfo c2 = CultureInfo.GetCultureInfo("sv-SE");
            //.ToString(c2)
            List<AccountModel> tempAccount = userIndex.accounts;
            int counter = 1;
            foreach (AccountModel account in tempAccount) // This is used to iterate through the logged in persons bank_account in DB
            {
                //List<AccountModel> tempAccount = userIndex.accounts;
                Console.WriteLine($"{counter}. {account.name}");
                Console.WriteLine($"Account id/nummber: {account.id}");
                if(account.currency_name == "SEK")
                {
                    Console.WriteLine($"Balance: {account.balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                }
                else if (account.currency_name == "USD")
                {
                    Console.WriteLine($"Balance: {account.balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                }
                
                if(account.interest_rate > 0) { Console.WriteLine($"Interest rate: {account.interest_rate} %"); }
                Console.WriteLine();
                counter++;
            }
            Console.WriteLine("Please press ENTER to continue.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }
    }
}