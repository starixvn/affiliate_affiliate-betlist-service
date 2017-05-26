// ----------------------------------------------------------------------
// <copyright file="BetListDataAccess.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using BetList.Core.Entities;
    using Common;
    using Criteria.BetList;
    using Data;
    using DTO.BetList;

    public class BetListDataAccess : IBetListDataAccess
    {
        private static readonly IList<BetListColumn> BetListColumnList = new List<BetListColumn>
        {
            new BetListColumn { Name = "username", Type = DbType.String, MappingName = "UserName" },
            new BetListColumn { Name = "refno", Type = DbType.String, MappingName = "RefNo" },
            new BetListColumn { Name = "matchcode", Type = DbType.String, MappingName = "MatchCode" },
            new BetListColumn { Name = "status", Type = DbType.String, MappingName = "Status" },
            new BetListColumn { Name = "betteam", Type = DbType.String, MappingName = "BetTeam" },
            new BetListColumn { Name = "betstatus", Type = DbType.String, MappingName = "EventStatus" },
            new BetListColumn { Name = "oddstype", Type = DbType.String, MappingName = "OddsType" },
            new BetListColumn { Name = "nickname", Type = DbType.String, MappingName = "ExternalId" },
            new BetListColumn { Name = "race", Type = DbType.String, MappingName = "Race" },
            new BetListColumn { Name = "transdesc", Type = DbType.String, MappingName = "TransDesc" },
            new BetListColumn { Name = "extrainfo", Type = DbType.String, MappingName = "ExtraInfo" },
            new BetListColumn { Name = "betcheck", Type = DbType.String, MappingName = "BetCheck" },
            new BetListColumn { Name = "currency", Type = DbType.String, MappingName = "Currency" },
            new BetListColumn { Name = "liveindicator", Type = DbType.Boolean, MappingName = "IsLive" },
            new BetListColumn { Name = "isneutral", Type = DbType.Boolean, MappingName = "IsNeutral" },
            new BetListColumn { Name = "sporttype", Type = DbType.Int32, MappingName = "SportTypeId" },
            new BetListColumn { Name = "bettype", Type = DbType.Int32, MappingName = "BetTypeId" },
            new BetListColumn { Name = "leagueid", Type = DbType.Int32, MappingName = "LeagueId" },
            new BetListColumn { Name = "homeid", Type = DbType.Int32, MappingName = "HomeId" },
            new BetListColumn { Name = "awayid", Type = DbType.Int32, MappingName = "AwayId" },
            new BetListColumn { Name = "matchid", Type = DbType.Int32, MappingName = "MatchId" },
            new BetListColumn { Name = "custid", Type = DbType.Int32, MappingName = "CustId" },
            new BetListColumn { Name = "currencyid", Type = DbType.Int32, MappingName = "CurrencyId" },
            new BetListColumn { Name = "livehomescore", Type = DbType.Int32, MappingName = "LiveHomeScore" },
            new BetListColumn { Name = "liveawayscore", Type = DbType.Int32, MappingName = "LiveAwayScore" },
            new BetListColumn { Name = "statusid", Type = DbType.Int32, MappingName = "StatusId" },
            new BetListColumn { Name = "originalbettype", Type = DbType.Int32, MappingName = "OriginalBetType" },
            new BetListColumn { Name = "transid", Type = DbType.Int64, MappingName = "TransId" },
            new BetListColumn { Name = "betid", Type = DbType.Int64, IsNullable = true, MappingName = "BetId" },
            new BetListColumn { Name = "betdaqid", Type = DbType.Int64, MappingName = "BetdaqId" },
            new BetListColumn { Name = "winlostdate", Type = DbType.DateTime, MappingName = "WinlostDate" },
            new BetListColumn { Name = "transdate", Type = DbType.DateTime, MappingName = "TransDate" },
            new BetListColumn { Name = "winlost", Type = DbType.Decimal, MappingName = "Winlost" },
            new BetListColumn { Name = "stake", Type = DbType.Decimal, MappingName = "Stake" },
            new BetListColumn { Name = "odds", Type = DbType.Decimal, MappingName = "Odds" },
            new BetListColumn { Name = "modds", Type = DbType.Decimal, MappingName = "MasterOdds" },
            new BetListColumn { Name = "hdp1", Type = DbType.Decimal, MappingName = "Handicap1" },
            new BetListColumn { Name = "hdp2", Type = DbType.Decimal, MappingName = "Handicap2" },
        };

        public static List<AffTicket> ReadMainTicketData(DataTable dataTable, string baseCurrency)
        {
            var playerCommCol = "playercomm" + baseCurrency;
            var commCol = "comm" + baseCurrency;
            var licenseeWlCol = "licenseewl" + baseCurrency;
            var actualStakeCol = "actual_stake" + baseCurrency;
            var columnCollection = dataTable.Columns;
            var betlistColumnByCurrency = new List<BetListColumn>
            {
                new BetListColumn { Name = playerCommCol, Type = DbType.Decimal, HasData = columnCollection.Contains(playerCommCol), MappingName = "PlayerComm" },
                new BetListColumn { Name = commCol, Type = DbType.Decimal, HasData = columnCollection.Contains(commCol), MappingName = "Comm" },
                new BetListColumn { Name = licenseeWlCol, Type = DbType.Decimal, HasData = columnCollection.Contains(licenseeWlCol), MappingName = "LicenseeWl" },
                new BetListColumn { Name = actualStakeCol, Type = DbType.Decimal, HasData = columnCollection.Contains(actualStakeCol), MappingName = "ActualStake" }
            };

            var columnList = BetListColumnList.Select(i => new BetListColumn
            {
                Name = i.Name,
                HasData = columnCollection.Contains(i.Name),
                IsNullable = i.IsNullable,
                Type = i.Type,
                MappingName = i.MappingName
            }).Union(betlistColumnByCurrency).ToList();

            var ticketType = typeof(AffTicket);
            var setters = GetSetterMethods(ticketType);
            var result = new List<AffTicket>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(CreateAffTicket<AffTicket>(setters, columnList, ticketType, dataRow));
            }

            return result;
        }

        public static IList<CashOutTicket> ReadCashOutTicketDetailData(DataTable dataTable)
        {
            var columnCollection = dataTable.Columns;
            var items = BetListColumnList.Select(i => new BetListColumn
            {
                Name = i.Name,
                HasData = columnCollection.Contains(i.Name),
                IsNullable = i.IsNullable,
                Type = i.Type,
                MappingName = i.MappingName
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
            var setters = GetSetterMethods(ticketType);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(CreateAffTicket<CashOutTicket>(setters, items, ticketType, dataRow));
            }

            return result;
        }

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

                systemParlayData.SerialList = dataSet.Tables.Count > 1 ?
                        ReadSystemParlaySerialList(dataSet.Tables[1]) :
                        new List<ISystemParlaySerial>();

                systemParlayData.Outstanding = Converter.ToDecimal(param["outstanding"]);
                return systemParlayData;
            }
        }

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
                objectDb.AddInParameter("OriginalTransId", DbType.Int64, criteria.RefNo);
                var ds = objectDb.ExecuteDataSet();

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                var cashOutTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();

                result.OriginalTicket = ReadMainTicketData(mainTable, string.Empty);
                result.CashOutTicket = ReadCashOutTicketDetailData(cashOutTable);
            }

            return result;
        }

        private static IList<ISystemParlaySerial> ReadSystemParlaySerialList(DataTable table)
        {
            var list = new List<ISystemParlaySerial>();
            foreach (DataRow row in table.Rows)
            {
                var serial = new SystemParlaySerial
                {
                    Serial = Convert.ToInt64(row["serial"]),
                    HomeId = Converter.ToInt(row["homeid"]),
                    AwayId = Converter.ToInt(row["awayid"])
                };

                list.Add(serial);
            }

            return list;
        }

        private static IDictionary<string, MethodInfo> GetSetterMethods(Type type)
        {
            return type.GetProperties()
                            .Select(i => new KeyValuePair<string, MethodInfo>(i.Name, i.GetSetMethod()))
                            .ToDictionary(j => j.Key, j => j.Value);
        }

        private static T CreateAffTicket<T>(IDictionary<string, MethodInfo> setters, IList<BetListColumn> columnList, Type ticketType, DataRow dataRow) where T : class
        {
            var ticket = Activator.CreateInstance(ticketType) as T;

            foreach (var column in columnList)
            {
                if (!setters.ContainsKey(column.MappingName))
                {
                    continue;
                }

                var setter = setters[column.MappingName];
                if (column.HasData)
                {
                    AssignDataToAffTicket(ticket, setter, dataRow, column);
                }
                else if (column.Type == DbType.String)
                {
                    setter.Invoke(ticket, new object[] { string.Empty });
                }
                else if (column.Type == DbType.DateTime)
                {
                    setter.Invoke(ticket, new object[] { DateTime.Now });
                }
            }

            return ticket;
        }

        private static void AssignDataToAffTicket<T>(T ticket, MethodInfo setter, DataRow dataRow, BetListColumn item) where T : class
        {
            if (setter != null)
            {
                var columnValue = dataRow[item.Name];
                switch (item.Type)
                {
                    case DbType.String:
                        setter.Invoke(ticket, new object[] { Converter.ToString2(columnValue) });
                        break;

                    case DbType.Boolean:
                        setter.Invoke(ticket, new object[] { ReadBoolean(columnValue) });
                        break;

                    case DbType.Int32:
                        setter.Invoke(ticket, new object[] { Converter.ToInt(columnValue) });

                        break;

                    case DbType.Int64:
                        var value = item.IsNullable ? Convert.IsDBNull(columnValue) ? new long?(Convert.ToInt64(columnValue)) : null
                            : Convert.ToInt64(columnValue);

                        setter.Invoke(ticket, new object[] { value });
                        break;

                    case DbType.DateTime:
                        setter.Invoke(ticket, new object[] { Converter.ToDateTime(columnValue) });
                        break;

                    case DbType.Decimal:
                        setter.Invoke(ticket, new object[] { Converter.ToDecimal(columnValue) });
                        break;
                }
            }
        }

        private static bool ReadBoolean(object value)
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
    }
}