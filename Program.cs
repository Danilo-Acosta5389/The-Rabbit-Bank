using Npgsql.Replication;
using System.Security;

namespace rabbit_bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeRabbit();
            List<BankUserModel> users = DBdataAccess.LoadBankUsers();
            Console.WriteLine($"\nusers length: {users.Count}");
            foreach (BankUserModel user in users)
            {
                Console.WriteLine($"Hello {user.first_name} your pincode is {user.pin_code}");
            }

            bool isRunning = true;
            while(isRunning)
            {
                try
                {
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
                        //logOut = true;
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("ERROR, please try again.");
                }
                
            }

            
        }
        static void WelcomeRabbit()
        {
            Console.WriteLine("                                         ...:^~~~~^^^::.                                          \r\n                                 .:~7??!!^^Y##BB#BBBB##BBG?            .!~                        \r\n                             :!7!JGBBG57^  ?B#BGBBGPB####&G          ^7Y57                        \r\n                         .!5GBBP?!PB##B5~  !BBBBBBGPBB##&&J        ^!7J5?.                        \r\n                      .7PB#BBBBGG??G##BP?: :P##BBGGGB###&&?    .:^:!J5Y^                          \r\n                    .P######BB#BGP7?PBGGY^ .?BBBBBGGB##&&&! ..^~:^7555:                           \r\n                    .P######BBBBBPPJJPBBGJ: ~5BGBGGG##&&##~:~^::7JJ?^!.                           \r\n                      7#####BGGBBGPPJYGBG5~.:?GBGGGB#&&&#P7:..^J5J~..~..                          \r\n                       ^B###BBBBBGPP5?YGGPJ:.!PBBGGB#&&BY!: .~Y57~:..:.                         \r\n                        :G###BBBBBGGPY?PBBG!.:JB#BB####?^. .^JPJ!!:.~:..                          \r\n                         .G###BBBBBGGPJYBBG5^.!G####BBY7. ..?P5?!~.~^.                         \r\n                          ^######BBBGG5J5BBB?.^P#B#BG57: ..^5P5?7^:!:.                         \r\n                           7########BBBGYGGPYJYPGB##5?!.:^^^PPY?7!JBP7:.                        \r\n                            5###&##BGP5J~^7YY?YG#&&G5!^^~::~PJJ?77G#GPG!.                       \r\n                            :B##BBGP55J7~~JJ~:7G#&&GY7!!~.^7Y!7?7PB5JGBB^.                      \r\n                             Y####BGGGGYY5GG5~~5##BPJ~^:.:^?J?~77BBGB##5:.                       \r\n                           .^Y#&&&#B#GPB&&&#BPP5J7??!^^::^^!7!7YB#&&#P!.                       \r\n                        ^JGB#######BG#&&&&BG5J?!^~!~~^^^:^^!7!7YB#G?^:.                       \r\n                     .~J5GB###BBGPPB&&&BPY7!~^:...:::::^^:^~!~!7JP7.                       \r\n                    .YPY77J5PGPPG#&&#G5?~~........ ..:~~^~:^^~~77YY^.                         \r\n                    !GBBG5YJYPB&&#GY?!~^:.  ..:^7??^..:...::^~^!!Y5!:.                         \r\n                    ~JGGGB#&&&BP5Y7!^~::....:.7GGGBY:.:.: .:^^^!7JY~.                         \r\n                     .:^~7?7!YYJ?!~:^......:~77YPG5~.  ....~^~~!?J?.                         ");
            //Original image is Regency Rabbit, a drawing by Adam Zebediah Joseph.
            Console.WriteLine("                                      Welcome to");
            string prompt = @"
██████╗  █████╗ ██████╗ ██████╗ ██╗████████╗    ██████╗  █████╗ ███╗   ██╗██╗  ██╗
██╔══██╗██╔══██╗██╔══██╗██╔══██╗██║╚══██╔══╝    ██╔══██╗██╔══██╗████╗  ██║██║ ██╔╝
██████╔╝███████║██████╔╝██████╔╝██║   ██║       ██████╔╝███████║██╔██╗ ██║█████╔╝ 
██╔══██╗██╔══██║██╔══██╗██╔══██╗██║   ██║       ██╔══██╗██╔══██║██║╚██╗██║██╔═██╗ 
██║  ██║██║  ██║██████╔╝██████╔╝██║   ██║       ██████╔╝██║  ██║██║ ╚████║██║  ██╗
╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ╚═════╝ ╚═╝   ╚═╝       ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝";
            Console.WriteLine(prompt);
            Console.WriteLine("                             The most rabbid bank in the world");
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