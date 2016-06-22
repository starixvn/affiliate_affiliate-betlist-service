// ----------------------------------------------------------------------
// <copyright file="IAffiliateService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/24">Created</change>
//   <change who="" date=""></change>
// </history>

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