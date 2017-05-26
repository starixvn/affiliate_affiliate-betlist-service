// ----------------------------------------------------------------------
// <copyright file="CashOutService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using Common.Helper;
    using Criteria.BetList;
    using DTO.BetList;
    using Fanex.Affiliate.WebServices.DataAccess;

    public class CashOutService : ICashOutService
    {
        private readonly IBetListDataAccess _betlistDataAccess;

        public CashOutService()
            : this(null)
        {
        }

        private CashOutService(IBetListDataAccess betlistDataAccess)
        {
            _betlistDataAccess = betlistDataAccess ?? new BetListDataAccess();
        }

        public CashOutTicketDetail GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListCashOutDetail(criteria, spkey);
        }
    }
}