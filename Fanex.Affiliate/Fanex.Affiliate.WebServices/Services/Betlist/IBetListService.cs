// ----------------------------------------------------------------------
// <copyright file="IBetListService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using BetList.Core.Entities;
    using Criteria.BetList;
    using DTO.BetList;

    public interface IBetListService
    {
        TicketResult GetBetList(BetListCriteria criteria);

        TicketResult GetBetListParlay(BetListParlayCriteria criteria);

        SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria);

        void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria);

        CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername);

        TicketResult GetBetListDetail(BetListDetailCriteria criteria, string spkey);
    }
}