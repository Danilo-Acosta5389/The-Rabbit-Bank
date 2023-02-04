using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public static class globalItems
    {
        public static int attempts = 3;
    }
    public class Login
    {
        public static void LoginTry(string first_Name, int pin_Code)
        {
            //string nameToLower = first_Name.ToLower();
            TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
            string capInput = currentTextInfo.ToTitleCase(first_Name.ToLower());
            //Above is converting firstName input to match the way it is capitalized in DB.

            bool loginRunning = true;
            while (loginRunning)
            {
                List<UserModel> checkedUsers = DBAccess.CheckLogin(capInput, pin_Code);
                Console.WriteLine();
                if (checkedUsers.Count < 1)
                {
                    Console.WriteLine("Login failed, please try again");
                    loginRunning = false;
                    globalItems.attempts--;
                    break;
                }
                foreach (UserModel user in checkedUsers)
                {
                    if (user.locked_user == true)
                    {
                        Console.WriteLine("Your account is locked, contact a admin!");
                        Console.ReadLine();
                        loginRunning = false;
                        break;
                    }
                    else
                    {
                        globalItems.attempts = 3;
                        user.accounts = DBAccess.GetUserAccounts(user.id);
                        Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                        Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                        Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                        Console.WriteLine($"User account list length: {user.accounts.Count}");

                        if (user.accounts.Count > 0)
                        {
                            foreach (AccountModel account in user.accounts)
                            {
                                Console.WriteLine($"ID: {account.id} Account name: {account.name} Balance: {account.balance}");
                                Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                            }
                        }

                        Console.WriteLine();

                        if (user.is_admin)
                        {
                            AdminLoginMenu(user);
                        }
                        else if (user.is_client)
                        {
                            UserLoginMenu(user);
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
                        else if (yesNo.ToLower() == "n")
                        {
                            continue;
                        }
                    }
                }
            }
        }
        //TODO: Creates blocked user screen
        //static void UserBlockedScreen()
        //{
        //    Console.WriteLine("This account is blocket. Please Contact admin for help.");
        //}

        static void UserLoginMenu(UserModel userIndex)
        {
            bool loggedIn = true;
            while (loggedIn)
            {

                Console.WriteLine("Make your choice with 1-6");
                Console.WriteLine("1. See your accounts and balances [NOT WORKING]\n2. Transfer money [NOT WORKING]\n3. Add a new account [NOT WORKING]\n4. Make a bank loan [NOT WORKING]\n5. Transaction history [NOT WORKING]\n6. Log out [CURRENTLY WORKING]");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("");
                        //Todo: skapa funktion för att visa konton. showAccounts();
                        break;

                    case "2":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för att föra över pengar till eget och andras konton: transferMoney();
                        break;
                    case "3":
                        Console.WriteLine("");
                        Console.WriteLine("==========\nSkapa nytt konto\n========");
                        CreateAccount(userIndex);
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
                        // ToDo: Skapa en funktion för användare att se överföringshistorik
                        Console.WriteLine("");
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("");
                        continue;
                }
            }
        }


        static void AdminLoginMenu(UserModel userIndex)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("Make your choice with 1-8");
                Console.WriteLine("1. See your accounts and balances [NOT WORKING]\n2. Transfer money [NOT WORKING]\n3. Add a new account [NOT WORKING]\n4. Make a bank loan [NOT WORKING]\n5. Transaction history [NOT WORKING]\n6. Set exchange rate [NOT WORKING]\n7.  Create new user [IS KINDA WORKING]\n8. Log out [CURRENTLY WORKING]");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.WriteLine("");
                        //Todo: skapa funktion för att visa konton. showAccounts();
                        break;

                    case "2":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för att föra över pengar till eget och andras konton: transferMoney();
                        break;

                    case "3":
                        CreateAccount(userIndex);
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
                        // ToDo: Skapa en funktion för admin att skapa nya användare i systemet

                        Console.WriteLine("Create new user\n");
                        CreateUser();
                        break;

                    case "8":
                        // ToDo: Skapa en funktion för admin att sätta dagens växelkurs
                        Console.WriteLine("");
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("");
                        continue;
                }
            }
        }

        static void CreateUser()
        {
            Console.Write("Please enter FirstName: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter LastName: ");
            string lastName = Console.ReadLine();
            Console.Write("Please enter PinCode: ");
            string pinCode = Console.ReadLine();
            Console.Write("Enter roleId");
            int roleId = int.Parse(Console.ReadLine());
            Console.Write("Enter branchId");
            int branchId = int.Parse(Console.ReadLine());
            UserModel newUser = new UserModel
            {
                first_name = firstName,
                last_name = lastName,
                pin_code = pinCode,
                role_id = roleId,
                branch_id = branchId
            };
            DBAccess.SaveBankUser(newUser);
        }

        static void CreateAccount(UserModel userIndex)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            bool createAccRunning = true;
            while (createAccRunning)
            {
                double interestRate = 2.85;
                Console.WriteLine("==============================\nVälj vilket konto du vill skapa.\n==============================");
                Console.ResetColor();
                Console.WriteLine("1. Checking account. Sorry, no interest rate.");
                
                Console.WriteLine($"2. Savings account. Current interest rate: {interestRate}");
                Console.WriteLine($"3. Currency account (Currency USD || Exchange rate:");
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
    }
}
