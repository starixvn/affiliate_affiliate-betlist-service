// ----------------------------------------------------------------------
// <copyright file="ResultService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Result
{
    using System.Collections.Generic;
    using System.Linq;
    using Criteria.BetList;
    using DataAccess;
    using Fanex.Result.Core;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

    public class ResultService : IResultService
    {
        private readonly IResultDataAccess _resultDataAccess;

        private static readonly IEnumerable<SportType> _racingSportTypes = new List<SportType>
        {
            SportType.HorseRacing, SportType.Greyhound, SportType.Harness,
            SportType.VirtualHorseRacing, SportType.VirtualGreyhounds, SportType.VirtualMotorRacing,
            SportType.VirtualSpeedway, SportType.VirtualCycling
        };

        public ResultService()
            : this(null)
        {
        }

        private ResultService(IResultDataAccess resultDataAccess)
        {
            _resultDataAccess = resultDataAccess ?? new ResultDataAccess();
        }

        public string GetResultHtml(
            BetListResultsCriteria criteria,
            SportType sportType,
            ResultSetting setting)
        {
            if (_racingSportTypes.Contains(sportType))
            {
                if (criteria.RaceNo == null)
                {
                    return string.Empty;
                }

                var dataRc = GetRacingResults(criteria.MatchId, criteria.RaceNo.Value);
                return ResultEngine.HtmlParser(dataRc, setting);
            }
            else if (sportType == SportType.LiveCasino)
            {
                var dataLc = GetLiveCasinoResults(criteria.MatchId);
                return ResultEngine.HtmlParser(dataLc, setting);
            }
            else if (sportType == SportType.NumberGame)
            {
                var dataNg = GetNumberGameResults(criteria.MatchId);
                return ResultEngine.HtmlParser(dataNg, setting);
            }
            else
            {
                var dataSb = GetSportsbookResults(criteria);
                return ResultEngine.HtmlParser(dataSb, setting);
            }
        }

        public string GetParlayResultHtml(long refNo, ResultSetting setting)
        {
            var data = _resultDataAccess.GetParlayResults(refNo).ToList();
            return ResultEngine.HtmlParser(data, setting);
        }

        private IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria)
        {
            return _resultDataAccess.GetSportsbookResults(criteria).ToList();
        }

        private IList<LiveCasino> GetLiveCasinoResults(int matchId)
        {
            return _resultDataAccess.GetLiveCasinoResults(matchId).ToList();
        }

        private IList<NumberGame> GetNumberGameResults(int matchId)
        {
            return _resultDataAccess.GetNumberGameResults(matchId).ToList();
        }

        private IList<Racing> GetRacingResults(int matchId, int raceNo)
        {
            return _resultDataAccess.GetRacingResults(matchId, raceNo).ToList();
        }
    }
}