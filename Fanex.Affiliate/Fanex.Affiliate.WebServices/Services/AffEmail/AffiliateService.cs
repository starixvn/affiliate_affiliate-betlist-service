// ----------------------------------------------------------------------
// <copyright file="AffiliateService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/24">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Services
{
    using Criteria;
    using DataAccess;

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