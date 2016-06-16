namespace Fanex.Affiliate.WebServices.Utilities
{
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using Fanex.Affiliate.WebServices.Resources;

    /// <summary>
    /// Language Mapper.
    /// </summary>
    public class LanguageManager
    {
        /// <summary>
        /// Culture USA.
        /// </summary>
        private static readonly CultureInfo _DefaultCulture = CultureInfo.CreateSpecificCulture("en-US");

        /// <summary>
        /// Resource mapper language.
        /// </summary>
        private readonly ResourceManager _resourceManagerLang;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageManager"/> class.
        /// </summary>
        public LanguageManager()
        {
            _resourceManagerLang = new ResourceManager(typeof(Lang)) { IgnoreCase = true };
        }

        /// <summary>
        /// Set Culture by Language code.
        /// </summary>
        /// <param name="language">Language code.</param>
        public void SetCulture(string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = _DefaultCulture.DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = _DefaultCulture.NumberFormat;
        }

        /// <summary>
        /// Gets Text by Key.
        /// </summary>
        /// <param name="key">Resource key.</param>
        /// <returns>Return Resource data by key.</returns>
        public string GetResourceData(string key)
        {
            if (!ReferenceEquals(_resourceManagerLang, null))
            {
                return _resourceManagerLang.GetString(key);
            }

            return string.Empty;
        }
    }
}