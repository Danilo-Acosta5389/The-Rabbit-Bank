using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public class MenuOptions
    {
        public static void UserLoginMenu(UserModel userIndex)
        {
            Console.WriteLine($"Welcome back {userIndex.first_name}.");
            Console.WriteLine($"User ID: {userIndex.id}");
            if (userIndex.role_id == 1)
            {
                Console.WriteLine($"[Admin account]");
            }

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
                        MenuOptions.AccountsAndBalances(userIndex);
                        break;

                    case "2":
                        MenuOptions.RunTransferMoney(userIndex);
                        break;
                    case "3":
                        Console.WriteLine("");
                        Console.WriteLine("==========\nSkapa nytt konto\n========");
                        MenuOptions.CreateAccount(userIndex);
                        //ToDo: skapa funktion för användare att lägga till nytt konto (list med olika konton: ();
                        //ToDo: sparkonto med ränta
                        break;

                    case "4":
                        // ToDo: skapa ett valutakonto i annan valuta än SEK.
                        Console.WriteLine("==========\nSkapa nytt konto\n========");
                        //CreateAccount(userIndex);
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


        public static void AdminLoginMenu(UserModel userIndex)
        {
            Console.WriteLine($"Welcome back {userIndex.first_name}.");
            Console.WriteLine($"User ID: {userIndex.id}");
            if (userIndex.role_id == 1)
            {
                Console.WriteLine($"[Admin account]");
            }

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

                Console.WriteLine("1. Block/Unblock Users" +
                    "\n2. Transaction history [NOT WORKING]" +
                    "\n3. Set exchange rate [NOT WORKING]" +
                    "\n4. Create new user" +
                    "\n5. Logout ");
                Console.Write("--> ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("List of users:");
                        List<UserModel> user = DBAccess.LoadBankUsers();
                        foreach (UserModel u in user)
                        {
                            Console.WriteLine($"ID is: {u.id}, Name is {u.first_name}, number of attempts: {u.attempts}");
                        }
                        Console.WriteLine("\n1. Block a user with ID\n2. Unblock user by ID");
                        string bChoice = Console.ReadLine();
                        switch (bChoice)
                        {
                            case "1":
                                Console.WriteLine("input id of user you want to block");
                                int pick = int.Parse(Console.ReadLine());
                                DBAccess.blockUser(pick);
                                Console.WriteLine("Done!");
                                Console.ReadLine();
                                break;
                            case "2":
                                Console.WriteLine("input id of user you want to unblock");
                                int id = int.Parse(Console.ReadLine());
                                DBAccess.unblockUser(id);
                                Console.WriteLine("Done!");
                                Console.ReadLine();
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för användare att lägga till nytt konto (list med olika konton: ();
                        //ToDo: sparkonto med ränta
                        break;

                    case "3":
                        // ToDo: skapa ett valutakonto i annan valuta än SEK.
                        Console.WriteLine("");
                        break;

                    case "4":
                        MenuOptions.CreateNewUser();
                        break;

                    case "5":
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a number between 1 and 8.");
                        continue;
                }
            }
        }

        public static void CreateNewUser()
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
                        break;
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

                    var tempList = GlobalItems.accountsList;
                    tempList.Clear();

                    for (int i = 0; i < userIndex.accounts.Count; i++)
                    {
                        //GlobalItems.accountNameList.Add(tempAccount[i].name);
                        tempList.Add(userIndex.accounts[i].id);
                        GlobalItems.currencyNameList.Add(userIndex.accounts[i].currency_name);
                        //GlobalItems.currencyRateList.Add(userIndex.accounts[i].currency_exchange_rate);
                        Console.WriteLine($"{userIndex.accounts[i].name}");
                        Console.WriteLine($"Account number/ID: {userIndex.accounts[i].id}");
                        //Console.WriteLine(userIndex.accounts[i].currency_exchange_rate);

                        if (userIndex.accounts[i].currency_name == "SEK")
                        {
                            Console.WriteLine($"Balance: {userIndex.accounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                        }
                        else if (userIndex.accounts[i].currency_name == "USD")
                        {
                            Console.WriteLine($"Balance: {userIndex.accounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                        }
                        Console.WriteLine();
                    }

                    

                    Console.WriteLine("\nPlease input FROM account");
                    Console.Write("Account number/ID here --> ");
                    int fromAccount = int.Parse(Console.ReadLine());

                    int lastOnList = tempList.Last();

                    for (int i = 0; i < tempList.Count; i++)
                    {
                        //Console.WriteLine(tempList[i]);
                        if (fromAccount == tempList[i])
                        {
                            //Console.WriteLine("{0} = {1}", fromAccount, tempList[i]);
                            //Console.WriteLine("WOHOO FOUND IT");
                            break;
                        }
                        else if (lastOnList == tempList[i])
                        {
                            Console.WriteLine("Error. Invalid account number/ID.");
                            return false;
                        }
                    }


                    Console.WriteLine("\nPlease input RECIEVING account number/ID");
                    Console.Write("Account numeber/ID here --> ");
                    int toAccount = int.Parse(Console.ReadLine());

                    Console.Write("\nPlease input amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());


                    Console.WriteLine($"\nFrom account number/ID: {fromAccount}");
                    Console.WriteLine($"To account number/ID: {toAccount}");
                    Console.WriteLine($"Amount: {amount}");



                    //for (int i = 0; i < tempCurrList.Count; i++)
                    //{
                    //    Console.WriteLine(tempIDlist[i]);      //38, 40, 41, 42, 45
                    //    Console.WriteLine(tempCurrList[i]); // SEK, SEK, USD, USD, SEK
                    //    Console.WriteLine();


                    //    if (fromAccount == tempIDlist[i] && tempCurrList[i] == "SEK")
                    //    {
                    //        Console.WriteLine("From account is swedish");

                    //        for (int j = 0; j < tempIDlist.Count; j++)
                    //        {
                    //            if (toAccount == tempIDlist[j] && tempCurrList[j] == "SEK")
                    //            {
                    //                Console.WriteLine("To account is swedish");
                    //            }
                    //            else if (toAccount == tempIDlist[j] && tempCurrList[j] == "USD")
                    //            {
                    //                Console.WriteLine("To account is american");
                    //                Console.WriteLine($"{convertCurrency(amount, "sek")}");
                    //                amount = Convert.ToDecimal(convertCurrency(amount, "sek"));
                    //                Console.WriteLine("LOOOK HERE! USD");
                    //            }
                    //        }

                    //    }
                    //    else if (fromAccount == tempIDlist[i] && tempCurrList[i] == "USD")
                    //    {
                    //        Console.WriteLine("From account is american");
                    //        Console.WriteLine();

                    //        for (int j = 0; j < tempIDlist.Count; j++)
                    //        {
                    //            if (toAccount == tempIDlist[j] && tempCurrList[j] == "SEK")
                    //            {
                    //                Console.WriteLine("To account is swedish");
                    //                Console.WriteLine($"{convertCurrency(amount, "usd")}");
                    //                amount = Convert.ToDecimal(convertCurrency(amount, "usd"));
                    //                Console.WriteLine("LOOK HERE!! SEK");
                    //            }
                    //            else if (toAccount == tempIDlist[j] && tempCurrList[j] == "USD")
                    //            {
                    //                Console.WriteLine("To account is american");
                    //            }
                    //        }
                    //    }

                    //}
                    //decimal newAmount = Math.Round(amount, 2);
                    //Console.WriteLine("HERE AGAIN");
                    //Console.WriteLine(newAmount);
                    //Console.WriteLine();




                    //if (GlobalItems.currencyNameList[0] == "SEK")
                    //{
                    //    Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                    //}
                    //else if (GlobalItems.currencyNameList[1] == "USD")
                    //{
                    //    Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                    //}




                    //Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                    //for (int i = 0; i < GlobalItems.currencyNameList.Count; i++)
                    //{
                    //    Console.WriteLine(GlobalItems.currencyNameList[i]);

                    //    if (GlobalItems.currencyNameList[i] == "SEK")
                    //    {
                    //        Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                    //    }
                    //    else if (GlobalItems.currencyNameList[i] == "USD")
                    //    {
                    //        Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                    //    }
                    //}


                    Console.Write("\nIs this correct? Y/N: ");
                    string yesNo = Console.ReadLine();

                    if (yesNo.ToLower() == "y")
                    {
                        return DBAccess.TransferMoney(userIndex.id, 0, fromAccount, toAccount, amount);
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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

                    var tempIDlist = GlobalItems.accountsList;
                    tempIDlist.Clear();
                    var tempCurrList = GlobalItems.currencyNameList;
                    tempCurrList.Clear();
                    // Below we iterate through the logged in persons bank accounts in DB
                    //Each account is added to a List. The list index is showed to the user.

                    for (int i = 0; i < userIndex.accounts.Count; i++)
                    {
                        tempIDlist.Add(userIndex.accounts[i].id);
                        //GlobalItems.balanceList.Add(userIndex.accounts[i].balance);
                        tempCurrList.Add(userIndex.accounts[i].currency_name);
                        Console.WriteLine($"Account number/ID: {userIndex.accounts[i].id}");
                        Console.WriteLine($"Account name: {userIndex.accounts[i].name}");
                        //Console.WriteLine($"Balance: {tempAccount[i].balance}");
                        if (userIndex.accounts[i].currency_name == "SEK")
                        {
                            Console.WriteLine($"Balance: {userIndex.accounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                        }
                        else if (userIndex.accounts[i].currency_name == "USD")
                        {
                            Console.WriteLine($"Balance: {userIndex.accounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                        }
                        Console.WriteLine();
                    }
                    
                    Console.Write("\nPlease input from account: ");
                    int fromAccount = int.Parse(Console.ReadLine());

                    int lastOnList = tempIDlist.Last();

                    for (int i = 0; i < tempIDlist.Count; i++)
                    {
                        Console.WriteLine(tempIDlist[i]);
                        if (fromAccount == tempIDlist[i])
                        {
                            Console.WriteLine("{0} = {1}", fromAccount, tempIDlist[i]);
                            Console.WriteLine("WOHOO FOUND IT");
                            break;
                        }
                        else if (lastOnList == tempIDlist[i])
                        {
                            Console.WriteLine("Error. Invalid account number/ID.");
                            return false;
                        }
                    }

                    Console.Write("Please input to account: ");
                    int toAccount = int.Parse(Console.ReadLine());
                    //int toAccountID = GlobalItems.accountsList[toAccount];

                    for (int i = 0; i < tempIDlist.Count; i++)
                    {
                        //Console.WriteLine(tempList[i]);
                        if (toAccount == tempIDlist[i])
                        {
                            //Console.WriteLine("{0} = {1}", toAccount, tempList[i]);
                            //Console.WriteLine("WOHOO FOUND IT");
                            break;
                        }
                        else if (lastOnList == tempIDlist[i])
                        {
                            Console.WriteLine("Error. Invalid account number/ID.");
                            return false;
                        }
                    }

                    Console.Write("Please input amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());



                    for (int i = 0; i < tempCurrList.Count; i++)
                    {
                        Console.WriteLine(tempIDlist[i]);      //38, 40, 41, 42, 45
                        Console.WriteLine(tempCurrList[i]); // SEK, SEK, USD, USD, SEK
                        Console.WriteLine();


                        if (fromAccount == tempIDlist[i] && tempCurrList[i] == "SEK")
                        {
                            Console.WriteLine("From account is swedish");

                            for (int j = 0; j < tempIDlist.Count; j++)
                            {
                                if (toAccount == tempIDlist[j] && tempCurrList[j] == "SEK")
                                {
                                    Console.WriteLine("To account is swedish");
                                }
                                else if (toAccount == tempIDlist[j] && tempCurrList[j] == "USD")
                                {
                                    Console.WriteLine("To account is american");
                                    Console.WriteLine($"{convertCurrency(amount, "sek")}");
                                    amount = Convert.ToDecimal(convertCurrency(amount, "sek"));
                                    Console.WriteLine("LOOOK HERE! USD");
                                }
                            }

                        }
                        else if (fromAccount == tempIDlist[i] && tempCurrList[i] == "USD")
                        {
                            Console.WriteLine("From account is american");
                            Console.WriteLine();

                            for (int j = 0; j < tempIDlist.Count; j++)
                            {
                                if (toAccount == tempIDlist[j] && tempCurrList[j] == "SEK")
                                {
                                    Console.WriteLine("To account is swedish");
                                    Console.WriteLine($"{convertCurrency(amount, "usd")}");
                                    amount = Convert.ToDecimal(convertCurrency(amount, "usd"));
                                    Console.WriteLine("LOOK HERE!! SEK");
                                }
                                else if (toAccount == tempIDlist[j] && tempCurrList[j] == "USD")
                                {
                                    Console.WriteLine("To account is american");
                                }
                            }
                        }
                        
                    }
                    decimal newAmount = Math.Round(amount, 2);
                    Console.WriteLine("HERE AGAIN");
                    Console.WriteLine(newAmount);
                    Console.WriteLine();

                    return DBAccess.TransferMoney(userIndex.id, userIndex.id, fromAccount, toAccount, newAmount);
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


        public static void AccountsAndBalances(UserModel userIndex)
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
                if (account.currency_name == "SEK")
                {
                    Console.WriteLine($"Balance: {account.balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                }
                else if (account.currency_name == "USD")
                {
                    Console.WriteLine($"Balance: {account.balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                }

                if (account.interest_rate > 0) { Console.WriteLine($"Interest rate: {account.interest_rate} %"); }
                Console.WriteLine();
                counter++;
            }
            Console.WriteLine("Please press ENTER to continue.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }


        public static void CreateAccount(UserModel userIndex)
        {
            Console.Clear();
            double interestRate = 2.85;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            bool createAccRunning = true;
            while (createAccRunning)
            {
                for (int i = 0; i < userIndex.accounts.Count; i++)
                {
                    GlobalItems.currencyRateList.Add(userIndex.accounts[i].currency_exchange_rate);
                }
                Console.WriteLine("==============================\nVälj vilket konto du vill skapa.\n==============================");
                Console.ResetColor();
                Console.WriteLine("1. Checking account. Sorry, no interest rate.");
                Console.WriteLine($"2. Savings account. Current interest rate: {interestRate} %");
                Console.WriteLine($"3. Currency account (Currency USD || Exchange rate: {GlobalItems.currencyRateList[1].ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                Console.WriteLine("4. Cancel");
                string userChoice = Console.ReadLine();
                int userInput = int.Parse(userChoice);
                int _user_id = userIndex.id;
                {
                    switch (userInput)
                    {

                        case 1:
                            Console.WriteLine("Case 1");
                            Console.WriteLine("Create checking account");
                            string _name = "Checking account";

                            AccountModel newAccount = new AccountModel
                            {
                                name = _name,
                                user_id = _user_id
                            };
                            DBAccess.SaveNewAccount(newAccount);

                            foreach (AccountModel account in userIndex.accounts)
                            {
                                Console.WriteLine($"Account name: {account.name} Balance: {account.balance}");
                                Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                            }
                            bool inputCheck = true;
                            do
                            {
                                Console.WriteLine("\nPress [Enter] to go back to main menu.");
                                ConsoleKeyInfo info = Console.ReadKey(); // Kollar om användaren trycker ner ENTER-knappen
                                if (info.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    inputCheck = false;
                                }
                                else Console.WriteLine("Invalid input.");
                            } while (inputCheck == true);
                            createAccRunning = false;

                            return;
                        case 2:

                            Console.WriteLine("case 2");
                            Console.WriteLine($"Savings account (Interest rate: {interestRate}");
                            string savings_name = "Sparkonto";
                            
                            AccountModel newSavingsAccount = new AccountModel
                            {
                                name = savings_name,
                                user_id = _user_id,
                                interest_rate = interestRate

                            };
                            DBAccess.SaveNewAccount(newSavingsAccount);
                            foreach (AccountModel account in userIndex.accounts)
                            {
                                Console.WriteLine($"Account name: {account.name} Balance: {account.balance}");
                                Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                            }
                            inputCheck = true;
                            do
                            {
                                Console.WriteLine("\nPress [Enter] to go back to main menu.");

                                ConsoleKeyInfo info = Console.ReadKey(); // Kollar om användaren trycker ner ENTER-knappen
                                if (info.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    inputCheck = false;

                                }
                                else Console.WriteLine("Invalid input.");
                            } while (inputCheck == true);
                            createAccRunning = false;
                            return;

                        case 3:
                            Console.WriteLine("case 3");
                            Console.WriteLine("Create a currency account ");
                            string currency_name = "Currency account";
                            int currencyID = 2;
                            AccountModel newCurrencyAccount = new AccountModel
                            {
                                name = currency_name,
                                user_id = _user_id,
                                currency_id = currencyID

                            };
                            DBAccess.SaveNewAccount(newCurrencyAccount);
                            Console.WriteLine("New currency account created:\nBalance: ");
                            inputCheck = true;
                            do
                            {
                                Console.WriteLine("\nPress [Enter] to go back to main menu.");

                                ConsoleKeyInfo info = Console.ReadKey(); // Kollar om användaren trycker ner ENTER-knappen
                                if (info.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    inputCheck = false;

                                }
                                else Console.WriteLine("Invalid input.");
                            } while (inputCheck == true);
                            createAccRunning = false;
                            return;
                        case 4:
                            Console.WriteLine("Cancel.");
                            return;

                        default:
                            Console.WriteLine("Invalid input. Please input a number between 1 and 3.");
                            break;

                    }
                    Console.ReadKey();
                }

            }

        }

        static string convertCurrency(decimal amount, string currencyName)
        {
            /*
            Tar in SEK och konverterar till USD och vice versa
            10,35 sek = 1 usd
            returnera resultat för USD till SEK eller från SEK till USD

            Om det förs in 'sek' i 'currency' så blir formeln amount / 10,35. Exempel: 100 / 10,35 = 9,66. 
            Användaren som för över 100 sek till ett USD konto, USD kontot tar då emot 9,66 dollar.
            
            Om det förs in 'usd' i istället så blir formeln amount * 10,35 . Exempel: 100 * 10,35 = 1035. 
            Användaren som för över 100 dollar till SEK konto, SEK kontot tar då emot 1035 kr.
            */

            //decimal converted_USD_currency = Convert.ToDecimal(USD_currency);
            decimal usd = 10.35m;
            if (currencyName == "sek")
            {
                decimal result = amount / usd;
                return result.ToString();
            }
            else if (currencyName == "usd")
            {
                decimal result = amount * usd;
                return result.ToString();
            }
            else
            {
                decimal result = amount * 1;
                return result.ToString();
            }

        }
    }
}

