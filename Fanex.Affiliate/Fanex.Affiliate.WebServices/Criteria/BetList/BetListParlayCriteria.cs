// ----------------------------------------------------------------------
// <copyright file="BetListParlayCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/17">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class BetListParlayCriteria : CriteriaBase
    {
        public BetListParlayCriteria()
        {
            AffiliateGroup = string.Empty;
            Language = "E";
        }

        [SpParam(Ignored = true)]
        public string AffiliateGroup { get; set; }

        public long TransID { get; set; }

        public string Language { get; set; }

        /// <summary>
        /// Get SP name.
        /// </summary>
        /// <returns>Setting key.</returns>
        public override string GetSettingKey()
        {
            return "Aff_BetList_SelParlayByTransID";
        }
    }
}