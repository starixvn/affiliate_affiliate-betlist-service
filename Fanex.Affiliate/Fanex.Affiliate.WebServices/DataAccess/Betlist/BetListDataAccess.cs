// ----------------------------------------------------------------------
// <copyright file="BetListDataAccess.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/12/03">Created</change>
//   <change who="" date=""></change>
// </history>

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using BetList.Core.Entities;
    using Common;
    using Criteria;
    using Data;
    using DTO.BetList;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class BetListDataAccess : IBetListDataAccess
    {
        #region BetList

        /// <summary>
        /// Read main ticket data.
        /// </summary>
        /// <param name="dataTable">Data table Parameter value.</param>
        /// <param name="baseCurrency"> Parameter value.</param>
        /// <returns>Result value.</returns>
        [SuppressMessage(
            "StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage(
            "Microsoft.Maintainability",
            "CA1502:AvoidExcessiveComplexity",
            Justification = "Reviewed. Suppression is OK here.")]
        public static List<AffTicket> ReadMainTicketData(DataTable dataTable, string baseCurrency)
        {
            var playerCommCol = "playercomm" + baseCurrency;
            var commCol = "comm" + baseCurrency;
            var licenseeWlCol = "licenseewl" + baseCurrency;
            var actualStakeCol = "actual_stake" + baseCurrency;

            var columnCollection = dataTable.Columns;

            // String
            var containsUsername = columnCollection.Contains("username");
            var containsRefNo = columnCollection.Contains("refno");
            var containsMatchCode = columnCollection.Contains("matchcode");
            var containsStatus = columnCollection.Contains("status");
            var containsBetTeam = columnCollection.Contains("betteam");
            var containsBetStatus = columnCollection.Contains("betstatus");
            var containsShowTime = columnCollection.Contains("showtime");
            var containsOddsType = columnCollection.Contains("oddstype");
            var containsExternalId = columnCollection.Contains("nickname");
            var containsRace = columnCollection.Contains("race");

            // Boolean
            var containsLiveIndicator = columnCollection.Contains("liveindicator");
            var containsNeutral = columnCollection.Contains("isneutral");

            // Int
            var containsSportType = columnCollection.Contains("sporttype");
            var containsBetType = columnCollection.Contains("bettype");
            var containsLeagueId = columnCollection.Contains("leagueid");
            var containsHomeId = columnCollection.Contains("homeid");
            var containsAwayId = columnCollection.Contains("awayid");
            var containsMatchId = columnCollection.Contains("matchid");
            var containsCustId = columnCollection.Contains("custid");
            var containsCurrencyId = columnCollection.Contains("currencyid");
            var containsLiveHomeScore = columnCollection.Contains("livehomescore");
            var containsLiveAwayScore = columnCollection.Contains("liveawayscore");
            var containsStatusID = columnCollection.Contains("statusid");

            // Long
            var containsTransId = columnCollection.Contains("transid");
            var containsBetId = columnCollection.Contains("betid");

            // DateTime
            var containsWinlostDate = columnCollection.Contains("winlostdate");
            var containsTransDate = columnCollection.Contains("transdate");

            // Money
            var containsPlayerComm = columnCollection.Contains(playerCommCol);
            var containsComm = columnCollection.Contains(commCol);
            var containsLicenseeWl = columnCollection.Contains(licenseeWlCol);
            var containsWinlost = columnCollection.Contains("winlost");
            var containsStake = columnCollection.Contains("stake");
            var containsActualStake = columnCollection.Contains(actualStakeCol);
            var containsOdds = columnCollection.Contains("odds");
            var containsMasterOdds = columnCollection.Contains("modds");
            var containsHandicap1 = columnCollection.Contains("hdp1");
            var containsHandicap2 = columnCollection.Contains("hdp2");
            var containsBetCheck = columnCollection.Contains("betcheck");

            var result = new List<AffTicket>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                var ticket = new AffTicket();

                // String
                ticket.UserName = containsUsername ? Converter.ToString2(dataRow["username"]) : string.Empty;
                ticket.RefNo = containsRefNo ? Converter.ToString2(dataRow["refno"]) : string.Empty;
                ticket.MatchCode = containsMatchCode ? Converter.ToString2(dataRow["matchcode"]) : string.Empty;
                ticket.Status = containsStatus ? Converter.ToString2(dataRow["status"]) : string.Empty;
                ticket.BetTeam = containsBetTeam ? Converter.ToString2(dataRow["betteam"]) : string.Empty;
                ticket.EventStatus = containsBetStatus ? Converter.ToString2(dataRow["betstatus"]) : string.Empty;
                ticket.ShowTime = containsShowTime ? Converter.ToString2(dataRow["showtime"]) : string.Empty;
                ticket.OddsType = containsOddsType ? Converter.ToString2(dataRow["oddstype"]) : "0";
                ticket.ExternalId = containsExternalId ? Converter.ToString2(dataRow["nickname"]) : string.Empty;
                ticket.Race = containsRace ? Converter.ToString2(dataRow["race"]) : string.Empty;

                // Boolean
                ticket.IsLive = containsLiveIndicator ? ReadBoolean(dataRow["liveindicator"]) : false;
                ticket.IsNeutral = containsNeutral ? ReadBoolean(dataRow["isneutral"]) : false;

                // Int
                ticket.SportTypeId = containsSportType ? Converter.ToInt(dataRow["sporttype"]) : 0;
                ticket.BetTypeId = containsBetType ? Converter.ToInt(dataRow["bettype"]) : 0;
                ticket.LeagueId = containsLeagueId ? Converter.ToInt(dataRow["leagueid"]) : 0;
                ticket.HomeId = containsHomeId ? Converter.ToInt(dataRow["homeid"]) : 0;
                ticket.AwayId = containsAwayId ? Converter.ToInt(dataRow["awayid"]) : 0;
                ticket.MatchId = containsMatchId ? Converter.ToInt(dataRow["matchid"]) : 0;
                ticket.CustId = containsCustId ? Converter.ToInt(dataRow["custid"]) : 0;
                ticket.CurrencyId = containsCurrencyId ? Converter.ToInt(dataRow["currencyid"]) : 0;
                ticket.LiveHomeScore = containsLiveHomeScore ? Converter.ToInt(dataRow["livehomescore"]) : 0;
                ticket.LiveAwayScore = containsLiveAwayScore ? Converter.ToInt(dataRow["liveawayscore"]) : 0;
                ticket.StatusId = containsStatusID ? Converter.ToInt(dataRow["statusid"]) : 0;

                // Long
                ticket.TransId = containsTransId ? Convert.ToInt64(dataRow["transid"]) : 0;
                ticket.BetId = containsBetId && !Convert.IsDBNull(dataRow["betid"]) ? new long?(Convert.ToInt64(dataRow["betid"])) : null;

                // DateTime
                ticket.WinlostDate = containsWinlostDate ? Converter.ToDateTime(dataRow["winlostdate"]) : DateTime.Now;
                ticket.TransDate = containsTransDate ? Converter.ToDateTime(dataRow["transdate"]) : DateTime.Now;

                // Money
                ticket.PlayerComm = containsPlayerComm ? Converter.ToDecimal(dataRow[playerCommCol]) : 0;
                ticket.Comm = containsComm ? Converter.ToDecimal(dataRow[commCol]) : 0;
                ticket.LicenseeWl = containsLicenseeWl ? Converter.ToDecimal(dataRow[licenseeWlCol]) : 0;
                ticket.Winlost = containsWinlost ? Converter.ToDecimal(dataRow["winlost"]) : 0;
                ticket.Stake = containsStake ? Converter.ToDecimal(dataRow["stake"]) : 0;
                ticket.ActualStake = containsActualStake ? Converter.ToDecimal(dataRow[actualStakeCol]) : 0;
                ticket.Odds = containsOdds ? Converter.ToDecimal(dataRow["odds"]) : 0;
                ticket.MasterOdds = containsMasterOdds ? Converter.ToDecimal(dataRow["modds"]) : 0;
                ticket.Handicap1 = containsHandicap1 ? Converter.ToDecimal(dataRow["hdp1"]) : 0;
                ticket.Handicap2 = containsHandicap2 ? Converter.ToDecimal(dataRow["hdp2"]) : 0;
                ticket.BetCheck = containsBetCheck ? Converter.ToString2(dataRow["BetCheck"]) : string.Empty;

                result.Add(ticket);
            }

            return result;
        }

        /// <summary>
        /// Read reference ticket.
        /// </summary>
        /// <param name="data">Data Parameter value.</param>
        /// <param name="baseCurrency"> Parameter value.</param>
        /// <returns>Result value.</returns>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Reviewed.")]
        public static List<ITicketData> ReadReferneceTicket(DataTable data, string baseCurrency)
        {
            var result = new List<ITicketData>();

            var containsBetTeam = data.Columns.Contains("betteam");
            var containsStake = data.Columns.Contains("stake");
            foreach (DataRow dataRow in data.Rows)
            {
                var ticket = new TicketData
                {
                    BetTeam = containsBetTeam ? dataRow["betteam"].ToString() : string.Empty,
                    RefNo = dataRow["refno"].ToString(),
                    Status = dataRow["status"].ToString(),
                    BetTypeId = Convert.ToInt32(dataRow["bettype"]),
                    Stake = containsStake ? Convert.ToDecimal(dataRow["stake" + baseCurrency]) : 0,
                    Odds = Convert.ToDecimal(dataRow["odds"])
                };

                result.Add(ticket);
            }

            return result;
        }

        /// <summary>
        /// Read boolean object.
        /// </summary>
        /// <param name="value"> Parameter value.</param>
        /// <returns>Result value.</returns>
        public static bool ReadBoolean(object value)
        {
            if (value == null || value == Convert.DBNull)
            {
                return false;
            }

            if (value.GetType() == typeof(bool))
            {
                return Convert.ToBoolean(value);
            }

            if (value.GetType() == typeof(int))
            {
                return Convert.ToInt32(value) > 0;
            }

            return false;
        }

        /// <summary>
        /// Get Bet List.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetList(BetListCriteria criteria)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var ds = objectDb.ExecuteDataSet(param);
                var baseCurrency = string.Empty;

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = ReadMainTicketData(mainTable, baseCurrency);

                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = ReadReferneceTicket(referenceTable, baseCurrency);

                result.TotalRows = Converter.ToInt(param["totalrows"]);
            }

            return result;
        }

        /// <summary>
        /// Get Bet List Parlay.
        /// </summary>
        /// <param name="criteria">Bet list parlay.</param>
        /// <returns>Ticket results.</returns>
        public TicketResult GetBetListParlay(BetListParlayCriteria criteria)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var ds = objectDb.ExecuteDataSet(param);

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = ReadMainTicketData(mainTable, string.Empty);

                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = ReadReferneceTicket(referenceTable, string.Empty);
            }

            return result;
        }

        /// <summary>
        /// Get System Parlay Detail data.
        /// </summary>
        /// <param name="criteria">The metal model.</param>
        /// <returns>The system parlay.</returns>
        public SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var dataSet = objectDb.ExecuteDataSet(param);
                var ticketData = dataSet.Tables.Count > 0 ? dataSet.Tables[0] : new DataTable();
                var serialData = dataSet.Tables.Count > 1 ? dataSet.Tables[1] : new DataTable();
                var systemParlayData = new SystemParlayData();

                systemParlayData.TicketList = new List<ISystemParlayTicket>();
                foreach (DataRow row in ticketData.Rows)
                {
                    var ticket = new SystemParlayTicket
                    {
                        Comm = Converter.ToDecimal(row["comm"]),
                        MOdds = Converter.ToDecimal(row["modds"]),
                        PlayerComm = Converter.ToDecimal(row["playercomm"]),
                        Serial = Convert.ToInt64(row["serial"]),
                        Stake = Converter.ToDecimal(row["stake"]),
                        Status = Converter.ToString2(row["status"]),
                        StatusId = Converter.ToInt(row["statusid"]),
                        TransId = Convert.ToInt64(row["transid"]),
                        WinLost = Converter.ToDecimal(row["winlost"]),
                        WinLostDate = Converter.ToDateTime(row["winlostdate"])
                    };

                    systemParlayData.TicketList.Add(ticket);
                }

                systemParlayData.SerialList = new List<ISystemParlaySerial>();
                foreach (DataRow row in serialData.Rows)
                {
                    var serial = new SystemParlaySerial
                    {
                        Serial = Convert.ToInt64(row["serial"]),
                        HomeId = Converter.ToInt(row["homeid"]),
                        AwayId = Converter.ToInt(row["awayid"])
                    };

                    systemParlayData.SerialList.Add(serial);
                }

                systemParlayData.Outstanding = Converter.ToDecimal(param["outstanding"]);
                return systemParlayData;
            }
        }

        /// <summary>
        /// Get balance up and move bet dates.
        /// </summary>
        /// <param name="criteria">Input criteria.</param>
        public void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria)
        {
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var dataSet = objectDb.ExecuteDataSet(param);
                criteria.BalanceUpDay = DateTime.Parse(dataSet.Tables[0].Rows[0]["BalanceUpDay"].ToString());
                criteria.MoveBetsDay = DateTime.Parse(dataSet.Tables[0].Rows[0]["MoveBetsDay"].ToString());
            }
        }

        /// <summary>
        /// Get player's username.
        /// </summary>
        /// <param name="affiliateGroup">Affiliate group.</param>
        /// <param name="playerUsername">Affiliate username.</param>
        /// <returns>Return player's Info.</returns>
        public CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername)
        {
            var hashParam = new Hashtable
            {
                { "username", playerUsername },
                { "custid", 0 },
                { "ibcusername", string.Empty },
                { "currencyid", 0 }
            };
            var objectDb = new ObjectDb("Aff_BetList_SelDummyAccID");
            objectDb.ExecuteNonQuery(hashParam);

            return new CustomerInfoBetList
            {
                CustId = int.Parse(hashParam["custid"].ToString()),
                Username = playerUsername,
                IBCUsername = hashParam["ibcusername"].ToString(),
                CurrencyId = int.Parse(hashParam["currencyid"].ToString())
            };
        }

        #endregion BetList

        #region Betlist.Results

        /// <summary>
        /// Get Sports-book results.
        /// </summary>
        /// <param name="criteria">Bet List Results Criteria.</param>
        /// <returns>The Sports-book results.</returns>
        public IList<Sportsbook> GetSportsbookResults(BetListResultsCriteria criteria)
        {
            var results = new List<Sportsbook>();
            var gameDetails = new List<GameDetail>();
            using (var db = new ObjectDb("Aff_BetList_SelMatchResult"))
            {
                using (var reader = db.ExecuteReader(new { matchid = criteria.MatchId, bettype = criteria.Bettype }))
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

        /// <summary>
        /// Get LiveCasino's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The LiveCasino's results.</returns>
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

        /// <summary>
        /// Get NumberGame's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <returns>The NumberGame's results.</returns>
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

        /// <summary>
        /// Get Racing's results.
        /// </summary>
        /// <param name="matchId">Match ID value.</param>
        /// <param name="raceNo">Race Number value.</param>
        /// <returns>The Racing's results.</returns>
        public IEnumerable<Racing> GetRacingResults(int matchId, int raceNo)
        {
            var param = new
            {
                matchId = matchId,
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

        /// <summary>
        /// Get Parlay's results.
        /// </summary>
        /// <param name="refNo">Reference Number value.</param>
        /// <returns>The Parlay's results.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan",
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

        /// <summary>
        /// Validate Permission.
        /// </summary>
        /// <param name="criteria">Criteria Input.</param>
        public void ValidatePermission(ValidationCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                db.ExecuteNonQuery(param);

                var outputParams = db.GetOuputParams();
                var properties = criteria.GetType().GetProperties();
                foreach (string outputParam in outputParams.Keys)
                {
                    foreach (var property in properties)
                    {
                        if (property.CanWrite && property.Name.ToLower() == outputParam)
                        {
                            property.SetValue(criteria, param[outputParam] == DBNull.Value ? null : param[outputParam], null);
                        }
                    }
                }
            }
        }

        #endregion Betlist.Results

        #region Betlist.Results.Utilities

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="reader">The IDATAREADER.</param>
        /// <returns>The SPORTBOOK DTO.</returns>
        private static Sportsbook BindSportsbook(System.Data.IDataReader reader)
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
                BetID = GetBetIDFromToken(reader)
            };

            return item;
        }

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="reader">The IDATAREADER.</param>
        /// <returns>The live casino DTO.</returns>
        private static LiveCasino BindLiveCasino(System.Data.IDataReader reader)
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

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="reader">The IDATAREADER.</param>
        /// <returns>The Racing DTO.</returns>
        private static Racing BindRacing(System.Data.IDataReader reader)
        {
            return new Racing
            {
                LeagueId = int.Parse(reader["meetingid"].ToString()),
                EngineId = reader["HorseId"].ToString(),
                EngineNo = reader["HorseNo"].ToString(),
                IsFavor = reader["IsFavor"] != DBNull.Value && reader["IsFavor"].ToString().Equals("1") ? true : false,
                IsSCR = reader["IsSCR"] != DBNull.Value && reader["IsSCR"].ToString().Equals("1") ? true : false,
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

        /// <summary>
        /// Bind data from IDATAREADER to DTO.
        /// </summary>
        /// <param name="result">The result list.</param>
        /// <param name="reader">The IDATAREADER.</param>
        private static void BindNumberGame(List<NumberGame> result, System.Data.IDataReader reader)
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
                    IsPending = reader.GetOrdinal("isrefund") >= 0 && reader["isrefund"].ToString() != "0",
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seg = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) }
                });
            }
        }

        /// <summary>
        /// Check the column existed in data reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>True or False.</returns>
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

        /// <summary>
        /// Bind game detail value.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>The game detail value.</returns>
        private static GameDetail BindGameDetail(IDataReader reader)
        {
            return new GameDetail
            {
                MatchId = int.Parse(reader["matchid"].ToString()),
                ResourceId = reader["ResourceId"].ToString(),
                HomeValue = reader["HomeValue"].ToString(),
                AwayValue = reader["AwayValue"].ToString(),
                ResultStatusID = int.Parse(reader["ResultStatusID"].ToString()),
                SettlementStatusID = int.Parse(reader["SettlementStatusID"].ToString()),
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

        #endregion Betlist.Results.Utilities
    }
}