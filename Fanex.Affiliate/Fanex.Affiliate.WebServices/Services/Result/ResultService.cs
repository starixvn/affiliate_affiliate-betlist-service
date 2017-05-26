// ----------------------------------------------------------------------
// <copyright file="ResultService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using System.Collections.Generic;
    using System.Linq;
    using Criteria.BetList;
    using DataAccess;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Result;

    public class ResultService : IResultService
    {
        private readonly IResultDataAccess _resultDataAccess;

        public ResultService()
            : this(null)
        {
        }

        private ResultService(IResultDataAccess resultDataAccess)
        {
            _resultDataAccess = resultDataAccess ?? new ResultDataAccess();
        }

        public IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria)
        {
            return _resultDataAccess.GetSportsbookResults(criteria).ToList();
        }

        public IList<LiveCasino> GetLiveCasinoResults(int matchId)
        {
            return _resultDataAccess.GetLiveCasinoResults(matchId).ToList();
        }

        public IList<NumberGame> GetNumberGameResults(int matchId)
        {
            return _resultDataAccess.GetNumberGameResults(matchId).ToList();
        }

        public IList<Racing> GetRacingResults(int matchId, int raceNo)
        {
            return _resultDataAccess.GetRacingResults(matchId, raceNo).ToList();
        }

        public IList<Parlay> GetParlayResults(long refNo)
        {
            return _resultDataAccess.GetParlayResults(refNo).ToList();
        }
    }
}