namespace Fanex.Affiliate.WebServices.Utilities
{
    using System.Globalization;
    using System.Threading;

    public static class LanguageHelper
    {
        private static readonly CultureInfo DefaultCulture = CultureInfo.CreateSpecificCulture("en-US");

        public static void SetCulture(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = DefaultCulture.DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = DefaultCulture.NumberFormat;
        }
    }
}