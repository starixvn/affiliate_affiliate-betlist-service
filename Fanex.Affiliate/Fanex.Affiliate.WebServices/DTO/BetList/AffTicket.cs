// ----------------------------------------------------------------------
// <copyright file="AffTicket.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

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