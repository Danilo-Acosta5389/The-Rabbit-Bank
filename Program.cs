namespace rabbit_bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to\nThe Rabbit Bank");
            List<BankUserModel> users = DBdataAccess.OldLoadBankUsers();
            Console.WriteLine($"users length: {users.Count}");
            foreach (BankUserModel user in users)
            {
                Console.WriteLine($"Hello {user.first_name} your pincode is {user.pin_code}");
            }
            Console.ReadKey();
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Write("Please enter FirstName: ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter PinCode: ");
                int pinCode = int.Parse(Console.ReadLine());
                List<BankUserModel> checkedUsers = DBdataAccess.CheckLogin(firstName, pinCode);
                if (checkedUsers.Count < 1)
                {
                    Console.WriteLine("Login failed, please try again");
                    continue;
                }
                //foreach (BankUserModel user in users)
                //{
                //    if (user.is_admin)
                //    {
                //        AdminLoginMenu();
                //    }

                //    else
                //    {
                //        UserLoginMenu();
                //    }
                //}
                foreach (BankUserModel user in checkedUsers)
                {

                    if (user.is_admin)
                    {
                        AdminLoginMenu();
                    }

                    else
                    {
                        UserLoginMenu();
                    }


                    user.accounts = DBdataAccess.GetUserAccounts(user.id);
                    Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                    Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                    Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                    Console.WriteLine($"User account list length: {user.accounts}");
                    if (user.accounts.Count > 0)
                    {
                        foreach (BankAccountModel account in user.accounts)
                        {
                            Console.WriteLine($"ID: {account.id} Account name: {account.name} Balance: {account.balance}");
                            Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                        }
                    }

                }



            }
        }

        static void UserLoginMenu()

        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("Make your choice with 1-6");
                Console.WriteLine("1. See your accounts and balances\n2. Transfer money\n3. Add a new account\n4. Make a bank loan\n5. Transaction history\n6. Log out");
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

                    default:
                        Console.WriteLine("");
                        continue;
                }
            }

        }

        static void AdminLoginMenu()

        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("Make your choice with 1-8");
                Console.WriteLine("1. See your accounts and balances\n2. Transfer money\n3. Add a new account\n4. Make a bank loan\n5. Transaction history\n6. Create new user\n7. Set exchange rate\n8. Log out");
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
                        Console.WriteLine("");
                        break;

                    case "8":
                        // ToDo: Skapa en funktion för admin att sätta dagens växelkurs
                        Console.WriteLine("");
                        break;

                    default:
                        Console.WriteLine("");
                        continue;
                }
            }

        }
    }
}