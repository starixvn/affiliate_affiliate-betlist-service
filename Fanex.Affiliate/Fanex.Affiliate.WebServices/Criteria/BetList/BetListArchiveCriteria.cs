// ----------------------------------------------------------------------
// <copyright file="BetListArchiveCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/17">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Criteria
{
    /// <summary>
    /// Bet List Archive Criteria.
    /// </summary>
    public class BetListArchiveCriteria : BetListCriteria
    {
        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_BetList_SelWinLossByDate_Old";
        }
    }
}