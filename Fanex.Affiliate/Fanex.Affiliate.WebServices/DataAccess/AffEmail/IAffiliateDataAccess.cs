// ----------------------------------------------------------------------
// <copyright file="IAffiliateDataAccess.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using Criteria;

    /// <summary>
    /// Interface Account.
    /// </summary>
    public interface IAffiliateDataAccess
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