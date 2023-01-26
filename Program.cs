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
            //while (true)
            //{
            //    Console.Write("Please enter FirstName: ");
            //    string firstName = Console.ReadLine();

            //    Console.Write("Please enter PinCode: ");
            //    int pinCode = int.Parse(Console.ReadLine());
            //    List<BankUserModel> checkedUsers = DBdataAccess.CheckLogin(firstName, pinCode);
            //    if (checkedUsers.Count < 1)
            //    {
            //        Console.WriteLine("Login failed, please try again");
            //        continue;
            //    }
                //foreach (BankUserModel user in checkedUsers)
                //{
                //    user.accounts = DBdataAccess.GetUserAccounts(user.id);
                //    Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the id is {user.id}");
                //    Console.WriteLine($"role_id: {user.role_id} branch_id: {user.branch_id}");
                //    Console.WriteLine($"is_admin: {user.is_admin} is_client: {user.is_client}");
                //    Console.WriteLine($"User account list length: {user.accounts}");
                //    if (user.accounts.Count > 0)
                //    {
                //        foreach (BankAccountModel account in user.accounts)
                //        {
                //            Console.WriteLine($"ID: {account.id} Account name: {account.name} Balance: {account.balance}");
                //            Console.WriteLine($"Currency: {account.currency_name} Exchange rate: {account.currency_exchange_rate}");
                //        }
                //    }

                //}
            //}
        }
    }
}