// ----------------------------------------------------------------------
// <copyright file="BetListViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DTO.BetList;

    /// <summary>
    /// Bet List View Model.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Reviewed. Suppression is OK here")]
    [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Since Betlist.dll used that data-structure.")]
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