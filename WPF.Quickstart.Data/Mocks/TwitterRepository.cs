using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Quickstart.Data.Mocks
{
    public static class TwitterRepository
    {
        public static List<string> GetSummaryScreenNames()
        {
            return new List<string>() { "TradingCentral", "Boursier_com", "InvestirFr", "LesEchos", "lemondefr", "Reuters", "FinancialTimes", "TheEconomist", "BloombergNews", "financialpost", "FinancialReview", "EconomicTimes", "ETMarkets", "Fin_Events" };
        }

        public static List<int> GetCountElementsForCombo()
        {
            return new List<int>() { 10, 20, 50, 100 };
        }
    }
}
