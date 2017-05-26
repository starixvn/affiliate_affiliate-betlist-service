// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using System.Collections.Generic;
    using System.Linq;
    using BetList.Core.Entities;
    using Criteria.BetList;
    using DataAccess.Betlist;
    using DTO.BetList;
    using Fanex.Common.Helper;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

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

        #region BetList

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

        #endregion BetList

        #region BetList.Results

        public IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria)
        {
            return _betlistDataAccess.GetSportsbookResults(criteria).ToList();
        }

        public IList<LiveCasino> GetLiveCasinoResults(int matchId)
        {
            return _betlistDataAccess.GetLiveCasinoResults(matchId).ToList();
        }

        public IList<NumberGame> GetNumberGameResults(int matchId)
        {
            return _betlistDataAccess.GetNumberGameResults(matchId).ToList();
        }

        public IList<Racing> GetRacingResults(int matchId, int raceNo)
        {
            return _betlistDataAccess.GetRacingResults(matchId, raceNo).ToList();
        }

        public IList<Parlay> GetParlayResults(long refNo)
        {
            return _betlistDataAccess.GetParlayResults(refNo).ToList();
        }

        public void ValidatePermission(ValidationCriteria criteria)
        {
            _betlistDataAccess.ValidatePermission(criteria);
        }

        #endregion BetList.Results
    }
}