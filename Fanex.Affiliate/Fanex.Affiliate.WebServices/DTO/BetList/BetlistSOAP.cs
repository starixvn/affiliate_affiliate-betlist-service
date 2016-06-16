// ----------------------------------------------------------------------
// <copyright file="BetlistSOAP.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/10/17">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Bet list SOAP class.
    /// </summary>
    public class BetlistSOAP
    {
        public BetlistSOAP()
        {
            Language = "en-US";
        }

        public string AffiliateName { get; set; }

        public decimal ExchangeRate { get; set; }

        public DateTime FromDate { get; set; }

        public string Language { get; set; }

        public string Product { get; set; }

        public List<Product> SupportedProducts { get; set; }

        public DateTime ToDate { get; set; }

        public string Username { get; set; }
    }
}