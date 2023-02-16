using System.Globalization;

namespace rabbit_bank
{
    public class MenuOptions
    {

        public static void UserLoginMenu(UserModel userIndex)
        {
            Console.WriteLine($"Welcome back {userIndex.first_name}.");
            Console.WriteLine($"User ID: {userIndex.id}");
            Api.ImportUSDRate(); // Gets latest USD to SEK rate

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
                    "\n3. Add a new account" +
                    "\n4. Make a bank loan [NOT WORKING]" +
                    "\n5. Transaction history [NOT WORKING]" +
                    "\n6. Withdraw money " +
                    "\n7. Deposit money" +
                    "\n8. Logout");
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
                        WithDraw(userIndex.id);
                        Console.WriteLine("");
                        break;
                    case "7":
                        Deposit(userIndex.id);
                        break;
                    case "8":
                        //Logout
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a number between 1 and 6.");
                        continue;
                }
                Console.WriteLine("Please press ENTER to continue.");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
            }
        }


        public static void AdminLoginMenu(UserModel userIndex)
        {
            Console.WriteLine($"Welcome back {userIndex.first_name}.");
            Console.WriteLine($"User ID: {userIndex.id}");
            Console.WriteLine($"[Admin account]");
            Api.ImportUSDRate(); // Gets latest USD to SEK rate

            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(" Make your choice with 1-5 ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("1. Block/Unblock Users" +
                    "\n2. Set exchange rate " +
                    "\n3. Create new user" +
                    "\n4. Logout ");
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
                        // Lets admin change exchange rate for USD
                        MenuOptions.SetExchangeRate();

                        // ToDo: skapa ett valutakonto i annan valuta än SEK.
                        Console.WriteLine("");
                        break;

                    case "3":
                        MenuOptions.CreateNewUser();
                        break;

                    case "4":
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a number between 1 and 8.");
                        continue;
                }
            }
        }

        // Method for changing exchange rate
        public static void SetExchangeRate()
        {
            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Set exchange rate on USD ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Choose new exchange rate for USD.");
            decimal userInput = decimal.Parse(Console.ReadLine());
            DBAccess.UpdateExchangeRate(userInput);
            List<AccountModel> accounts = DBAccess.exchangeR();
            foreach (AccountModel ex in accounts)
            {
                Console.WriteLine($"exchange rate for {ex.name}: {ex.exchange_rate}");
            }
            Console.ReadLine();
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

                    var tempIDlist = GlobalItems.accountsList;
                    var tempCurrList = GlobalItems.currencyNameList;
                    tempCurrList.Clear();
                    tempIDlist.Clear();

                    for (int i = 0; i < userIndex.accounts.Count; i++)
                    {
                        //GlobalItems.accountNameList.Add(tempAccount[i].name);
                        tempIDlist.Add(userIndex.accounts[i].id);
                        tempCurrList.Add(userIndex.accounts[i].currency_name);
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

                    int lastOnList = tempIDlist.Last();

                    for (int i = 0; i < tempIDlist.Count; i++)
                    {
                        //Console.WriteLine(tempList[i]);
                        if (fromAccount == tempIDlist[i])
                        {
                            //Console.WriteLine("{0} = {1}", fromAccount, tempList[i]);
                            //Console.WriteLine("WOHOO FOUND IT");
                            break;
                        }
                        else if (lastOnList == tempIDlist[i])
                        {
                            Console.WriteLine("\nError. Invalid account number/ID.");
                            return false;
                        }
                    }



                    Console.WriteLine("\nPlease input RECIEVING account number/ID");
                    Console.Write("Account numeber/ID here --> ");
                    int toAccount = int.Parse(Console.ReadLine());

                    var getAllAccounts = GlobalItems.globalAccountsList;

                    int lastOnList2 = tempIDlist.Last();

                    for (int i = 0; i < getAllAccounts.Count; i++)
                    {
                        //Console.WriteLine(tempList[i]);
                        if (toAccount == getAllAccounts[i].id)
                        {
                            //Console.WriteLine("{0} = {1}", fromAccount, tempList[i]);
                            //Console.WriteLine("WOHOO FOUND IT");
                            break;
                        }
                        else if (lastOnList2 == getAllAccounts[i].id)
                        {

                            Console.WriteLine("\nError. Invalid account number/ID.");
                            return false;
                        }
                    }


                    Console.Write("\nPlease input amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());



                    // ==========BELOW IS THE ALGORITM THAT WILL CONVERT CURRENCY FROM ACCOUNTS ON TRANSFER========


                    int currencyID = 0;

                    for (int i = 0; i < tempIDlist.Count; i++)
                    {
                        //Console.WriteLine(tempIDlist[i]);      //38, 40, 41, 42, 45
                        //Console.WriteLine(tempCurrList[i]); // SEK, SEK, USD, USD, SEK
                        //Console.WriteLine();

                        //Below loops and if-statemnts check if the 'from' account has SEK or USD currency and then the same with 'To' account.
                        //A conversion is being made if the from account has SEK and the recieving has USD

                        if (fromAccount == tempIDlist[i] && tempCurrList[i] == "SEK")
                        {
                            //Console.WriteLine("From account is swedish");
                            currencyID = 1;

                            //for (int j = 0; j < getAllAccounts.Count; j++)
                            //{
                            //    if (toAccount == getAllAccounts[j].id && getAllAccounts[j].currency_name == "SEK")
                            //    {
                            //        //Console.WriteLine("To account is swedish");
                            //    }
                            //    else if (toAccount == getAllAccounts[j].id && getAllAccounts[j].currency_name == "USD")
                            //    {
                            //Console.WriteLine("To account is american");
                            //Console.WriteLine($"{convertCurrency(amount, "sek")}");
                            //amount = Convert.ToDecimal(convertCurrency(amount, "sek"));
                            //Console.WriteLine("LOOOK HERE! USD");
                            //}
                            //}

                        }
                        else if (fromAccount == tempIDlist[i] && tempCurrList[i] == "USD")
                        {
                            //Console.WriteLine("From account is american");
                            //Console.WriteLine();
                            currencyID = 2;

                            //for (int j = 0; j < tempIDlist.Count; j++)
                            //{
                            //    if (toAccount == getAllAccounts[j].id && getAllAccounts[j].currency_name == "SEK")
                            //    {
                            //Console.WriteLine("To account is swedish");
                            //Console.WriteLine($"{convertCurrency(amount, "usd")}");
                            //amount = Convert.ToDecimal(convertCurrency(amount, "usd"));
                            //Console.WriteLine("LOOK HERE!! SEK");
                            //}
                            //else if (toAccount == getAllAccounts[j].id && getAllAccounts[j].currency_name == "USD")
                            //{
                            //    //Console.WriteLine("To account is american");
                            //}
                            //}
                        }

                    }
                    decimal newAmount = Math.Round(amount, 2);
                    //Console.WriteLine("HERE AGAIN");
                    //Console.WriteLine(newAmount);
                    Console.WriteLine();


                    Console.WriteLine($"\nFrom account number/ID: {fromAccount}");
                    Console.WriteLine($"To account number/ID: {toAccount}");
                    Console.WriteLine($"Amount: {newAmount}");






                    //if (GlobalItems.currencyNameList[0] == "SEK")
                    //{
                    //    Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                    //}
                    //else if (GlobalItems.currencyNameList[1] == "USD")
                    //{
                    //    Console.WriteLine($"Amount: {amount.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                    //}



                    //====================BELOW IS JUST A PRETTIER NUMBER PRESENTATION FOR THE USER============!!!


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
                        return DBAccess.TransferMoney(userIndex.id, 0, currencyID, fromAccount, toAccount, amount); // NEW

                        //return DBAccess.TransferMoney(userIndex.id, 0, fromAccount, toAccount, newAmount); OLD
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

        public static bool TransferOwnAccounts(UserModel userIndex)  // TRANSFER MONEY BETWEEN USERS OWN ACCOUNTS
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

                    //Below, two lists are declared, one containing account id's and the other currency name (eg. SEK and USD). The very same lists are also cleared on the next lines
                    var tempIDlist = GlobalItems.accountsList;
                    tempIDlist.Clear();
                    var tempCurrList = GlobalItems.currencyNameList;
                    tempCurrList.Clear();

                    // Below we iterate through the logged in persons bank accounts in DB
                    //Each account id and currency name is added to the Lists. Also, the list indexes are showed to the user.

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
                    int fromAccount = int.Parse(Console.ReadLine()); //HERE the user inputs from wich account ID

                    //the for loop below checks if the ID exist withing this users accounts
                    //If not, the user gets thrown out one step.

                    int lastOnList = tempIDlist.Last();
                    for (int i = 0; i < tempIDlist.Count; i++)
                    {
                        //Console.WriteLine(tempIDlist[i]);
                        if (fromAccount == tempIDlist[i])
                        {
                            //Console.WriteLine("{0} = {1}", fromAccount, tempIDlist[i]);
                            //Console.WriteLine("WOHOO FOUND IT");
                            break;
                        }
                        else if (lastOnList == tempIDlist[i])
                        {
                            Console.WriteLine("\nError. Invalid account number/ID.");
                            return false;
                        }
                    }

                    Console.Write("Please input to account: "); //The same procidure as above is done in 'To' account
                    int toAccount = int.Parse(Console.ReadLine());

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
                            Console.WriteLine("\nError. Invalid account number/ID.");
                            return false;
                        }
                    }

                    // ==========BELOW IS THE ALGORITM THAT WILL CONVERT CURRENCY FROM ACCOUNTS ON TRANSFER========

                    Console.Write("Please input amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    //Below loops and if-statemnts check if the 'from' account has SEK or USD currency and then the same with 'To' account.
                    //A conversion is being made if the from account has SEK and the recieving has USD

                    int currencyID = 0; //This will determine if FROM SEK or FROM USD 

                    for (int i = 0; i < tempCurrList.Count; i++)
                    {
                        //Console.WriteLine(tempIDlist[i]);      //38, 40, 41, 42, 45
                        //Console.WriteLine(tempCurrList[i]); // SEK, SEK, USD, USD, SEK
                        //Console.WriteLine();


                        if (fromAccount == tempIDlist[i] && tempCurrList[i] == "SEK")
                        {
                            Console.WriteLine("From account is swedish");
                            currencyID = 1;

                            //for (int j = 0; j < tempIDlist.Count; j++)
                            //{
                            //    if (toAccount == tempIDlist[j] && tempCurrList[j] == "SEK")
                            //    {
                            //        Console.WriteLine("To account is swedish");
                            //    }
                            //    else if (toAccount == tempIDlist[j] && tempCurrList[j] == "USD")
                            //    {
                            //Console.WriteLine("To account is american");
                            //Console.WriteLine($"{convertCurrency(amount, "in sek")}");
                            //amount = Convert.ToDecimal(convertCurrency(amount, "sek"));
                            //Console.WriteLine("LOOOK HERE! USD");
                            //}
                            //}

                        }
                        else if (fromAccount == tempIDlist[i] && tempCurrList[i] == "USD")
                        {
                            Console.WriteLine("From account is american");
                            //Console.WriteLine();
                            currencyID = 2;

                            //for (int j = 0; j < tempIDlist.Count; j++)
                            //{
                            //    if (toAccount == tempIDlist[j] && tempCurrList[j] == "SEK")
                            //    {
                            //Console.WriteLine("To account is swedish");
                            //Console.WriteLine($"{convertCurrency(amount, "in usd")}");
                            //amount = Convert.ToDecimal(convertCurrency(amount, "usd"));
                            //Console.WriteLine("LOOK HERE!! SEK");
                            //    }
                            //    else if (toAccount == tempIDlist[j] && tempCurrList[j] == "USD")
                            //    {
                            //        Console.WriteLine("To account is american");
                            //    }
                            //}
                        }
                    }

                    //decimal newAmount = Math.Round(amount, 2);
                    //Console.WriteLine();
                    //Console.WriteLine(Math.Round(amount, 2) + "Will be transfered");
                    //Console.WriteLine();
                    return DBAccess.TransferMoney(userIndex.id, userIndex.id, currencyID, fromAccount, toAccount, amount); // NEW

                    //return DBAccess.TransferMoney(userIndex.id, userIndex.id, fromAccount, toAccount, amount);  OLD
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
            //List<AccountModel> tempAccount = userIndex.accounts;
            int counter = 1;
            foreach (AccountModel account in userIndex.accounts) // This is used to iterate through the logged in persons bank_account in DB
            {
                //List<AccountModel> tempAccount = userIndex.accounts;
                Console.WriteLine($"{counter}. {account.name}");
                Console.WriteLine($"Account nummber/ID: {account.id}");
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
            //Console.WriteLine("Please press ENTER to continue.");
            //while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }


        public static void CreateAccount(UserModel userIndex)
        {
            //Console.Clear();
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




        public static void WithDraw(int user_id)
        {
            //visar anv konton och saldon
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Withdraw ");
            Console.ResetColor();
            Console.WriteLine("\n");

            List<AccountModel> userAccounts = DBAccess.GetUserAccounts(user_id);

            for (int i = 0; i < userAccounts.Count; i++)
            {

                Console.WriteLine($"{i + 1}. {userAccounts[i].name}");

                if (userAccounts[i].currency_name == "SEK")
                {
                    Console.WriteLine($"Balance: {userAccounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                }
                else if (userAccounts[i].currency_name == "USD")
                {
                    Console.WriteLine($"Balance: {userAccounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                }
                Console.WriteLine("============================================================");
            }


            Console.Write("Which account do you want to withdraw from? :");
            int account = int.Parse(Console.ReadLine());


            account -= 1;
            Console.WriteLine($"You chose : {userAccounts[account].name} ");


            Console.Write("How much would you like to withdraw? :");
            decimal.TryParse(Console.ReadLine(), out decimal amount);


            if (amount <= 0)
            {
                Console.WriteLine("You cannot withdraw a negative amount.");
            }
            else if (userAccounts[account].balance < amount)
            {
                Console.WriteLine("\nYou don't have enough money on your account");
            }
            else
            {
                decimal newBalance = userAccounts[account].balance -= amount;
                Console.WriteLine($"\nYour balance is now: {newBalance} on account: {userAccounts[account].name} ");
                DBAccess.UpdateAccount(user_id, userAccounts[account].id, newBalance);

            }

        }

        public static void Deposit(int user_id)
        {
            //visar anv konton och saldon
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Deposit ");
            Console.ResetColor();
            Console.WriteLine("\n");

            List<AccountModel> userAccounts = DBAccess.GetUserAccounts(user_id);

            for (int i = 0; i < userAccounts.Count; i++)
            {

                Console.WriteLine($"{i + 1}. {userAccounts[i].name}");

                if (userAccounts[i].currency_name == "SEK")
                {
                    Console.WriteLine($"Balance: {userAccounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("sv-SE"))}");
                }
                else if (userAccounts[i].currency_name == "USD")
                {
                    Console.WriteLine($"Balance: {userAccounts[i].balance.ToString("C2", CultureInfo.GetCultureInfo("chr-Cher-US"))}");
                }
                Console.WriteLine("============================================================");
            }


            Console.Write("Which account do you want to deposit to? :");
            int account = int.Parse(Console.ReadLine());


            account -= 1;
            Console.WriteLine($"You chose : {userAccounts[account].name} ");


            Console.Write("How much would you like to deposit? :");
            decimal.TryParse(Console.ReadLine(), out decimal amount);


            if (amount <= 0)
            {
                Console.WriteLine("You cannot deposit a negative amount.");
            }
            //else if (userAccounts[account].balance < amount)
            //{
            //    Console.WriteLine("\nYou don't have enough money on your account");
            //}
            else
            {
                decimal newBalance = userAccounts[account].balance += amount;
                Console.WriteLine($"\nYour balance is now: {newBalance} on account: {userAccounts[account].name} ");
                DBAccess.UpdateAccount(user_id, userAccounts[account].id, newBalance);

            }

        }
    }
}


