namespace Fanex.Affiliate.WebServices.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using Criteria.BetList;
    using Data;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

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

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan",
     Justification = "Reviewed. Suppression is OK here.")]
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

        [SuppressMessage("SonarQube", "S1541:Methods and properties should not be too complex", Justification = "Reviewed")]
        private static Sportsbook BindSportsbook(IDataReader reader)
        {
            var item = new Sportsbook
            {
                AwayId = int.Parse(reader["awayID"].ToString()),
                HomeId = int.Parse(reader["homeID"].ToString()),
                BetStatus =
                    reader["betstatus"] != DBNull.Value ? reader["betstatus"].ToString() : string.Empty,
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                MatchId = int.Parse(reader["matchid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore =
                    reader["finalawayscore"] != DBNull.Value
                        ? int.Parse(reader["finalawayscore"].ToString())
                        : new int?(),
                HtAwayScore =
                    reader["htawayscore"] != DBNull.Value ? int.Parse(reader["htawayscore"].ToString()) : new int?(),
                Color = ColumnExisted(reader, "lcolor") && reader["lcolor"] != DBNull.Value ? reader["lcolor"].ToString() : string.Empty,
                FinalHomeScore =
                    reader["finalhomescore"] != DBNull.Value
                        ? int.Parse(reader["finalhomescore"].ToString())
                        : new int?(),
                HtHomeScore =
                    reader["hthomescore"] != DBNull.Value
                        ? int.Parse(reader["hthomescore"].ToString())
                        : new int?(),
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
                Bettype = GetBetTypeFromToken(reader),
                BetID = GetBetIDFromToken(reader),
                LiveAwayScore = ColumnExisted(reader, "LiveAwayScore") && reader["LiveAwayScore"] != DBNull.Value ? Convert.ToInt32(reader["LiveAwayScore"]) : new int?(),
                LiveHomeScore = ColumnExisted(reader, "LiveHomeScore") && reader["LiveHomeScore"] != DBNull.Value ? Convert.ToInt32(reader["LiveHomeScore"]) : new int?()
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
                FinalAwayScore =
                    reader["finalawayscore"] != DBNull.Value ? int.Parse(reader["finalawayscore"].ToString()) : new int?(),
                FinalHomeScore =
                    reader["finalhomescore"] != DBNull.Value ? int.Parse(reader["finalhomescore"].ToString()) : new int?(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                BetStatus = reader["betstatus"] != DBNull.Value ? reader["betstatus"].ToString() : string.Empty,
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
                IsFavor = reader["IsFavor"] != DBNull.Value && reader["IsFavor"].ToString().Equals("1"),
                IsSCR = reader["IsSCR"] != DBNull.Value && reader["IsSCR"].ToString().Equals("1"),
                Place = int.Parse(reader["place"].ToString()),
                PlaceOdds = decimal.Parse(reader["placeodds"].ToString()),
                WinOdds = decimal.Parse(reader["odds"].ToString()),
                RaceName = reader["racename"] != DBNull.Value ? int.Parse(reader["racename"].ToString()) : 0,
                KickOffTime = reader["kickofftime"] != DBNull.Value ? DateTime.Parse(reader["kickofftime"].ToString()) : DateTime.MinValue,
                Favourite = reader["IsFavor"] != DBNull.Value ? reader["IsFavor"].ToString() : string.Empty,
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

        private static bool ColumnExisted(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static GameDetail BindGameDetail(IDataReader reader)
        {
            var resultStatusId = reader["ResultStatusID"];
            var settlementStatusID = reader["SettlementStatusID"];

            return new GameDetail
            {
                MatchId = int.Parse(reader["matchid"].ToString()),
                ResourceId = reader["ResourceId"].ToString(),
                HomeValue = reader["HomeValue"].ToString(),
                AwayValue = reader["AwayValue"].ToString(),
                ResultStatusID = resultStatusId == DBNull.Value ? 0 : int.Parse(resultStatusId.ToString()),
                SettlementStatusID = settlementStatusID == DBNull.Value ? 0 : int.Parse(settlementStatusID.ToString()),
                OtherInfo = reader["OtherInfo"].ToString()
            };
        }

        private static int GetBetTypeFromToken(IDataReader reader)
        {
            const string BettypeColumn = "bettype";
            if (ColumnExisted(reader, BettypeColumn))
            {
                return int.Parse(reader[BettypeColumn].ToString());
            }

            return 0;
        }

        private static string GetBetIDFromToken(IDataReader reader)
        {
            const string BetcheckColumn = "betcheck";
            if (ColumnExisted(reader, BetcheckColumn))
            {
                return reader[BetcheckColumn].ToString();
            }

            return string.Empty;
        }
    }
}