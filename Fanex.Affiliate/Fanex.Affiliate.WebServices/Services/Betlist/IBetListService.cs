// ----------------------------------------------------------------------
// <copyright file="IBetListService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.Services
{
    using System.Collections.Generic;
    using BetList.Core.Entities;
    using Criteria;
    using DTO.BetList;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

    /// <summary>
    /// Interface Account service.
    /// </summary>
    public interface IBetListService
    {
        #region BetList

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        TicketResult GetBetList(BetListCriteria criteria);

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="criteria">Bet list parlay.</param>
        /// <returns>Ticket results.</returns>
        TicketResult GetBetListParlay(BetListParlayCriteria criteria);

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria);
        
        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria);

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate group.</param>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername);

        #endregion

        #region BetList.Results

        /// <summary>
        /// Get Sports-book results.
        /// </summary>
        /// <param name="criteria">Bet List Results Criteria.</param>
        /// <returns>The Sports-book results.</returns>
        IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria);

        /// <summary>
        /// Get LiveCasino's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The LiveCasino's results.</returns>
        IList<LiveCasino> GetLiveCasinoResults(int matchId);

        /// <summary>
        /// Get NumberGame's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The NumberGame's results.</returns>
        IList<NumberGame> GetNumberGameResults(int matchId);

        /// <summary>
        /// Get Racing's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <param name="raceNo">Race Number value.</param>
        /// <returns>The Racing's results.</returns>
        IList<Racing> GetRacingResults(int matchId, int raceNo);

        /// <summary>
        /// Get Parlay's results.
        /// </summary>
        /// <param name="refNo">Reference Number value.</param>
        /// <returns>The Parlay's results.</returns>
        IList<Parlay> GetParlayResults(long refNo);

        #endregion

        #region BetList.Results.RNGCasino

        #endregion

        #region Constraint Permission

        void ValidatePermission(ValidationCriteria criteria);

        #endregion
    }
}