namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Net;
    using System.Web;
    using Core;
    using Logging;
    using Newtonsoft.Json;

    public class RNGCasinoSingleWalletService : IRNGCasinoSingleWalletService
    {
        private static readonly IDictionary<string, string> SiteLanguages = new Dictionary<string, string>
            {
                { "en-US", "en" },
                { "zh-TW", "ch" },
                { "zh-CN", "cs" },
                { "zh-Hans", "zhcn" }
            };

        public string GetRngCasinoSingleWalletResult(string refNo)
        {
            var authenticatedToken = GetRngCasinoServerToken();

            if (string.IsNullOrEmpty(authenticatedToken))
            {
                return JsonConvert.SerializeObject(new { Success = false });
            }

            var authenticatedTokenInJson = JsonConvert.DeserializeObject<dynamic>(authenticatedToken);
            var result = authenticatedTokenInJson.Result;

            if (result.ErrorCode == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    Success = true,
                    Result = GetRngCasinoPopupUrl(result.Token.ToString(), refNo)
                });
            }

            return JsonConvert.SerializeObject(new
            {
                Success = false,
                Result = new
                {
                    ErrorCode = result.ErrorCode.ToString(),
                    ErrorDescription = result.ErrorDescription.ToString()
                }
            });
        }

        private static string CreateServerTokenParameters()
        {
            var timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            var login = AppSettings.RNGCasinoLogin;
            var password = AppSettings.RNGCasinoPassword;
            var siteId = AppSettings.RNGCasinoSiteId;
            var seq = AppSettings.RNGCasinoSeq;
            var vendorId = AppSettings.RNGCasinoVendorId;

            var auth = new { Login = login, Password = password, SiteId = siteId };
            var call = new { Seq = seq, VendorId = vendorId };

            return JsonConvert.SerializeObject(
                new
                {
                    TimeStamp = timeStamp,
                    Auth = auth,
                    Call = call
                });
        }

        private static string EncryptQueryString(string parameters, string encryptedKey)
        {
            return IBCCrypt.IBCCrypt.Encrypt(parameters, encryptedKey);
        }

        private static string GetLanguageCode()
        {
            var languageCode = CultureInfo.CurrentUICulture.IetfLanguageTag;
            string result;
            if (!SiteLanguages.TryGetValue(languageCode, out result))
            {
                return "en";
            }

            return result;
        }

        private static string GetQueryString(string token, string refNo)
        {
            var languageCode = GetLanguageCode();
            var template = AppSettings.RNGCasinoTemplate;
            var role = AppSettings.RNGCasinoRole;

            var queryString = $"Token={token}&RefID={refNo}&Lang={languageCode}&Template={template}&Role={role}";

            return queryString;
        }

        private static string GetRngCasinoPopupUrl(string token, string refNo)
        {
            var queryString = GetQueryString(token, refNo);
            var encryptedKey = AppSettings.RNGCasinoEncryptedKey;
            var encryptParam = EncryptQueryString(queryString, encryptedKey);
            var paramEncode = HttpUtility.UrlEncode(encryptParam);
            var popupUrl = AppSettings.RNGCasinoResultPopupUrl;

            return popupUrl + "?o=" + paramEncode;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed.")]
        private static string GetRngCasinoServerToken()
        {
            var uri = new Uri(AppSettings.RNGCasinoGetServerTokenAPIUrl);
            var paramString = CreateServerTokenParameters();
            var serverResponse = string.Empty;

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                    serverResponse = webClient.UploadString(uri, paramString);
                }
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception);
            }

            return serverResponse;
        }
    }
}