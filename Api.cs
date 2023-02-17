using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace rabbit_bank
{
    internal class Api
    {
            public static bool ImportUSDRate()
            {
                try
                {
                    String URLString = "https://v6.exchangerate-api.com/v6/32b26456dd41b6e1bc2befd1/pair/USD/SEK";
                    using (WebClient webClient = new WebClient())
                    {
                        var json = webClient.DownloadString(URLString);
                        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);
                        using (IDbConnection cnn = new NpgsqlConnection(DBAccess.LoadConnectionString()))
                        {
                            var output = cnn.Query<UserModel>($"UPDATE bank_currency SET exchange_rate = '{Math.Round(myDeserializedClass.conversion_rate, 2).ToString(CultureInfo.GetCultureInfo("en-GB"))}' WHERE bank_currency.id = '2'", new DynamicParameters());

                        }

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return true;
            }
        }

        public class Root
        {
            public string result { get; set; }
            public string documentation { get; set; }
            public string terms_of_use { get; set; }
            public int time_last_update_unix { get; set; }
            public string time_last_update_utc { get; set; }
            public int time_next_update_unix { get; set; }
            public string time_next_update_utc { get; set; }
            public string base_code { get; set; }
            public string target_code { get; set; }
            public double conversion_rate { get; set; }
        }
    }