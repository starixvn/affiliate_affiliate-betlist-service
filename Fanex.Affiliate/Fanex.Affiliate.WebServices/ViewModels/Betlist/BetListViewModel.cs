// ----------------------------------------------------------------------
// <copyright file="BetListViewModel.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/11/19">Created</change>
//   <change who="" date=""></change>
// </history>

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