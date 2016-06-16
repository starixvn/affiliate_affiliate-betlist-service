// ----------------------------------------------------------------------
// <copyright file="CustomerInfoBetList.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/13">Created</change>
//   <change who="" date=""></change>
// </history>

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