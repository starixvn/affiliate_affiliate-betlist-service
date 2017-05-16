// ----------------------------------------------------------------------
// <copyright file="BetListArchiveCriteria.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

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