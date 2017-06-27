// ----------------------------------------------------------------------
// <copyright file="BetListViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.ViewModels.Betlist
{
    using System;
    using System.Collections.Generic;
    using DTO.BetList;

    /// <summary>
    /// Bet List View Model.
    /// </summary>
    [Serializable]
    public class BetListViewModel
    {
        public List<TicketText> TicketTextList { get; set; }

        public TicketResult TicketResult { get; set; }

        public byte[] TicketResultBytes { get; set; }

        public bool IsShowSystemIdAndExternalId { get; set; }

        public bool IsShowWinLoss { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}