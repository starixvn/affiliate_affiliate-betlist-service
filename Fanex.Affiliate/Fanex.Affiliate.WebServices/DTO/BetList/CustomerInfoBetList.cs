// ----------------------------------------------------------------------
// <copyright file="CustomerInfoBetList.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    /// <summary>
    /// User data object.
    /// </summary>
    public class CustomerInfoBetList
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public string IBCUsername { get; set; }

        public int CurrencyId { get; set; }
    }
}