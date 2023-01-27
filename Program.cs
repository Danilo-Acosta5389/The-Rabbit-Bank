using Npgsql.Replication;

namespace rabbit_bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to");
            string prompt = @"
██████╗  █████╗ ██████╗ ██████╗ ██╗████████╗    ██████╗  █████╗ ███╗   ██╗██╗  ██╗
██╔══██╗██╔══██╗██╔══██╗██╔══██╗██║╚══██╔══╝    ██╔══██╗██╔══██╗████╗  ██║██║ ██╔╝
██████╔╝███████║██████╔╝██████╔╝██║   ██║       ██████╔╝███████║██╔██╗ ██║█████╔╝ 
██╔══██╗██╔══██║██╔══██╗██╔══██╗██║   ██║       ██╔══██╗██╔══██║██║╚██╗██║██╔═██╗ 
██║  ██║██║  ██║██████╔╝██████╔╝██║   ██║       ██████╔╝██║  ██║██║ ╚████║██║  ██╗
╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ╚═════╝ ╚═╝   ╚═╝       ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝";
            Console.WriteLine(prompt);
            Console.WriteLine("Hop in to the rabbit whole of wealth");

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
                    int pinCode = int.Parse(Console.ReadLine());
                    Login.LoginTry(firstName, pinCode);
                }
                catch (Exception)
                {

                    Console.WriteLine("ERROR, please try again.");
                }
                
            }
        }
    }
}