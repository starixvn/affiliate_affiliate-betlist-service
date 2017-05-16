// ----------------------------------------------------------------------
// <copyright file="IAffiliateService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services
{
    using Criteria;

    /// <summary>
    /// Interface Affiliate service.
    /// </summary>
    public interface IAffiliateService
    {
        /// <summary>
        /// Get Affiliate Information.
        /// </summary>
        /// <param name="criteria">Affiliate Information criteria.</param>
        /// <param name="siteName">Site Name.</param>
        /// <returns>Affiliate Information.</returns>
        object GetAffiliateInfo(AffiliateInfoCriteria criteria, string siteName);
    }
}