// ----------------------------------------------------------------------
// <copyright file="IResultService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Result
{
    using System.Collections.Generic;
    using Criteria.BetList;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

    public interface IResultService
    {
        IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria);

        IList<LiveCasino> GetLiveCasinoResults(int matchId);

        IList<NumberGame> GetNumberGameResults(int matchId);

        IList<Racing> GetRacingResults(int matchId, int raceNo);

        IList<Parlay> GetParlayResults(long refNo);

        void ValidatePermission(ValidationCriteria criteria);
    }
}