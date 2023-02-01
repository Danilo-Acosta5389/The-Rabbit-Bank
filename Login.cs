using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public static class GlobalItems
    {
        public static int attempts = 3;
        public static TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
    }
    public class Login
    {
        public static void LoginTry(string first_Name, int pin_Code) //Arguments store user input from login screen
        {

            string capInput = GlobalItems.currentTextInfo.ToTitleCase(first_Name.ToLower());
            //Above is converting firstName input to match the way it is capitalized in DB.

            bool loginRunning = true;
            while (loginRunning)
            {
                Console.Clear();
                List<UserModel> checkedUsers = DBAccess.CheckLogin(capInput, pin_Code); //User input from log in screen is passed in here.
                                                                                        //CheckLogin checks if input name and pin matches with bank_user in DB
                Console.WriteLine();
                if (checkedUsers.Count < 1)
                {
                    Console.WriteLine("Login failed, please try again");
                    loginRunning = false;
                    GlobalItems.attempts--;
                    break;
                }
                foreach (UserModel user in checkedUsers) //This itarates through all data in DB that matches UserModel class(Look in UserModel.cs). 
                { //user is the variable that is used to indikate index
                    if (user.locked_user == true)
                    {
                        Console.WriteLine("Your account is locked, contact a admin!");
                        Console.ReadLine();
                        loginRunning = false;
                        break;
                    }
                    else
                    {
                        GlobalItems.attempts = 3;
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
                            AdminLoginMenu(user);  //the variable user is passed in here
                            loginRunning = false;
                        }
                        else if (user.is_client)
                        {

                            UserLoginMenu(user); //the variable user is passed in here
                            loginRunning = false;

                        }
                        //Console.Write("Do you wish to exit? Y/N --> ");
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
        //TODO: Creates blocked user screen
        //static void UserBlockedScreen()
        //{
        //    Console.WriteLine("This account is blocket. Please Contact admin for help.");
        //}

        static void UserLoginMenu(UserModel userIndex) // the user variable is stored in userIndex
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

                Console.WriteLine("1. See your accounts and balances [WORKING]\n2. Transfer money [NOT WORKING]\n3. Add a new account [NOT WORKING]\n4. Make a bank loan [NOT WORKING]\n5. Transaction history [NOT WORKING]\n6. Log out [WORKING]");
                Console.Write("--> ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        AccountsAndBalances(userIndex);
                        break;

                    case "2":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för att föra över pengar till eget och andras konton: transferMoney();
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
                        // Log out user;
                        Console.WriteLine("");
                        loggedIn = false;
                        break;

                    default:
                        Console.WriteLine("");
                        continue;
                }
            }
        }


        static void AdminLoginMenu(UserModel userIndex) // the user variable is stored in userIndex
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

                Console.WriteLine("1. See accounts and balances \n2. Transfer money [NOT WORKING]\n3. Add a new account [NOT WORKING]\n4. Make a bank loan [NOT WORKING]\n5. Transaction history [NOT WORKING]\n6. Set exchange rate [NOT WORKING]\n7. Create new user \n8. Log out ");
                Console.Write("--> ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        AccountsAndBalances(userIndex);
                        break;

                    case "2":
                        Console.WriteLine("");
                        //ToDo: skapa funktion för att föra över pengar till eget och andras konton: transferMoney();
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
                        // Create new user here
                        CreateUser();
                        break;

                    case "8":
                        // Log out user
                        Console.Clear();
                        Console.WriteLine("Logging out now.");
                        Thread.Sleep(1000);
                        Console.WriteLine("\nThank you for using Rabbit Bank services.");
                        Thread.Sleep(1000);
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

                    Console.Write("Please enter PinCode: ");
                    string pinCode = Console.ReadLine();

                    Console.Write("Enter Role Id: ");
                    int roleId = int.Parse(Console.ReadLine());

                    Console.Write("Enter branchId: ");
                    int branchId = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine($"FirstName: {capLastName}");
                    Console.WriteLine($"LastName: {capFirstName}");
                    Console.WriteLine($"Pin: {pinCode}");
                    Console.WriteLine($"Role ID: {roleId}");
                    Console.WriteLine($"Branch ID: {branchId}");
                    Console.Write("\nIs this correct? Y/N --> ");
                    string yesNo = Console.ReadLine();
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
                    string yesNo = Console.ReadLine();
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

        //public static List<AccountModel> GetUserAccounts(int user_id)
        //{
        //    using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        //    {

        //        var output = cnn.Query<AccountModel>($"SELECT bank_account.*, bank_currency.name AS currency_name, bank_currency.exchange_rate AS currency_exchange_rate FROM bank_account, bank_currency WHERE user_id = '{user_id}' AND bank_account.currency_id = bank_currency.id", new DynamicParameters());
        //        //Console.WriteLine(output);
        //        return output.ToList();
        //    }
        //}
        //public NpgsqlConnection(string connectionString)//Initializes a new instance of NpgsqlConnection with the given connection string.

        //Todo: Put See account and balances in a method

        static void AccountsAndBalances(UserModel userIndex)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" Accounts and balances ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

            foreach (AccountModel account in userIndex.accounts) // This is used to iterate through the logged in persons bank_account in DB
            {
                Console.WriteLine($"{account.name}" +
                    $"\nBalance: {account.balance} {account.currency_name}");
                Console.WriteLine();
                //Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
            }
            Console.WriteLine("Please press ENTER to continue.");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }

    }
}
        