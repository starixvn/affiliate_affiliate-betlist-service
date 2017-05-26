// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using BetList.Core.Entities;
    using Criteria.BetList;
    using DataAccess;
    using DTO.BetList;
    using Fanex.Common.Helper;

    public class BetListService : IBetListService
    {
        private readonly IBetListDataAccess _betlistDataAccess;

        public BetListService()
            : this(null)
        {
        }

        private BetListService(IBetListDataAccess betlistDataAccess)
        {
            _betlistDataAccess = betlistDataAccess ?? new BetListDataAccess();
        }

        public TicketResult GetBetList(BetListCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetList(criteria);
        }

        public TicketResult GetBetListParlay(BetListParlayCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListParlay(criteria);
        }

        public SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetSystemParlayDetail(criteria);
        }

        public void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _betlistDataAccess.GetBalanceUpMoveBetDate(criteria);
        }

        public CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername)
        {
            return _betlistDataAccess.GetPlayerIbcInfo(affiliateGroup, playerUsername);
        }

        public TicketResult GetBetListDetail(BetListDetailCriteria criteria, string spkey)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListDetail(criteria, spkey);
        }

        public CashOutTicketDetail GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListCashOutDetail(criteria, spkey);
        }
    }
}