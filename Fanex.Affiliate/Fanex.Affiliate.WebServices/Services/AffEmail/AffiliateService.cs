// ----------------------------------------------------------------------
// <copyright file="AffiliateService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.AffEmail
{
    using Criteria.AffEmail;
    using DataAccess.AffEmail;

    /// <summary>
    /// Account Service.
    /// </summary>
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateDataAccess _affiliateDataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="AffiliateService" /> class.
        /// </summary>
        public AffiliateService()
        {
            _affiliateDataAccess = new AffiliateDataAccess();
        }

        /// <summary>
        /// Get Affiliate Information.
        /// </summary>
        /// <param name="criteria">Affiliate Information criteria.</param>
        /// <param name="siteName">Site Name.</param>
        /// <returns>Affiliate Information.</returns>
        public object GetAffiliateInfo(AffiliateInfoCriteria criteria, string siteName)
        {
            return _affiliateDataAccess.GetAffiliateInfo(criteria, siteName);
        }
    }
}