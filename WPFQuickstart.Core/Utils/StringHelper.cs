using System;

namespace WPFQuickstart.Core.Utils
{
    public static class StringHelper
    {
        public static double ReplaceAndConvertToDouble(this string sInput, string oldSep, string newSep)
        {
            if (string.IsNullOrEmpty(sInput))
            {
                return 0;
            }
            else
            {
                string output = sInput.Replace(oldSep, newSep);
                return Convert.ToDouble(output);
            }
        }
    }
}
