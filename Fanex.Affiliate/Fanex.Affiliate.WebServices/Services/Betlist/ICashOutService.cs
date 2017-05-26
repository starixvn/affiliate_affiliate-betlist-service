// ----------------------------------------------------------------------
// <copyright file="ICashOutService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using Criteria.BetList;
    using Fanex.Affiliate.WebServices.DTO.BetList;

    public interface ICashOutService
    {
        CashOutTicketDetail GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey);
    }
}