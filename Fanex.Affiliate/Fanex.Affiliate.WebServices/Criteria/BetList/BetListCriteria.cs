// ----------------------------------------------------------------------
// <copyright file="BetListCriteria.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

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
            ShowCS = false;
            ShowRB = false;
            ShowNG = false;
            ShowLC = false;
            ShowVS = false;
            ShowKN = false;
            ShowAG = false;
            ShowGD = false;
            ShowCB = false;
            ShowCF = false;
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

        public bool ShowCS { get; set; }

        public bool ShowRB { get; set; }

        public bool ShowNG { get; set; }

        public bool ShowLC { get; set; }

        public bool ShowVS { get; set; }

        public bool ShowKN { get; set; }

        public bool ShowAG { get; set; }

        public bool ShowGD { get; set; }

        public bool ShowCB { get; set; }

        public bool ShowCF { get; set; }

        // Output parameters
        public int Error { get; set; }

        public int Duration { get; set; }
    }
}