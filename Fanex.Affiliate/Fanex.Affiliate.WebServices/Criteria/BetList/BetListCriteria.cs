// ----------------------------------------------------------------------
// <copyright file="BetListCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/19">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class BetListCriteria : CriteriaBase
    {
        public BetListCriteria()
        {
            AffiliateGroup = string.Empty;
            BaseCurrency = "EUR";
            Language = "E";
            ShowSB = false;
            ShowCasino = false;
            ShowRB = false;
            ShowNG = false;
            ShowLiveCasino = false;
            ShowVS = false;
            ShowProgressiveBonus = false;
            ShowKeno = false;
        }

        [SpParam(Ignored = true)]
        public string AffiliateGroup { get; set; }

        [SpParam(Ignored = true)]
        public string BaseCurrency { get; set; }

        [SpParam(Ignored = true)]
        public string Currency { get; set; }

        [SpParam(Ignored = true)]
        public int CurrencyId { get; set; }

        public int CustId { get; set; }
        
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Language { get; set; }

        public bool ShowSB { get; set; }

        public bool ShowCasino { get; set; }

        public bool ShowRB { get; set; }

        public bool ShowNG { get; set; }

        public bool ShowLiveCasino { get; set; }

        public bool ShowVS { get; set; }

        public bool ShowProgressiveBonus { get; set; }

        public bool ShowKeno { get; set; }
        
        // Output parameters
        public int Error { get; set; }

        public int Duration { get; set; }
    }
}