using Dapper;
using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public class DBAccess
    {

        //public static bool TransferSameCurrency(int user_id, int other_user_id, int from_account_id, int to_account_id, decimal amount)
        //{
        //    if (other_user_id == 0)
        //    {
        //        if (currency_id == 1) //CURRENCY ID 1 IS SEK, THIS WILL CHECK IF:    SEK TO SEK,   OR    SEK TO USD
        //        {
        //            try
        //            {
        //                string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB")); //THIS STRING MAKES amount look brittish eg. 100,00 becomes 100.00
        //                var output = cnn.Query<UserModel>($@"
        //                        BEGIN TRANSACTION;
        //                        UPDATE bank_account SET balance = CASE
        //                             when id = {from_account_id} AND user_id = {user_id} AND currency_id = 1 AND balance >= {newAmount} then balance - {newAmount}
        //                             when id = {to_account_id} AND currency_id = 1 then balance + {newAmount}
        //                             when id = {to_account_id} AND currency_id = 2 then balance + round({newAmount} / 10.35, 2)
        //                        END
        //                        WHERE id IN({from_account_id}, {to_account_id});
        //                        INSERT INTO bank_transaction (name, from_account_id, to_account_id, amount) VALUES ('Överföring', {from_account_id}, {to_account_id}, round(100 / 10.35, 2));
        //                        COMMIT;    
        //                        ", new DynamicParameters());    //RIGHT ABOVE HERE IS WHERE CONVERSION IS ACTUALLY BEING MADE

        //                Console.WriteLine("\nSuccessful transfer");
        //            }
        //            catch (Npgsql.PostgresException e)
        //            {
        //                Console.WriteLine();
        //                Console.WriteLine(e.ErrorCode);
        //                Console.WriteLine(e.MessageText);
        //                Console.WriteLine("\nTransfer was not successful. ");
        //            }
        //        }
        //        else if (currency_id == 2)    //CURRENCY ID 2 IS USD, SAME PROCEDURE AS ABOVE WILL HAPPEN BUT WITH SMALL CHANGES
        //        {
        //            try
        //            {
        //                string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
        //                var output = cnn.Query<UserModel>($@"
        //                        UPDATE bank_account SET balance = CASE
        //                             when id = {from_account_id} AND user_id = {user_id} AND currency_id = 2 AND balance >= {newAmount} then balance - {newAmount}
        //                             when id = {to_account_id} AND currency_id = 1 then balance + round({newAmount} * 10.35, 2)
        //                             when id = {to_account_id} AND currency_id = 2 then balance + {newAmount}
        //                        END
        //                        WHERE id IN({from_account_id}, {to_account_id})", new DynamicParameters());
        //                Console.WriteLine("\nSuccessful transfer");
        //            }
        //            catch (Npgsql.PostgresException e)
        //            {
        //                Console.WriteLine();
        //                Console.WriteLine(e.ErrorCode);
        //                Console.WriteLine(e.MessageText);
        //                Console.WriteLine("\nTransfer was not successful. ");
        //            }

        //        }
        //    }
        //    else //IF other_user_id IS ONE SELF THEN THIS BLOCK WILL RUN
        //    {
        //        try
        //        {
        //            string newAmount = Math.Round(amount, 2).ToString(CultureInfo.CreateSpecificCulture("en-GB"));
        //            var output = cnn.Query<UserModel>($@"
        //                        UPDATE bank_account SET balance = CASE
        //                             when id = {from_account_id} AND user_id = {user_id} AND currency_id = 1 AND balance >= {newAmount} then balance - {newAmount}
        //                             when id = {to_account_id} AND currency_id = 1 AND user_id = {other_user_id} then balance + {newAmount}
        //                             when id = {to_account_id} AND currency_id = 2 AND user_id = {other_user_id} then balance + round({newAmount} / 10.35, 2)
        //                        END
        //                        WHERE id IN({from_account_id}, {to_account_id})", new DynamicParameters());

        //            Console.WriteLine("\nSuccessful transfer");
        //        }
        //        catch (Npgsql.PostgresException e)
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine(e.ErrorCode);
        //            Console.WriteLine(e.MessageText);
        //            Console.WriteLine("\nTransfer was not successful. ");
        //        }
        //    }
        //    return true;
        //}

        //public static bool TransferDIfferentCurrencies(int user_id, int other_user_id,int currency_id, int from_account_id, int to_account_id, decimal amount)
        //{

        //}

        public static bool TransferMoney(int user_id, int other_user_id, int currency_id, int from_account_id, int to_account_id, decimal amount, string logEx)
        {
            //In this method, money is being transfered to self or others
            //Money also gets converted here

            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                //IF SEND TO OTHERS ACCOUNTS THEN other_user_id = 0
                //ELSE SENDS TO USERS OWN ACCOUNTS
                
                if (other_user_id == 0)
                {
                    if (currency_id == 1) //CURRENCY ID 1 IS SEK, THIS WILL CHECK IF:    SEK TO SEK,   OR    SEK TO USD
                    {
                        try
                        {
                            string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB")); //THIS STRING MAKES amount look brittish eg. 100,00 becomes 100.00
                            var output = cnn.Query($@"
                                BEGIN TRANSACTION;
                                UPDATE bank_account SET balance = CASE
                                     when id = {from_account_id} AND user_id = {user_id} AND currency_id = 1 AND balance >= '{newAmount}' then balance - '{newAmount}'
                                     when id = {to_account_id} AND currency_id = 1 then balance + {newAmount}
                                     when id = {to_account_id} AND currency_id = 2 then balance + round({newAmount} / 10.35, 2)
                                END
                                WHERE id IN ({from_account_id}, {to_account_id});
                                INSERT INTO bank_transaction (name, from_account_id, to_account_id, amount) VALUES ('Överföring', {from_account_id}, {to_account_id}, '{newAmount}');
                                COMMIT;", new DynamicParameters());    //RIGHT ABOVE HERE IS WHERE CONVERSION IS ACTUALLY BEING MADE

                            Console.WriteLine("\nSuccessful transfer");
                        }
                        catch (Npgsql.PostgresException e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.ErrorCode);
                            Console.WriteLine(e.MessageText);
                            Console.WriteLine("\nTransfer was not successful. ");
                        }
                    }
                    else if (currency_id == 2)    //CURRENCY ID 2 IS USD, SAME PROCEDURE AS ABOVE WILL HAPPEN BUT WITH SMALL CHANGES
                    {
                        try
                        {
                            string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
                            var output = cnn.Query<UserModel>($@"
                                BEGIN TRANSACTION;
                                UPDATE bank_account SET balance = CASE
                                     when id = {from_account_id} AND user_id = {user_id} AND currency_id = 2 AND balance >= {newAmount} then balance - {newAmount}
                                     when id = {to_account_id} AND currency_id = 1 then balance + round({newAmount} * 10.35, 2)
                                     when id = {to_account_id} AND currency_id = 2 then balance + {newAmount}
                                END
                                WHERE id IN({from_account_id}, {to_account_id});
                                INSERT INTO bank_transaction (name, from_account_id, to_account_id, amount) VALUES ('Överföring', {from_account_id}, {to_account_id}, '{newAmount}');
                                COMMIT;", new DynamicParameters());
                            Console.WriteLine("\nSuccessful transfer");
                        }
                        catch (Npgsql.PostgresException e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.ErrorCode);
                            Console.WriteLine(e.MessageText);
                            Console.WriteLine("\nTransfer was not successful. ");
                        }

                    }
                }
                else //IF other_user_id IS ONE SELF THEN THIS BLOCK WILL RUN
                {
                    if (currency_id == 1) //AND THIS IS EXACLTY THE SAME AS ABOVE EXCEPT FOR THE "AND user_id = {other_user_id}" PART IN THE QUERY BELOW
                    {
                        try
                        {
                            string newAmount = Math.Round(amount, 2).ToString(CultureInfo.CreateSpecificCulture("en-GB"));
                            var output = cnn.Query<UserModel>($@"
                                BEGIN TRANSACTION;
                                UPDATE bank_account SET balance = CASE
                                     when id = {from_account_id} AND user_id = {user_id} AND currency_id = 1 AND balance >= {newAmount} then balance - {newAmount}
                                     when id = {to_account_id} AND currency_id = 1 AND user_id = {other_user_id} then balance + {newAmount}
                                     when id = {to_account_id} AND currency_id = 2 AND user_id = {other_user_id} then balance + round({newAmount} / 10.35, 2)
                                END
                                WHERE id IN({from_account_id}, {to_account_id});
                                INSERT INTO bank_transaction (name, from_account_id, to_account_id, amount) VALUES ('Överföring', {from_account_id}, {to_account_id}, '{newAmount}');
                                COMMIT;", new DynamicParameters());

                            Console.WriteLine("\nSuccessful transfer");
                        }
                        catch (Npgsql.PostgresException e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.ErrorCode);
                            Console.WriteLine(e.MessageText);
                            Console.WriteLine("\nTransfer was not successful. ");
                        }
                    }
                    else if (currency_id == 2)
                    {
                        try
                        {
                            string newAmount = Math.Round(amount, 2).ToString(CultureInfo.CreateSpecificCulture("en-GB"));
                            var output = cnn.Query<UserModel>($@"
                                BEGIN TRANSACTION;
                                UPDATE bank_account SET balance = CASE
                                     when id = {from_account_id} AND user_id = {user_id} AND currency_id = 2 AND balance >= {newAmount} then balance - {newAmount}
                                     when id = {to_account_id} AND currency_id = 1 AND user_id = {other_user_id} then balance + round({newAmount} * 10.35, 2)
                                     when id = {to_account_id} AND currency_id = 2 AND user_id = {other_user_id} then balance + {newAmount}
                                END
                                WHERE id IN({from_account_id}, {to_account_id});
                                INSERT INTO bank_transaction (name, from_account_id, to_account_id, amount) VALUES ('Överföring', {from_account_id}, {to_account_id}, '{newAmount}');
                                COMMIT;", new DynamicParameters());
                            Console.WriteLine("\nSuccessful transfer");
                        }
                        catch (Npgsql.PostgresException e)
                        {
                            Console.WriteLine();
                            Console.WriteLine(e.ErrorCode);
                            Console.WriteLine(e.MessageText);
                            Console.WriteLine("\nTransfer was not successful. ");
                        }

                    }
                }
                return true;
            }
        }



        //====      BELLOW IS THE OLD TransferMoney method

        //public static bool TransferMoney(int user_id, int other_user_id, int from_account_id, int to_account_id, decimal amount)
        //{
        //    using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        //    {
        //        if (other_user_id == 0)
        //        {
        //            try
        //            {
        //                string newAmount = Math.Round(amount, 2).ToString(CultureInfo.CreateSpecificCulture("en-GB"));
        //                var output = cnn.Query<UserModel>($@"
        //           UPDATE bank_account SET balance = CASE
        //               when id = {from_account_id} AND user_id = {user_id} then AND balance >= {newAmount} balance - {newAmount}
        //               when id = {to_account_id} then balance + {newAmount}
        //           END
        //           WHERE id IN ({from_account_id}, {to_account_id})", new DynamicParameters());

        //                Console.WriteLine("\nSuccessful transfer");
        //            }
        //            catch (Npgsql.PostgresException e)
        //            {
        //                Console.WriteLine();
        //                Console.WriteLine(e.ErrorCode);
        //                Console.WriteLine(e.MessageText);
        //                Console.WriteLine("\nTransfer was not successful. ");
        //            }
        //            //return true;
        //        }
        //        else
        //        {
        //            try
        //            {
        //                string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
        //                var output = cnn.Query<UserModel>($@"
        //           UPDATE bank_account SET balance = CASE
        //               when id = {from_account_id} AND user_id = {user_id} AND balance >= {newAmount} then balance - {newAmount}
        //               when id = {to_account_id} AND user_id = {other_user_id} then balance + {newAmount}
        //           END
        //           WHERE id IN ({from_account_id}, {to_account_id})", new DynamicParameters());
        //                Console.WriteLine("\nSuccessful transfer");
        //            }
        //            catch (Npgsql.PostgresException e)
        //            {
        //                Console.WriteLine();
        //                Console.WriteLine(e.ErrorCode);
        //                Console.WriteLine(e.MessageText);
        //                Console.WriteLine("\nTransfer was not successful. ");
        //            }

        //        }
        //        return true;
        //    }
        //}



        // METHOD BELOW WAS MADE TO ONLY LIST ALL ACCOUNTS ID's AND CURRENCIES IN RABBIT BANK. THIS HELPS ISOLATE SO THAT MONEY CANNOT ACCIDENTALLY BE LOST
        // AND ALSO HELPS IN OTHER WAYS
        public static List<AccountModel> GetAllAccountsIdAndCurrency()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<AccountModel>("SELECT bank_account.id, bank_currency.name AS currency_name, bank_currency.exchange_rate AS currency_exchange_rate FROM bank_account, bank_currency WHERE bank_account.currency_id = bank_currency.id ORDER BY id", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
        }
        public static List<UserModel> LoadBankUsers()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<UserModel>("select * from bank_user", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // Kopplar upp mot DB:n
            // läser ut alla Users
            // Returnerar en lista av Users
        }
        public static List<UserModel> CheckLogin(int firstName, int pinCode)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<UserModel>($"SELECT * FROM bank_user WHERE id = '{firstName}' AND pin_code = '{pinCode}'", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // Kopplar upp mot DB:n
            // läser ut alla Users
            // Returnerar en lista av Users
        }
        public static List<UserModel> CheckUsername(int firstName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<UserModel>($"SELECT * FROM bank_user WHERE id = '{firstName}'", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // Kopplar upp mot DB:n
            // läser ut alla Users
            // Returnerar en lista av Users
        }


        public static AccountModel GetAccountById(int account_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<AccountModel>($"SELECT bank_account.*, bank_currency.name AS currency_name, bank_currency.exchange_rate AS currency_exchange_rate FROM bank_account, bank_currency WHERE bank_account.id = '{account_id}' AND bank_account.currency_id = bank_currency.id", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList().First();
            }
        }

        //public static AccountModel GetexRate()
        //{
        //    using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
        //    {
        //        var output = cnn.Query<AccountModel>($"SELECT bank_currency.*, bank_currency.name AS currency_name, bank_currency.exchange_rate AS currency_exchange_rate FROM bank_currency WHERE name = USD AND bank_currency.id = 2", new DynamicParameters());
        //        //Console.WriteLine(output);
        //        return output;
        //    }
        //}


        public static List<AccountModel> GetUserAccounts(int user_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<AccountModel>($"SELECT bank_account.*, bank_currency.name AS currency_name, bank_currency.exchange_rate AS currency_exchange_rate FROM bank_account, bank_currency WHERE user_id = '{user_id}' AND bank_account.currency_id = bank_currency.id ORDER BY id", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // denna funktion ska leta upp användarens konton från databas och returnera dessa som en lista
            // vad behöver denna funktion för information för att veta vems konto den ska hämta
            // vad har den för information att tillgå?
            // vilken typ av sql-query bör vi använda, INSERT, UPDATE eller SELECT?
            // ...?

        }

        public static void UpdateExchangeRate(decimal userInput)
        {
            string newUserInput = userInput.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE bank_currency SET exchange_rate = {newUserInput} WHERE name = 'USD'");

            }
        }

        public static void ListExchangeRate()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"SELECT exchange_rate FROM bank_currency WHERE name='USD'");

            }
        }

        public static void SaveBankUser(UserModel user)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into bank_user (first_name, last_name, pin_code, role_id, branch_id) values (@first_name, @last_name, @pin_code, @role_id,@branch_id)", user);

            }
        }

        public static void SaveNewAccount(AccountModel account)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into bank_account (name, interest_rate, user_id, currency_id) values (@name, @interest_rate, @user_id, @currency_id)", account);

            }
        }

        public static void ListNewAccount(AccountModel account)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into bank_account (name, interest_rate, user_id, currency_id) values (@name, @interest_rate, @user_id, @currency_id)", account);

            }
        }

        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void updateBlockedUser()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE bank_user SET blocked_user = true WHERE attempts < 1");

                cnn.Execute("UPDATE bank_user SET blocked_user = false WHERE attempts > 0");
            }
        }

        public static void subtractAttempt(UserModel specificUser)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE bank_user SET attempts = attempts - 1 WHERE first_name = '{specificUser.first_name}'");
            }
        }

        public static void resetAttempts(UserModel specificUser)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE bank_user SET attempts = 3 WHERE id = '{specificUser.id}'");
            }
        }
        public static void blockUser(int pick)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE bank_user SET attempts = 0 WHERE id = '{pick}'");

            }
        }
        public static void unblockUser(int id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute($"UPDATE bank_user SET attempts = 3 WHERE id = '{id}'");
            }
        }

        public static List<AccountModel> exchangeR()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<AccountModel>("select * from bank_currency", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // Kopplar upp mot DB:n
            // läser ut alla Users
            // Returnerar en lista av Users
        }
        public static void UpdateAccount(int user_id, int from_account, decimal newBalance)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Query($"UPDATE bank_account SET balance = '{newBalance.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}' WHERE id = '{from_account}' AND user_id = '{user_id}'", new DynamicParameters());
                
            }

        }
        public static List<AccountModel> getUSDrate()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<AccountModel>("select exchange_rate from bank_currency where id = 2", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
        }

        public static List<TransactionModel> GetTransactionByAccountId(int account_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<TransactionModel>($"SELECT * FROM bank_transaction WHERE from_account_id = {account_id} OR to_account_id = {account_id} ORDER BY timestamp DESC", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // denna funktion ska leta upp användarens konton från databas och returnera dessa som en lista
            // vad behöver denna funktion för information för att veta vems konto den ska hämta
            // vad har den för information att tillgå?
            // vilken typ av sql-query bör vi använda, INSERT, UPDATE eller SELECT?
            // ...?

        }
        public static List<TransactionModel> GetTransactionsAll()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<TransactionModel>($"SELECT * FROM bank_transaction ORDER BY timestamp DESC", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // denna funktion ska leta upp användarens konton från databas och returnera dessa som en lista
            // vad behöver denna funktion för information för att veta vems konto den ska hämta
            // vad har den för information att tillgå?
            // vilken typ av sql-query bör vi använda, INSERT, UPDATE eller SELECT?
            // ...?

        }

    }
}
