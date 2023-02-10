using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Npgsql.Replication;

namespace rabbit_bank
{
    public class General
    {
        public static void app()
        {
            bool isRunning = true;
            while (isRunning)
            {
                WelcomeRabbit();
                List<UserModel> users = DBAccess.LoadBankUsers();
                Console.WriteLine($"users length: {users.Count}");
                foreach (UserModel user in users)
                {
                    Console.WriteLine($"Existing user id: {user.id} name: {user.first_name} with pincode: {user.pin_code}, account lock:{user.blocked_user}, attempts left: {user.attempts}, admin = {user.role_id}");
                }
                try
                {
                    Console.Write("\nPlease enter id: ");
                    int ident = int.Parse(Console.ReadLine());

                    Console.Write("Please enter PinCode: ");
                    SecureString pin = HidePin();
                    string pinCode = new System.Net.NetworkCredential(String.Empty, pin).Password;
                    Console.WriteLine();

                    int inputPIN = 0;
                    bool success = int.TryParse(pinCode, out inputPIN);
                    if (success)
                    {
                        Login.LoginTry(ident, inputPIN);
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("ERROR, try again");
                    Console.ReadLine();
                }
                
            }

        }

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
            //Console.WriteLine("                                    .:Welcome to:.");
            string prompt = @"
██████╗  █████╗ ██████╗ ██████╗ ██╗████████╗    ██████╗  █████╗ ███╗   ██╗██╗  ██╗
██╔══██╗██╔══██╗██╔══██╗██╔══██╗██║╚══██╔══╝    ██╔══██╗██╔══██╗████╗  ██║██║ ██╔╝
██████╔╝███████║██████╔╝██████╔╝██║   ██║       ██████╔╝███████║██╔██╗ ██║█████╔╝ 
██╔══██╗██╔══██║██╔══██╗██╔══██╗██║   ██║       ██╔══██╗██╔══██║██║╚██╗██║██╔═██╗ 
██║  ██║██║  ██║██████╔╝██████╔╝██║   ██║       ██████╔╝██║  ██║██║ ╚████║██║  ██╗
╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ╚═════╝ ╚═╝   ╚═╝       ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝";
            Console.WriteLine(prompt);
            Console.WriteLine("                         .:The most rabid bank in the world:.");
            DBAccess.updateBlockedUser();
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
