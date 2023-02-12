using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbit_bank
{
    public static class GlobalItems
    {

        public static TextInfo currentTextInfo = CultureInfo.CurrentCulture.TextInfo;
        public static List<int> accountsList = new List<int>();
        public static List<string> accountNameList = new List<string>();
        public static List<string> currencyNameList = new List<string>();
        public static List<decimal> balanceList = new List<decimal>();
        public static List<double> currencyRateList = new List<double>();
        public static List<double> interestRateList = new List<double>();
        public static List<AccountModel> globalAccountsList = DBAccess.GetAllAccountsIdAndCurrency();

    }
}
