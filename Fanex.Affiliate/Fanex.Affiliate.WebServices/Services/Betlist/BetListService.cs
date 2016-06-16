// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/05">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using BetList.Core.Entities;
    using Common.Helper;
    using Criteria;
    using DataAccess;
    using DTO.BetList;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

    /// <summary>
    /// Account Service.
    /// </summary>
    public class BetListService : QueryService, IBetListService
    {
        #region Fields

        /// <summary>
        /// Define Account DataAccess.
        /// </summary>
        private readonly IBetListDataAccess _betlistDataAccess;

        private readonly string _subDomainPath = string.Empty;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListService" /> class.
        /// </summary>
        public BetListService()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BetListService" /> class.
        /// </summary>
        /// <param name="betlistDataAccess">Account data access.</param>
        private BetListService(IBetListDataAccess betlistDataAccess)
        {
            _betlistDataAccess = betlistDataAccess ?? new BetListDataAccess();
        }

        #endregion Constructor
        
        #region BetList

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetList(BetListCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetList(criteria);
        }

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="criteria">Bet list parlay.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetListParlay(BetListParlayCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListParlay(criteria);
        }

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        public SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetSystemParlayDetail(criteria);
        }

        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Balance up and move bet criteria.</param>
        public void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _betlistDataAccess.GetBalanceUpMoveBetDate(criteria);
        }

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate group.</param>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        public CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername)
        {
            return _betlistDataAccess.GetPlayerIbcInfo(affiliateGroup, playerUsername);
        }

        #endregion

        #region BetList.Results

        /// <summary>
        /// Get Sports-book results.
        /// </summary>
        /// <param name="criteria">Bet List Results Criteria.</param>
        /// <returns>The Sports-book results.</returns>
        public IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria)
        {
            return _betlistDataAccess.GetSportsbookResults(criteria).ToList();
        }

        /// <summary>
        /// Get LiveCasino's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The LiveCasino's results.</returns>
        public IList<LiveCasino> GetLiveCasinoResults(int matchId)
        {
            return _betlistDataAccess.GetLiveCasinoResults(matchId).ToList();
        }

        /// <summary>
        /// Get NumberGame's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The NumberGame's results.</returns>
        public IList<NumberGame> GetNumberGameResults(int matchId)
        {
            return _betlistDataAccess.GetNumberGameResults(matchId).ToList();
        }

        /// <summary>
        /// Get Racing's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <param name="raceNo">Race Number value.</param>
        /// <returns>The Racing's results.</returns>
        public IList<Racing> GetRacingResults(int matchId, int raceNo)
        {
            return _betlistDataAccess.GetRacingResults(matchId, raceNo).ToList();
        }

        /// <summary>
        /// Get Parlay's results.
        /// </summary>
        /// <param name="refNo">Reference Number value.</param>
        /// <returns>The Parlay's results.</returns>
        public IList<Parlay> GetParlayResults(long refNo)
        {
            return _betlistDataAccess.GetParlayResults(refNo).ToList();
        }

        public void ValidatePermission(ValidationCriteria criteria)
        {
            _betlistDataAccess.ValidatePermission(criteria);
        }

        #endregion

        #region BetList.Results.RNGCasino
        
        #endregion
        
        #region BetList.Results.RNGCasino.Utilities
        
        #endregion
    }
}