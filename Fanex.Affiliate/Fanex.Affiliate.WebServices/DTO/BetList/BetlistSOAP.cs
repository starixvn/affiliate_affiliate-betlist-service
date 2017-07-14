// ----------------------------------------------------------------------
// <copyright file="BetlistSOAP.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using System.Collections.Generic;

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