// ----------------------------------------------------------------------
// <copyright file="BetListNormalCriteria.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Criteria
{
    using System;

    /// <summary>
    /// Bet List Normal Criteria.
    /// </summary>
    public class BetListNormalCriteria : BetListCriteria
    {
        public DateTime MoveBetsDay { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_BetList_SelWinLossByDate";
        }
    }
}