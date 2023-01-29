using Npgsql.Replication;
using System.Security;

namespace rabbit_bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    WelcomeRabbit();
                    Console.Write("\nPlease enter FirstName: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Please enter PinCode: ");
                    SecureString pin = HidePin();
                    string pinCode = new System.Net.NetworkCredential(String.Empty, pin).Password;
                    Console.WriteLine();

                    int inputPIN = 0;
                    bool success = int.TryParse(pinCode, out inputPIN);
                    if (success)
                    {
                        Login.LoginTry(firstName, inputPIN);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nOgiltigt val. Var god och ange heltal endast!\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Tryck enter för att fortsätta.");
                        Console.ReadKey();
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("ERROR, please try again.");
                }

            }
            



            //Moved below code to Login class by Danilo

            //bool loggedIn = true;
            //while (loggedIn)
            //{
            //    Console.Write("Please enter FirstName: ");
            //    string firstName = Console.ReadLine();

            //    Console.Write("Please enter PinCode: ");
            //    int pinCode = int.Parse(Console.ReadLine());
            //    List<UserModel> checkedUsers = DBdataAccess.CheckLogin(firstName, pinCode);
            //    if (checkedUsers.Count < 1)
            //    {
            //        Console.WriteLine("Login failed, please try again");
            //        continue;
            //    }
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
                //foreach (UserModel user in checkedUsers)
                //{

                //    if (user.is_admin)
                //    {
                //        AdminLoginMenu();
                //    }

                //    else
                //    {
                //        UserLoginMenu();
                //    }


                //    user.accounts = DBdataAccess.GetUserAccounts(user.id);
                //    Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                //    Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                //    Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                //    Console.WriteLine($"User account list length: {user.accounts}");
                //    if (user.accounts.Count > 0)
                //    {
                //        foreach (AccountModel account in user.accounts)
                //        {
                //            Console.WriteLine($"ID: {account.id} Account name: {account.name} Balance: {account.balance}");
                //            Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                //        }
                //    }

                //}

            //}
        }



        //Moved UserLoginMenu() to Login class


        //static void UserLoginMenu()
        //{
        //    bool loggedIn = true;
        //    while (loggedIn)
        //    {
        //        Console.WriteLine("Make your choice with 1-6");
        //        Console.WriteLine("1. See your accounts and balances\n2. Transfer money\n3. Add a new account\n4. Make a bank loan\n5. Transaction history\n6. Log out");
        //        string userChoice = Console.ReadLine();
        //        switch (userChoice)
        //        {
        //            case "1":
        //                Console.WriteLine("");
        //                //Todo: skapa funktion för att visa konton. showAccounts();
        //                break;

        //            case "2":
        //                Console.WriteLine("");
        //                //ToDo: skapa funktion för att föra över pengar till eget och andras konton: transferMoney();
        //                break;
        //            case "3":
        //                Console.WriteLine("");
        //                //ToDo: skapa funktion för användare att lägga till nytt konto (list med olika konton: ();
        //                //ToDo: sparkonto med ränta
        //                break;

        //            case "4":
        //                // ToDo: skapa ett valutakonto i annan valuta än SEK.
        //                Console.WriteLine("");
        //                break;
        //            case "5":
        //                //ToDo: skapa en funktion för användare att låna pengar
        //                Console.WriteLine("");
        //                break;

        //            case "6":
        //                // ToDo: Skapa en funktion för användare att se överföringshistorik
        //                Console.WriteLine("");
        //                break;

        //            default:
        //                Console.WriteLine("");
        //                continue;
        //        }
        //    }
        //}



        //Moved AdminLoginMenu() to Login class

        //static void AdminLoginMenu()
        //{
        //    bool loggedIn = true;
        //    while (loggedIn)
        //    {
        //        Console.WriteLine("Make your choice with 1-8");
        //        Console.WriteLine("1. See your accounts and balances\n2. Transfer money\n3. Add a new account\n4. Make a bank loan\n5. Transaction history\n6. Create new user\n7. Set exchange rate\n8. Log out");
        //        string userChoice = Console.ReadLine();
        //        switch (userChoice)
        //        {
        //            case "1":
        //                Console.WriteLine("");
        //                //Todo: skapa funktion för att visa konton. showAccounts();
        //                break;

        //            case "2":
        //                Console.WriteLine("");
        //                //ToDo: skapa funktion för att föra över pengar till eget och andras konton: transferMoney();
        //                break;

        //            case "3":
        //                Console.WriteLine("");
        //                //ToDo: skapa funktion för användare att lägga till nytt konto (list med olika konton: ();
        //                //ToDo: sparkonto med ränta
        //                break;

        //            case "4":
        //                // ToDo: skapa ett valutakonto i annan valuta än SEK.
        //                Console.WriteLine("");
        //                break;

        //            case "5":
        //                //ToDo: skapa en funktion för användare att låna pengar
        //                Console.WriteLine("");
        //                break;

        //            case "6":
        //                // ToDo: Skapa en funktion för användare att se överföringshistorik
        //                Console.WriteLine("");
        //                break;

        //            case "7":
        //                // ToDo: Skapa en funktion för admin att skapa nya användare i systemet
        //                Console.WriteLine("");
        //                break;

        //            case "8":
        //                // ToDo: Skapa en funktion för admin att sätta dagens växelkurs
        //                Console.WriteLine("");
        //                break;

        //            default:
        //                Console.WriteLine("");
        //                continue;
        //        }
        //    }

        //}


        static void WelcomeRabbit()
        {
            string rabbit = @"                                                                                                    
                                 ..::^~~!!!!!~~~^^.                                         
                          .:~!???77~~JGGGGGPGPGGGGPP?.          .^7!
                      :~7?7JPPGPY?!..JGGPPPPP5GGGGBBP         .~?Y5?
                   :7YPGG5?75PGGPY!:.7PGGPGP55GPGBBBJ..     .!7?JY?^
                ^?5GGGGGPPP??PGGG5?^ ~5GGGGP5PGGGBBB?:. .:^~~7JYJ~
              ^5GBGBGGGGGP55?J5PP5Y!.^?PGPPPPPPGBBBB7.:^!!~!?YYY^
              :5BGGBGGGPGGP55JJ5PPPJ~.!5GPPPPPGBBBBG7~!!^~?JJ?!7^
               .?GGGGGPPPGPP55JYPPP5!:~?PGPPPPGBBBG57~::!JYJ!^^!
                 ~GGGGGGPPPP55Y?YPP5J~^75GGPPGBBBPY7^.:!YY?!~:^~
                  ~PGGGGGPGPP55J?5GP57^~JGGGPGGBGJ~^.:~J5J77^^!^                      
                   ^PGGGGPGPPPP5JYGGPY~^7PGGGGGPJ?^.:^?5Y?7!^!!^
                    ~GGGGGGGGPP5YJYPGPJ^!5GGGGPY?^.:^~Y5Y?7~~7^^
                     ?GGGGGGBGGPG5YPP5JJY5PPBGYJ7^~~!!55Y??7JG5?~^^
                      YBBBBBGGP5YJ!!?YJ?JPGBBP57~~7~~!5JJ???PBP557^^
                      ~GGGGP555YJ?!7JJ!~?PGBBPY777!^!?Y77??5PYJPPP!^^
                       YGGBGPPPP5YYYPPY!!5GGP5J7~~:~!?JJ!??GPPGGGY~^
                    .^!JGBBBGGGP5PBBBGP55YJ?J?7~~~~~!7?7?JGGBBB57^^^
                 :!J5GBGGBBBGPPGB##BG5YJ?7~!7!!~~!^~!7?7?YGG5J!^^^^
               :!JYPGGBGGPP55GBBBG5J?77~^^^:^~~~^!!~!!7!7?J57^^^^
              ^Y5Y??JY5555PGBBGPY?7!^^::^^^:.::~7!!!^~~!!7?YY~^^^
              75PG5YJJY5GBBGPYJ7!!^^..^^^~7??!::~^^^^~!!!77YY7~^^
              !JPPPGGBBBG5YJ?7!!^~^^^^~^?PPPPY^:^^^.:^~~~7?JY!^^
                ^!77??7YYJ?7!~!:::::^~!7?Y5PY!^..:^^^!~!!7?J?^:^
                      ^JJ7~^~^^::^::^!~^^^^^^::. :^~!!!?!7JY7^^^
                      7Y?!^^^^:::::::::^^^::::. :^~7?7?YJYJY7~^^
                    .:YJ7~~^^::^:......:~!~^^..:~!7???7!~^::~
                     ~5J7~^^!~:..:^^:...:~77!~~!77~::...  :?!^^^
                     75J?7~7?7:.::^!?!^^^^7?J????.   .:^:~7JY??^^
                     !Y5YYYY!~^^^:^!?J?!!~7??JJ?: .:.^7:?~J^??5Y!
                     :7YYYY?7!!~!~7??YJ?????JJY7^^?JJY^~7!?~?Y5YP^
                       ~YP5YJJ??JJ?JYYYYYYJJJJYJ7?7!Y7 !:^.^?J!YG?
                       ^!^7Y?YJYY5YYYJ?J?JJJ???7!7?J5^.^.!^J?!YGPP!
                         ^ 7??77!7JYY???7?7^^^^:!77Y!:777YJ7!Y5J7JGY!^";
            Console.WriteLine(rabbit);
            //Original image: "Regency Rabbit", drawing by Adam Zebediah Joseph.
            Console.WriteLine("                                    .:Welcome to:.");
            string prompt = @"
██████╗  █████╗ ██████╗ ██████╗ ██╗████████╗    ██████╗  █████╗ ███╗   ██╗██╗  ██╗
██╔══██╗██╔══██╗██╔══██╗██╔══██╗██║╚══██╔══╝    ██╔══██╗██╔══██╗████╗  ██║██║ ██╔╝
██████╔╝███████║██████╔╝██████╔╝██║   ██║       ██████╔╝███████║██╔██╗ ██║█████╔╝ 
██╔══██╗██╔══██║██╔══██╗██╔══██╗██║   ██║       ██╔══██╗██╔══██║██║╚██╗██║██╔═██╗ 
██║  ██║██║  ██║██████╔╝██████╔╝██║   ██║       ██████╔╝██║  ██║██║ ╚████║██║  ██╗
╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ╚═════╝ ╚═╝   ╚═╝       ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝";
            Console.WriteLine(prompt);
            Console.WriteLine("                         .:The most rabid bank in the world:.");

        }

        static SecureString HidePin()
        {
            SecureString pin = new SecureString();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pin.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin.RemoveAt(pin.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                return pin;
            }
        }

    }
}