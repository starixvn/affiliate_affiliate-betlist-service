// ----------------------------------------------------------------------
// <copyright file="TicketResult.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    [Serializable]
    public class TicketResult
    {
        public TicketResult()
        {
            MainTicketList = new List<AffTicket>();
            TicketDataList = new List<ITicketData>();
        }

        public IList<AffTicket> MainTicketList { get; set; }

        public List<ITicketData> TicketDataList { get; set; }

        // Sum value
        public int TotalRows { get; set; }

        public decimal SumWinLoss { get; set; }

        public decimal SumLicenseeWl { get; set; }

        public decimal SumComm { get; set; }

        public decimal SumPlayerComm { get; set; }

        public decimal SumStake { get; set; }
    }
}