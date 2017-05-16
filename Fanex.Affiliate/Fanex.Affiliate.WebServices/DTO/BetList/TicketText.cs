// ----------------------------------------------------------------------
// <copyright file="TicketText.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;

    /// <summary>
    /// Ticket text.
    /// </summary>
    [Serializable]
    public class TicketText
    {
        public int Index { get; set; }

        public string ExternalId { get; set; }

        public string SystemId { get; set; }

        public string TransTime { get; set; }

        public string Choice { get; set; }

        public string Odds { get; set; }

        public string Stake { get; set; }

        public string WinLoss { get; set; }

        public string LicenseeWl { get; set; }

        public string Status { get; set; }
    }
}