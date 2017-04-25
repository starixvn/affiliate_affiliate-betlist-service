// ----------------------------------------------------------------------
// <copyright file="BalanceUpMoveBetCriteria.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Criteria to get Balance-Up and Move-Bet dates.
    /// </summary>
    public class BalanceUpMoveBetCriteria : CriteriaBase
    {
        // Output parameters
        [SpParam(Ignored = true)]
        public DateTime MoveBetsDay { get; set; }

        [SpParam(Ignored = true)]
        public DateTime BalanceUpDay { get; set; }

        [SpParam(Ignored = true)]
        public string AffiliateGroup { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_BetList_SelBalanceUpDay";
        }
    }
}