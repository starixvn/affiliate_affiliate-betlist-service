namespace Fanex.Affiliate.WebServices.DataAccess.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using BetList.Core.Entities;
    using Common;
    using Fanex.Affiliate.WebServices.DTO.BetList;

    public static class BetListDataAccessHelper
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

        public static IEnumerable<AffTicket> ReadMainAffTickets(DataTable dataTable, string baseCurrency)
        {
            var playerCommCol = "playercomm" + baseCurrency;
            var commCol = "comm" + baseCurrency;
            var licenseeWlCol = "licenseewl" + baseCurrency;
            var actualStakeCol = "actual_stake" + baseCurrency;
            var columnCollection = dataTable.Columns;
            var columnsByCurrency = new List<BetListColumn>
            {
                new BetListColumn { Name = playerCommCol, Type = DbType.Decimal, HasData = columnCollection.Contains(playerCommCol), MappingName = "PlayerComm" },
                new BetListColumn { Name = commCol, Type = DbType.Decimal, HasData = columnCollection.Contains(commCol), MappingName = "Comm" },
                new BetListColumn { Name = licenseeWlCol, Type = DbType.Decimal, HasData = columnCollection.Contains(licenseeWlCol), MappingName = "LicenseeWl" },
                new BetListColumn { Name = actualStakeCol, Type = DbType.Decimal, HasData = columnCollection.Contains(actualStakeCol), MappingName = "ActualStake" }
            };

            return ReadMainTickets<AffTicket>(dataTable, columnsByCurrency);
        }

        public static IEnumerable<ITicketData> ReadReferenceTickets(DataTable data, string baseCurrency)
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

        public static IEnumerable<T> ReadMainTickets<T>(DataTable dataTable, IEnumerable<BetListColumn> additionalColumns) where T : class
        {
            var columns = GenerateAvailableBetListColumns(dataTable.Columns).Union(additionalColumns);

            var ticketType = typeof(T);
            var setters = GetSetterMethods(ticketType);
            var result = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                result.Add(CreateAffTicket<T>(setters, columns, ticketType, dataRow));
            }

            return result;
        }

        private static IEnumerable<BetListColumn> GenerateAvailableBetListColumns(DataColumnCollection columnCollection)
        {
            return BetListColumnList.Select(i => new BetListColumn
            {
                Name = i.Name,
                HasData = columnCollection.Contains(i.Name),
                IsNullable = i.IsNullable,
                Type = i.Type,
                MappingName = i.MappingName
            }).ToList();
        }

        private static IDictionary<string, MethodInfo> GetSetterMethods(Type type)
        {
            return type.GetProperties()
                            .Select(i => new KeyValuePair<string, MethodInfo>(i.Name, i.GetSetMethod()))
                            .ToDictionary(j => j.Key, j => j.Value);
        }

        private static T CreateAffTicket<T>(IDictionary<string, MethodInfo> setters, IEnumerable<BetListColumn> columns, Type ticketType, DataRow row) where T : class
        {
            var ticket = Activator.CreateInstance(ticketType) as T;

            foreach (var column in columns)
            {
                if (!setters.ContainsKey(column.MappingName))
                {
                    continue;
                }

                var setter = setters[column.MappingName];
                if (column.HasData)
                {
                    AssignDataToAffTicket(ticket, setter, row, column);
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