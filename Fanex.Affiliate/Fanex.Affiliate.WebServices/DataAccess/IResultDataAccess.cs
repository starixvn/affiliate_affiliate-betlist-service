// ----------------------------------------------------------------------
// <copyright file="IResultDataAccess.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using System.Collections.Generic;
    using Criteria.BetList;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

    public interface IResultDataAccess
    {
        IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria);

        IEnumerable<LiveCasino> GetLiveCasinoResults(int matchId);

        IEnumerable<NumberGame> GetNumberGameResults(int matchId);

        IEnumerable<Racing> GetRacingResults(int matchId, int raceNo);

        IEnumerable<Parlay> GetParlayResults(long refNo);
    }
}