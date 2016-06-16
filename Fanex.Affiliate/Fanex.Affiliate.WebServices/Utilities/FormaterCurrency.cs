// ----------------------------------------------------------------------
// <copyright file="FormaterCurrency.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/11/12">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Utilities
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Format currency.
    /// </summary>
    public static class FormaterCurrency
    {
        public const string Decimalformat20 = "{0:#,##0.00}";

        private const string Minus = "<span style='color: red'>{0}</span>";

        private static CultureInfo _defaultCulture = Thread.CurrentThread.CurrentCulture;

        private static Dictionary<string, CultureInfo> SymbolsByCode
        {
            get
            {
                try
                {
                    var symbolsByCode = new Dictionary<string, CultureInfo>();
                    var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                    foreach (var item in regions)
                    {
                        var key = new RegionInfo(item.LCID).ISOCurrencySymbol;
                        if (!symbolsByCode.ContainsKey(key))
                        {
                            item.NumberFormat.CurrencyDecimalDigits = 2;
                            symbolsByCode.Add(key, item);
                        }
                    }

                    return symbolsByCode;
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Get currency symbol from currency code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        /// <returns>Currency symbol.</returns>
        public static string GetCurrencySymbol(string currencyCode = null)
        {
            return string.IsNullOrEmpty(currencyCode)
                ? _defaultCulture.NumberFormat.CurrencySymbol
                : SymbolsByCode[currencyCode].NumberFormat.CurrencySymbol;
        }

        /// <summary>
        /// Set default currency from a currency code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        public static void SetDefaultCurrency(string currencyCode)
        {
            _defaultCulture = GetCulture(currencyCode);
        }

        /// <summary>
        /// Set default currency from a culture information.
        /// </summary>
        /// <param name="culture">Culture information.</param>
        public static void SetDefaultCurrency(CultureInfo culture)
        {
            _defaultCulture = culture;
        }

        /// <summary>
        /// Get culture from a currency code.
        /// </summary>
        /// <param name="currencyCode">Currency code.</param>
        /// <returns>Culture information.</returns>
        public static CultureInfo GetCulture(string currencyCode)
        {
            // Default currency code
            if (string.IsNullOrEmpty(currencyCode))
            {
                return SymbolsByCode["MYR"];
            }

            // Test currency
            if (currencyCode.ToUpper() == "UUS")
            {
                return SymbolsByCode["USD"];
            }

            return SymbolsByCode[currencyCode];
        }

        /// <summary>
        /// Format the value based on culture.
        /// </summary>
        /// <param name="value">Object value.</param>
        /// <param name="culture">Culture information.</param>
        /// <param name="checkMinus">Check minus.</param>
        /// <param name="hasCurrency">Has currency.</param>
        /// <returns>Formatted value.</returns>
        public static string Format(object value, CultureInfo culture, bool checkMinus, bool hasCurrency)
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

        /// <summary>
        /// Format the value based on culture.
        /// </summary>
        /// <param name="value">Object value.</param>
        /// <param name="currencySymbol">Currency symbol.</param>
        /// <param name="checkMinus">Check minus.</param>
        /// <param name="hasCurrency">Has currency.</param>
        /// <returns>Formatted value.</returns>
        public static string Format(object value, string currencySymbol, bool checkMinus, bool hasCurrency)
        {
            return Format(value, GetCulture(currencySymbol), checkMinus, hasCurrency);
        }

        /// <summary>
        /// Format the value based on culture.
        /// </summary>
        /// <param name="value">Object value.</param>
        /// <param name="checkMinus">Check minus.</param>
        /// <param name="hasCurrency">Has currency.</param>
        /// <returns>Formatted value.</returns>
        public static string Format(object value, bool checkMinus = true, bool hasCurrency = false)
        {
            return Format(value, _defaultCulture, checkMinus, hasCurrency);
        }
    }
}