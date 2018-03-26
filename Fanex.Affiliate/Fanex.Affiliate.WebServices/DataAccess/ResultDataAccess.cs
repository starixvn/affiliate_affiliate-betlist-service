namespace Fanex.Affiliate.WebServices.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using Criteria.BetList;
    using Data;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Utilities;

    public class ResultDataAccess : IResultDataAccess
    {
        public IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria)
        {
            var results = new List<Sportsbook>();
            var gameDetails = new List<GameDetail>();
            using (var db = new ObjectDb("Aff_BetList_SelMatchResult"))
            {
                using (var reader = db.ExecuteReader(new { matchid = criteria.MatchId, bettype = criteria.Bettype, betid = criteria.BetId }))
                {
                    while (reader.Read())
                    {
                        var item = BindSportsbook(reader);
                        item.Bettype = criteria.Bettype;
                        item.BetID = criteria.BetId;
                        item.Handicap2 = criteria.Handicap2;
                        results.Add(item);
                    }

                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            gameDetails.Add(BindGameDetail(reader));
                        }
                    }
                }
            }

            if (gameDetails.Count > 0)
            {
                foreach (var result in results)
                {
                    result.GameDetails.AddRange(gameDetails.Where(i => i.MatchId == result.MatchId));
                }
            }

            return results;
        }

        public IEnumerable<LiveCasino> GetLiveCasinoResults(int matchId)
        {
            var results = new List<LiveCasino>();
            using (var db = new ObjectDb("Aff_BetList_SelMatchResult"))
            {
                using (var reader = db.ExecuteReader(new { matchid = matchId }))
                {
                    while (reader.Read())
                    {
                        results.Add(BindLiveCasino(reader));
                    }
                }
            }

            return results;
        }

        public IEnumerable<NumberGame> GetNumberGameResults(int matchId)
        {
            var results = new List<NumberGame>();
            using (var db = new ObjectDb("Aff_BetList_SelMatchResultNumberGame"))
            {
                using (var reader = db.ExecuteReader(new { matchid = matchId }))
                {
                    while (reader.Read())
                    {
                        BindNumberGame(results, reader);
                    }
                }
            }

            return results;
        }

        public IEnumerable<Racing> GetRacingResults(int matchId, int raceNo)
        {
            var param = new
            {
                matchId,
                raceno = raceNo > -1 ? raceNo.ToString(CultureInfo.InvariantCulture) : null
            };

            var results = new List<Racing>();
            using (var db = new ObjectDb("Aff_BetList_SelMatchResult"))
            {
                using (var reader = db.ExecuteReader(param))
                {
                    while (reader.Read())
                    {
                        results.Add(BindRacing(reader));
                    }
                }
            }

            return results;
        }

        public IEnumerable<Parlay> GetParlayResults(long refNo)
        {
            var results = new List<Parlay>();
            var gameDetails = new List<GameDetail>();

            using (var db = new ObjectDb("Aff_BetList_SelMatchResultParlay"))
            {
                using (var reader = db.ExecuteReader(new { refno = refNo }))
                {
                    var leagueId = 0;
                    var sportId = 0;
                    IList<Sportsbook> sportbooks = new List<Sportsbook>();
                    while (reader.Read())
                    {
                        var newLeagueId = int.Parse(reader["LeagueID"].ToString());
                        var newsportId = int.Parse(reader["sporttype"].ToString());

                        if (leagueId != newLeagueId && leagueId > 0)
                        {
                            results.Add(new Parlay
                            {
                                SportId = sportId,
                                LeagueId = leagueId,
                                Sportbook = sportbooks,
                            });
                            sportbooks = new List<Sportsbook>();
                        }

                        sportbooks.Add(BindSportsbook(reader));

                        leagueId = newLeagueId;
                        sportId = newsportId;
                    }

                    /* add the latest row*/
                    results.Add(new Parlay
                    {
                        SportId = sportId,
                        LeagueId = leagueId,
                        Sportbook = sportbooks
                    });
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            gameDetails.Add(BindGameDetail(reader));
                        }
                    }

                    if (gameDetails.Count > 0)
                    {
                        foreach (var result in results)
                        {
                            foreach (var sb in result.Sportbook)
                            {
                                sb.GameDetails.AddRange(gameDetails.Where(i => i.MatchId == sb.MatchId));
                            }
                        }
                    }
                }
            }

            return results;
        }

        private static Sportsbook BindSportsbook(IDataReader reader)
        {
            var item = new Sportsbook
            {
                AwayId = int.Parse(reader["awayID"].ToString()),
                HomeId = int.Parse(reader["homeID"].ToString()),
                BetStatus = reader.GetStringOrDefault("betstatus"),
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                MatchId = int.Parse(reader["matchid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore = reader.GetNullableInt("finalawayscore"),
                HtAwayScore = reader.GetNullableInt("htawayscore"),
                Color = reader.GetStringOrDefaultSafely("lcolor"),
                FinalHomeScore = reader.GetNullableInt("finalhomescore"),
                HtHomeScore = reader.GetNullableInt("hthomescore"),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsNeutral = bool.Parse(reader["IsNeutral"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                MatchCode = reader["matchcode"].ToString(),
                GoalRecord = reader["goalrecord"].ToString(),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                RefNo = string.Empty,
                GameDetails = new List<GameDetail>(),
                Bettype = reader.GetIntSafely("bettype"),
                BetID = reader.GetStringOrDefaultSafely("betcheck"),
                LiveAwayScore = reader.GetNullableIntSafely("LiveAwayScore"),
                LiveHomeScore = reader.GetNullableIntSafely("LiveHomeScore")
            };

            return item;
        }

        private static LiveCasino BindLiveCasino(IDataReader reader)
        {
            return new LiveCasino
            {
                ShowTime = reader["showtime"].ToString(),
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore = reader.GetNullableInt("finalawayscore"),
                FinalHomeScore = reader.GetNullableInt("finalhomescore"),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                BetStatus = reader.GetStringOrDefault("betstatus"),
                Bettype = int.Parse(reader["homeid2"].ToString())
            };
        }

        private static Racing BindRacing(IDataReader reader)
        {
            return new Racing
            {
                LeagueId = int.Parse(reader["meetingid"].ToString()),
                EngineId = reader["HorseId"].ToString(),
                EngineNo = reader["HorseNo"].ToString(),
                IsFavor = reader.GetStringOrDefault("IsFavor").Equals("1"),
                IsSCR = reader.GetStringOrDefault("IsSCR").Equals("1"),
                Place = int.Parse(reader["place"].ToString()),
                PlaceOdds = decimal.Parse(reader["placeodds"].ToString()),
                WinOdds = decimal.Parse(reader["odds"].ToString()),
                RaceName = reader.GetInt("racename"),
                KickOffTime = reader["kickofftime"] != DBNull.Value ? DateTime.Parse(reader["kickofftime"].ToString()) : DateTime.MinValue,
                Favourite = reader.GetStringOrDefault("IsFavor"),
                RowCount = int.Parse(reader["rowcount"].ToString()),
                SCROfMatch = reader["SCR"].ToString(),
                TFOfMatch = reader["TF"].ToString()
            };
        }

        private static void BindNumberGame(List<NumberGame> result, IDataReader reader)
        {
            var matchId = int.Parse(reader["matchid"].ToString());
            var existMatch = result.SingleOrDefault(i => i.MatchId == matchId);
            if (existMatch != null)
            {
                existMatch.Seg.Add(int.Parse(reader["seq"].ToString()));
                existMatch.Ball.Add(int.Parse(reader["number"].ToString()));
            }
            else
            {
                result.Add(new NumberGame
                {
                    LeagueId = int.Parse(reader["leagueid"].ToString()),
                    MatchId = int.Parse(reader["matchid"].ToString()),
                    EventStatus = reader["eventstatus"].ToString(),
                    EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                    IsPending = Convert.ToBoolean(reader["isrefund"]),
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seg = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) }
                });
            }
        }

        private static GameDetail BindGameDetail(IDataReader reader)
        {
            return new GameDetail
            {
                MatchId = int.Parse(reader["matchid"].ToString()),
                ResourceId = reader["ResourceId"].ToString(),
                HomeValue = reader["HomeValue"].ToString(),
                AwayValue = reader["AwayValue"].ToString(),
                ResultStatusID = reader.GetInt("ResultStatusID"),
                SettlementStatusID = reader.GetInt("SettlementStatusID"),
                OtherInfo = reader["OtherInfo"].ToString()
            };
        }
    }
}