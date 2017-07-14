// ----------------------------------------------------------------------
// <copyright file="FormaterCurrency.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Utilities
{
    using System.Globalization;
    using System.Threading;

    public static class CurrencyFormatter
    {
        public const string Decimalformat20 = "{0:#,##0.00}";

        private const string Minus = "<span style='color: red'>{0}</span>";

        private static CultureInfo _defaultCulture = Thread.CurrentThread.CurrentCulture;

        public static string Format(object value, bool checkMinus = true, bool hasCurrency = false)
        {
            return Format(value, _defaultCulture, checkMinus, hasCurrency);
        }

        private static string Format(object value, CultureInfo culture, bool checkMinus, bool hasCurrency)
        {
            double d = 0;
            double.TryParse(value.ToString(), out d);

            // Check whether adding currency symbol or not
            var currencySymbol = !hasCurrency ? string.Empty
                : (culture.NumberFormat.CurrencySymbol ?? _defaultCulture.NumberFormat.CurrencySymbol);
            var newValue = string.Format("{0}{1}", currencySymbol, string.Format(Decimalformat20, value));

            // Format negative value: replace "-value" by "(value)"
            if (d < 0 && checkMinus)
            {
                if (newValue.Contains("-"))
                {
                    newValue = string.Format("({0})", newValue.Replace("-", string.Empty));
                }

                newValue = string.Format(Minus, newValue);
            }

            return newValue;
        }
    }
}