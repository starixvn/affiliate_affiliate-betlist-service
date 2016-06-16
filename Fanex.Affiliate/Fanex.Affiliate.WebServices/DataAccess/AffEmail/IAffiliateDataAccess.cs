// ----------------------------------------------------------------------
// <copyright file="IAffiliateDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/02/24">Created</change>
//   <change who="" date=""></change>
// </history>

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