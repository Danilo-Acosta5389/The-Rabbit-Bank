﻿using Dapper;
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
        public static bool TransferMoney(int user_id, int other_user_id, int from_account_id, int to_account_id, decimal amount)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                if (other_user_id == 0)
                {
                    try
                    {
                        string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
                        var output = cnn.Query<UserModel>($@"
                   UPDATE bank_account SET balance = CASE
                       when id = {from_account_id} AND user_id = {user_id} AND balance >= {newAmount} then balance - {newAmount}
                       when id = {to_account_id} then balance + {newAmount}
                   END
                   WHERE id IN ({from_account_id}, {to_account_id})", new DynamicParameters());

                        Console.WriteLine("\nSuccessful transfer");
                    }
                    catch (Npgsql.PostgresException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.ErrorCode);
                        Console.WriteLine(e.MessageText);
                        Console.WriteLine("\nTransfer was not successful. ");
                    }
                    //return true;
                }
                else
                {
                    try
                    {
                        string newAmount = amount.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
                        var output = cnn.Query<UserModel>($@"
                   UPDATE bank_account SET balance = CASE
                       when id = {from_account_id} AND user_id = {user_id} AND balance >= {newAmount} then balance - {newAmount}
                       when id = {to_account_id} AND user_id = {other_user_id} then balance + {newAmount}
                   END
                   WHERE id IN ({from_account_id}, {to_account_id})", new DynamicParameters());
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
                return true;
            }
        }



        public static List<UserModel> OldLoadBankUsers()
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
        public static List<UserModel> CheckLogin(string firstName, int pinCode)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<UserModel>($"SELECT bank_user.*, bank_role.is_admin, bank_role.is_client FROM bank_user, bank_role WHERE first_name = '{firstName}' AND pin_code = '{pinCode}' AND bank_user.role_id = bank_role.id", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // Kopplar upp mot DB:n
            // läser ut alla Users
            // Returnerar en lista av Users
        }
        public static List<UserModel> CheckUsername(string firstName)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {

                var output = cnn.Query<UserModel>($"SELECT bank_user.*, bank_role.is_admin, bank_role.is_client FROM bank_user, bank_role WHERE first_name = '{firstName}'", new DynamicParameters());
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


        public static List<AccountModel> GetUserAccounts(int user_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<AccountModel>($"SELECT bank_account.*, bank_currency.name AS currency_name, bank_currency.exchange_rate AS currency_exchange_rate FROM bank_account, bank_currency WHERE user_id = '{user_id}' AND bank_account.currency_id = bank_currency.id", new DynamicParameters());
                //Console.WriteLine(output);
                return output.ToList();
            }
            // denna funktion ska leta upp användarens konton från databas och returnera dessa som en lista
            // vad behöver denna funktion för information för att veta vems konto den ska hämta
            // vad har den för information att tillgå?
            // vilken typ av sql-query bör vi använda, INSERT, UPDATE eller SELECT?
            // ...?

        }

        public static void SaveBankUser(UserModel user)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into bank_user (first_name, last_name, pin_code, role_id, branch_id) values (@first_name, @last_name, @pin_code, @role_id,@branch_id)", user);

            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void updateBlockedUser()
        {
            using (var conn = new NpgsqlConnection(LoadConnectionString()))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("UPDATE bank_user SET blocked_user = true WHERE attempts < 1", conn))
                {
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new NpgsqlCommand("UPDATE bank_user SET blocked_user = false WHERE attempts > 0", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void subtractAttempt(UserModel specificUser)
        {
            using (var conn = new NpgsqlConnection(LoadConnectionString()))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand($"UPDATE bank_user SET attempts = attempts - 1 WHERE first_name = '{specificUser.first_name}'", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void resetAttempts(UserModel specificUser)
        {
            using (var conn = new NpgsqlConnection(LoadConnectionString()))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand($"UPDATE bank_user SET attempts = 3 WHERE first_name = '{specificUser.first_name}'", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
