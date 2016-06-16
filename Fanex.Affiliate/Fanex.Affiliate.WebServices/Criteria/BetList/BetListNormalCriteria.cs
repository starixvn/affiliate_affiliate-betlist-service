// ----------------------------------------------------------------------
// <copyright file="BetListNormalCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/17">Created</change>
//   <change who="" date=""></change>
// </history>

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