namespace Fanex.Affiliate.WebServices.Core
{
    using System;
    using System.Configuration;

    public static class AppSettings
    {
        public static Uri RNGCasinoGetServerTokenAPIUrl
        {
            get { return new Uri(GetValue("RNGCasinoGetServerTokenAPIUrl")); }
        }

        public static string RNGCasinoLogin
        {
            get { return GetValue("RNGCasinoLogin"); }
        }

        public static string RNGCasinoPassword
        {
            get { return GetValue("RNGCasinoPassword"); }
        }

        public static int RNGCasinoSiteId
        {
            get { return Convert.ToInt32(GetValue("RNGCasinoSiteId")); }
        }

        public static string RNGCasinoSeq
        {
            get { return GetValue("RNGCasinoSeq"); }
        }

        public static int RNGCasinoVendorId
        {
            get { return Convert.ToInt32(GetValue("RNGCasinoVendorId")); }
        }

        public static string RNGCasinoEncryptedKey
        {
            get { return GetValue("RNGCasinoEncryptedKey"); }
        }

        public static Uri RNGCasinoResultPopupUrl
        {
            get { return new Uri(GetValue("RNGCasinoResultPopupUrl")); }
        }

        public static string RNGCasinoTemplate
        {
            get { return GetValue("RNGCasinoTemplate"); }
        }

        public static string RNGCasinoRole
        {
            get { return GetValue("RNGCasinoRole"); }
        }

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}