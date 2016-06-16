// ----------------------------------------------------------------------
// <copyright file="BetListSystemParlayDetailCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/28">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// System Parlay Detail Criteria.
    /// </summary>
    public class BetListSystemParlayDetailCriteria : CriteriaBase
    {
        public BetListSystemParlayDetailCriteria()
        {
            AffiliateGroup = string.Empty;
        }

        [SpParam(Ignored = true)]
        public string AffiliateGroup { get; set; }

        public int UserId { get; set; }

        public long TransId { get; set; }

        public long RefNo { get; set; }

        public long BetId { get; set; }

        public int CustId { get; set; }

        public DateTime MoveBetsDay { get; set; }

        public DateTime BalanceUpDay { get; set; }

        // Output parameters
        public decimal Outstanding { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_BetList_SelCombinationParlayDetail";
        }
    }
}
