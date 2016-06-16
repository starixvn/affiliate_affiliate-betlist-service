// ----------------------------------------------------------------------
// <copyright file="AffTicket.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2014/04/15">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class AffTicket : Fanex.BetList.Core.Entities.Ticket
    {
        public string ExternalId { get; set; }

        public decimal LicenseeWl { get; set; }

        public int CurrencyId { get; set; }
    }
}