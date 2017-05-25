// ----------------------------------------------------------------------
// <copyright file="BetListDataAccess.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DataAccess.Betlist
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using BetList.Core.Entities;
    using Common;
    using Criteria.BetList;
    using Data;
    using DTO.BetList;
    using Fanex.Result.Core.LiveCasino.DTO;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Core.Racing.DTO;
    using Fanex.Result.Core.Sportsbook.DTO;

    /// <summary>
    /// Account data access.
    /// </summary>
    public class BetListDataAccess : IBetListDataAccess
    {
        #region BetList

        private static readonly IList<BetListColumn> BetListColumnList = new List<BetListColumn> {
            new BetListColumn {Name="username", Type=DbType.String, MappingName="UserName" },
            new BetListColumn {Name="refno",Type=DbType.String ,MappingName="RefNo" },
            new BetListColumn {Name="matchcode",Type=DbType.String,MappingName="MatchCode" },
            new BetListColumn {Name="status",Type=DbType.String ,MappingName="Status"},
            new BetListColumn {Name="betteam",Type=DbType.String,MappingName="BetTeam" },
            new BetListColumn {Name="betstatus",Type=DbType.String,MappingName="EventStatus" },
            new BetListColumn {Name="oddstype",Type=DbType.String,MappingName="OddsType" },
            new BetListColumn {Name="nickname",Type=DbType.String,MappingName="ExternalId" },
            new BetListColumn {Name="race",Type=DbType.String,MappingName="Race" },
            new BetListColumn {Name="transdesc",Type=DbType.String,MappingName="TransDesc" },
            new BetListColumn {Name="extrainfo",Type=DbType.String,MappingName="ExtraInfo" },
            new BetListColumn {Name="betcheck",Type=DbType.String,MappingName="BetCheck" },
            new BetListColumn {Name="currency",Type=DbType.String,MappingName="Currency" },
            new BetListColumn {Name="liveindicator",Type=DbType.Boolean,MappingName="IsLive" },
            new BetListColumn {Name="isneutral",Type=DbType.Boolean,MappingName="IsNeutral" },
            new BetListColumn {Name="sporttype",Type=DbType.Int32 ,MappingName="SportTypeId"},
            new BetListColumn {Name="bettype",Type=DbType.Int32,MappingName="BetTypeId" },
            new BetListColumn {Name="leagueid",Type=DbType.Int32,MappingName="LeagueId" },
            new BetListColumn {Name="homeid",Type=DbType.Int32,MappingName="HomeId" },
            new BetListColumn {Name="awayid",Type=DbType.Int32,MappingName="AwayId" },
            new BetListColumn {Name="matchid",Type=DbType.Int32,MappingName="MatchId" },
            new BetListColumn {Name="custid",Type=DbType.Int32,MappingName="CustId" },
            new BetListColumn {Name="currencyid",Type=DbType.Int32,MappingName="CurrencyId" },
            new BetListColumn {Name="livehomescore",Type=DbType.Int32,MappingName="LiveHomeScore" },
            new BetListColumn {Name="liveawayscore",Type=DbType.Int32,MappingName="LiveAwayScore" },
            new BetListColumn {Name="statusid",Type=DbType.Int32,MappingName="StatusId" },
            new BetListColumn {Name="originalbettype",Type=DbType.Int32,MappingName="OriginalBetType" },
            new BetListColumn {Name="transid",Type=DbType.Int64,MappingName="TransId" },
            new BetListColumn {Name="betid",Type=DbType.Int64,IsNullAble=true ,MappingName="BetId" },
            new BetListColumn {Name="betdaqid",Type=DbType.Int64,MappingName="BetdaqId" },
            new BetListColumn {Name="winlostdate",Type=DbType.DateTime,MappingName="WinlostDate" },
            new BetListColumn {Name="transdate",Type=DbType.DateTime,MappingName="TransDate" },
            new BetListColumn {Name="winlost",Type=DbType.Decimal,MappingName="Winlost" },
            new BetListColumn {Name="stake",Type=DbType.Decimal ,MappingName="Stake"},
            new BetListColumn {Name="odds",Type=DbType.Decimal,MappingName="Odds" },
            new BetListColumn {Name="modds",Type=DbType.Decimal,MappingName="MasterOdds" },
            new BetListColumn {Name="hdp1",Type=DbType.Decimal,MappingName="Handicap1" },
            new BetListColumn {Name="hdp2",Type=DbType.Decimal,MappingName="Handicap2" },
        };

        public static List<AffTicket> ReadMainTicketData(DataTable dataTable, string baseCurrency)
        {
            var playerCommCol = "playercomm" + baseCurrency;
            var commCol = "comm" + baseCurrency;
            var licenseeWlCol = "licenseewl" + baseCurrency;
            var actualStakeCol = "actual_stake" + baseCurrency;
            var result = new List<AffTicket>();
            var columnCollection = dataTable.Columns;
            var betlistColumnByCurrency = new List<BetListColumn>
            {
                new BetListColumn {Name=playerCommCol,Type=DbType.Decimal,HasData=columnCollection.Contains(playerCommCol),MappingName="PlayerComm" },
                new BetListColumn {Name=commCol + baseCurrency,Type=DbType.Decimal,HasData=columnCollection.Contains(commCol),MappingName="Comm" },
                new BetListColumn {Name=licenseeWlCol,Type=DbType.Decimal,HasData=columnCollection.Contains(licenseeWlCol),MappingName= "LicenseeWl"  },
                new BetListColumn {Name=actualStakeCol,Type=DbType.Decimal ,HasData=columnCollection.Contains(actualStakeCol),MappingName="ActualStake " }
            };

            var items = BetListColumnList.Select(i => new BetListColumn
            {
                Name = i.Name,
                HasData = columnCollection.Contains(i.Name),
                IsNullAble = i.IsNullAble,
                Type = i.Type
            }).Union(betlistColumnByCurrency).ToList();

            var ticketType = typeof(AffTicket);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(CreateAffTicket<AffTicket>(items, ticketType, dataRow));
            }

            return result;
        }

        private static void AssignDataToAffTicket(PropertyInfo property, DataRow dataRow, BetListColumn item)
        {
            switch (item.Type)
            {
                case DbType.String:
                    property.SetValue(item, Converter.ToString2(dataRow[item.Name]), null);
                    break;

                case DbType.Boolean:
                    property.SetValue(item, ReadBoolean(dataRow[item.Name]), null);
                    break;

                case DbType.Int32:
                    property.SetValue(item, Converter.ToInt(dataRow[item.Name]), null);

                    break;

                case DbType.Int64:
                    var value = item.IsNullAble ? Convert.IsDBNull(dataRow[item.Name]) ? new long?(Convert.ToInt64(dataRow[item.Name])) : null
                        : Convert.ToInt64(item.Name);

                    property.SetValue(item, value, null);
                    break;

                case DbType.DateTime:
                    property.SetValue(item, Converter.ToDateTime(dataRow[item.Name]), null);
                    break;

                case DbType.Decimal:
                    property.SetValue(item, Converter.ToDecimal(dataRow[item.Name]), null);
                    break;
            }
        }

        public static List<CashOutTicket> ReadCashOutTicketDetailData(DataTable dataTable)
        {
            var columnCollection = dataTable.Columns;
            var items = BetListColumnList.Select(i => new BetListColumn
            {
                Name = i.Name,
                HasData = columnCollection.Contains(i.Name),
                IsNullAble = i.IsNullAble,
                Type = i.Type
            }).ToList();
            items.Add(new BetListColumn
            {
                Name = "price",
                HasData = columnCollection.Contains("price"),
                MappingName = "Price",
                Type = DbType.Decimal
            });

            var ticketType = typeof(CashOutTicket);
            var result = new List<CashOutTicket>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(CreateAffTicket<CashOutTicket>(items, ticketType, dataRow));
            }

            return result;
        }

        private static T CreateAffTicket<T>(List<BetListColumn> items, Type ticketType, DataRow dataRow) where T : class
        {
            var ticket = Activator.CreateInstance(ticketType) as T;

            foreach (var item in items)
            {
                var property = ticketType.GetProperty(item.MappingName);
                if (item.HasData)
                {
                    AssignDataToAffTicket(property, dataRow, item);
                }
                else if (item.Type == DbType.String)
                {
                    property.SetValue(item, string.Empty, null);
                }
                else if (item.Type == DbType.DateTime)
                {
                    property.SetValue(item, DateTime.Now, null);
                }
            }

            return ticket;
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
                result.Add(new TicketData
                {
                    BetTeam = containsBetTeam ? dataRow["betteam"].ToString() : string.Empty,
                    RefNo = dataRow["refno"].ToString(),
                    Status = dataRow["status"].ToString(),
                    BetTypeId = Convert.ToInt32(dataRow["bettype"]),
                    Stake = containsStake ? Convert.ToDecimal(dataRow["stake" + baseCurrency]) : 0,
                    Odds = Convert.ToDecimal(dataRow["odds"])
                });
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

            if (value is bool)
            {
                return Convert.ToBoolean(value);
            }

            if (value is int)
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
                var systemParlayData = new SystemParlayData
                {
                    TicketList = new List<ISystemParlayTicket>()
                };

                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
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
                }

                systemParlayData.SerialList = new List<ISystemParlaySerial>();
                if (dataSet.Tables.Count > 1)
                {
                    foreach (DataRow row in dataSet.Tables[1].Rows)
                    {
                        var serial = new SystemParlaySerial
                        {
                            Serial = Convert.ToInt64(row["serial"]),
                            HomeId = Converter.ToInt(row["homeid"]),
                            AwayId = Converter.ToInt(row["awayid"])
                        };

                        systemParlayData.SerialList.Add(serial);
                    }
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
            using (var objectDb = new ObjectDb("Aff_BetList_SelDummyAccID"))
            {
                objectDb.ExecuteNonQuery(hashParam);
                return new CustomerInfoBetList
                {
                    CustId = int.Parse(hashParam["custid"].ToString()),
                    Username = playerUsername,
                    IBCUsername = hashParam["ibcusername"].ToString(),
                    CurrencyId = int.Parse(hashParam["currencyid"].ToString())
                };
            }
        }

        public TicketResult GetBetListDetail(BetListDetailCriteria criteria, string spkey)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb(spkey))
            {
                objectDb.AddInParameter("RefNo", DbType.Int64, criteria.RefNo);
                objectDb.AddInParameter("CustId", DbType.Int32, criteria.CustId);
                var ds = objectDb.ExecuteDataSet();

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = ReadMainTicketData(mainTable, string.Empty);
            }

            return result;
        }

        public CashOutTicketDetail GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey)
        {
            var result = new CashOutTicketDetail();

            using (var objectDb = new ObjectDb(spkey))
            {
                objectDb.AddInParameter("TransId", DbType.Int64, criteria.TransId);
                objectDb.AddInParameter("RefNo", DbType.Int64, criteria.RefNo);
                objectDb.AddInParameter("BetType", DbType.Int32, criteria.BetType);
                objectDb.AddInParameter("OriginalTransId", DbType.Int64, criteria.OriginalTransId);
                var ds = objectDb.ExecuteDataSet();

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                var cashOutTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();

                result.OriginalTicket = ReadMainTicketData(mainTable, string.Empty);
                result.CashOutTicket = ReadCashOutTicketDetailData(cashOutTable);
            }

            return result;
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
                BetID = GetBetIDFromToken(reader),
                LiveAwayScore = ColumnExisted(reader, "LiveAwayScore") && reader["LiveAwayScore"] != DBNull.Value ? Convert.ToInt32(reader["LiveAwayScore"]) : new int?(),
                LiveHomeScore = ColumnExisted(reader, "LiveHomeScore") && reader["LiveHomeScore"] != DBNull.Value ? Convert.ToInt32(reader["LiveHomeScore"]) : new int?()
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
                    IsPending = Convert.ToBoolean(reader["isrefund"]),
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

        #endregion Betlist.Results.Utilities
    }
}