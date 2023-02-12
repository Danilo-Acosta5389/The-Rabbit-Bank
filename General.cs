using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Npgsql.Replication;
using Spectre.Console;

namespace rabbit_bank
{
    public class General
    {
        public static void App()
        {
            bool isRunning = true;
            while (isRunning)
            {
                WelcomeRabbit();
                //List<UserModel> users = DBAccess.LoadBankUsers();
                //Console.WriteLine($"users length: {users.Count}");
                //foreach (UserModel user in users)
                //{
                //    Console.WriteLine($"Existing user: {user.first_name} with pincode: {user.pin_code}, account lock:{user.blocked_user}, attempts left: {user.attempts}");
                //}
                //int count = 1;
                //var getAllAccounts = GlobalItems.globalAccountsList;
                //foreach (var accounts in getAllAccounts)
                //{
                //    Console.WriteLine($" {count}. Account ID: {accounts.id}, currency ID: {accounts.currency_id}, currency name: {accounts.currency_name}");
                //    count++;
                //}
                List<UserModel> users = DBAccess.LoadBankUsers();
                Console.WriteLine($"users length: {users.Count}");
                
                foreach (UserModel user in users)
                {
                    string isAdmin = (user.role_id == 1) ? "YES" : "NO";
                    Console.WriteLine($"Existing user id: {user.id} name: {user.first_name} with pincode: {user.pin_code}, account lock:{user.blocked_user}, attempts left: {user.attempts}, is admin = {isAdmin}");
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
            AnsiConsole.Markup("[underline red]Hello[/] World!");

            string rabbit = @"



     
     
                                 ,,▄╦@ÑÑ▓████▓█████▄▄          ╓╗
                             ▄▄╫▓█▓█▓╢r ▓█████▓██████       ,@▓▓▓
                         ,▄████▓▓▓▓██▓@ ╫█████▓█████▌░    ,$▓▓▓▓`
                      ,▄███████▓▓▓▓██▓╣ ║▓████▓█████▌░░░#╨Ç0▓▓
                      ▀█████████▓▓▓▓██▓H]╢███▓██████▌╔╢╨╣▓▓▓╝╫░
                       ""████████▓▓▓▓██▓▓░▒▓███▓████▓▓░ ╔▓▓╣/┘▒▒░
                         ███████▓█▓▓▓▓█▓▒▒▓███████▓▀ .╠▓▓▓▒,Φ▒▒░
                          ▓███████▓▓▓▓█▓▓░▒▓█████▓M .å▓▓▓▓▒╓▒▒░░
                           ████████▓▓▓▓█▓▓╚▓████▓▌.nUB▓▓▓▓▒▓▒▒▒░░
                            ██████████▀▓▓▓▓▓███▓▓╝]╛%▐▓▓▓▓▓██▓▒▒▒░
                           ;▐█████▓▓▓╣▒▓▓▒▓████▓▓▓▓,h▓▌▓▓▓██▓██▒▒▒░ `
                             ██████▓█▓▓▓█▓▒▓███▓▌╨╖╓╝▓▓▓▓▓█████▒▒░
                           ▄▄█████████████▓▓▓╬▓▓Ç┼)┼╬▓▓▓▓████▀▒▒▒░░
                       ,▄██████████████▓▓▓╫ÖK╜╠3╝╙%▒Ñ╬Ñ▓▓▓▓░▒▒▒░░░
                      Æ▓▓▓▓▓████████▓▓▓""Ω `¡å``ª╟▓H▓▒Ñ╬Φ▓▓▓▒▒▒░░░░░
                      ▓███▓▓█████▓▓╣wΦF ,┘'╓@▓▓º╖▒ `▒╨@Ö▓▓▓▓░▒░░░░░░
                      ▀▀▀█████▓▓▓▓╬Ω ?:└}╗▓████k ""▒,⌐▒▒╟▓▓▓╨░░░░░
                        '░░░░▓▓▓Ü▒╜╤7² >â╜▒G╙▒╕└  ""╜▒╟╟▓▓▓▓▒░░░░░░
                            ▐▓▓Ñ[▒j, ~ `,""Y┴.─-` ≤ƒ%▓╟▓▓▓▓▀╛░░░░░
                            █▓╢▒▒▒ ¿,     `╠çΘ\╓╓╢▓╝▀╙⌐ "" å░░░░░░░
                           j▓▓╬╜ä▓▒   ╣B╗=─]▓▓▓╬▓▓   .─` ▐▓▓╣▒▒░░░░░░
                           .▓▓▓▓▓╫╓ú¬.ƒ▓▓▓╣W╫▓▓▓▓  , ╓╝▐╜▓ ▌▓▄░░▒░░░
                            ▀▓█▓▓φ╬∞╣╬▓▓▓▓▓▓▓▓▓▓@▒4▓▓▌─▓▐Ü▓▓▓▓▌░░░░░░
                            ─9▀▓▓▓▓▓▓▓▓▓▓▓▓▀▓▀▓▓▓PN▄▓╕  ╓ ▓▀╫██µ░░░▒ `
                              ] ▐╢▓▀▀▀█▓▓▓▓▀▀▀▀""`▐▀▓▌y╫m▓▓▒▓█▓▓▓▄░░░▒
                               \▒▌)▌╔m▄▌ ▒▓▒   ╙ ▓▓▓ ▓▒▓▓▒▓╬╫▒╫▓▓▓▓▄░░
                              ╒█WW▓ m╜▓▓]g▓▓╜  r▓▓▀,▓g▓▒╫▒▒▓▓▀╜▒▒║▓▓█
                             ╓█▓▓▓▓DÆ▓g▓▓▓▐▌▓╦A▓▓▓╫▓▓╜  `╫▓▒▒▒▒▒@▓████▄
                            ▄▓█▓▓▓▓▄▓▓▀ ╒▌╘▓j▓╠▓█▓▀  (╣▒ `╓φ▓╢▓▓█▓▓▓████,
                        ░ ▄▓▓█▌▓▓╜╓▓▀╜ ,▓▌ ,/,╢▓▀.l╬╫╢▓▓B╣▀▒╢▓█▓▒▓█▓▓▓▓██▓ÑV,
                         ' ▄▓█▒▌,▓╟` ▓ ▓▓ φ ▄▀,ƒ▒▒╜╙▓▓╨┤`,▓██▓▓▓█▓▒▄▓▓▓▓▒╙ ▀▀N
                           `▓▓ ▌╫▓╜ ▓""╓▓,▐▓▀,▓▓Ñ ¿▄j╬▒┘,▄█▓▓▓▓▓▀,W▓▓█▓▓─
                        .░Æ▓▓▓ ▓╩▓ $ ╙`╓▓▀ ""╙╙``▐██░F ▄█▓▓▓▓▓▓▒ß▓▓▓▓█▓*
                         ░╙▓▀▓ ╫▓▓▐▓╥▓▓▓▓½─m▀▓█▓███▌,▓█████▓▓╨ ╜`
                         ░ ╠▓▓m ▐█▐▓▓▓▓▓▓▓▓@▓╣▄██████████▓╨`
                         `░╟▓▓▓L∩▓▓▓▓▓▓╝▒▄▓▓██▓▓▓█████▓▓╝
                           ╙▓▌▓█▓▓▓▒▒▓▓▀▓▓▓▓█▓▓███▓▓╝╜ '
                           ╚▓██▌███▓▓▒╣Ñ▒▓█▓██▓▓▀▀
                          ,g▓█▌█▓▒▓▓▒▒▒▒▄▒``╙▀▓╜
                            j▓]▓█▓█▓,╙▀▀▓▓╫H
                             ▓ÿ▓▓▓▒╚▓@∩ ╡;Yj^
                             ╚ ╙
     
     
     
    
---
asciiart.club
";
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
