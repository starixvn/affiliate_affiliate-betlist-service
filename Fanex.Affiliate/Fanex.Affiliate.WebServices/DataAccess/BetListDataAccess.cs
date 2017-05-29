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
    using BetList.Core.Entities;
    using Common;
    using Criteria.BetList;
    using Data;
    using DTO.BetList;
    using Helpers;

    public class BetListDataAccess : IBetListDataAccess
    {
        public TicketResult GetBetList(BetListCriteria criteria)
        {
            var result = new TicketResult();

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                var ds = objectDb.ExecuteDataSet(param);
                var baseCurrency = string.Empty;

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                result.MainTicketList = BetListDataAccessHelper.ReadMainAffTickets(mainTable, baseCurrency).ToList();

                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = BetListDataAccessHelper.ReadReferenceTickets(referenceTable, baseCurrency).ToList();

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
                result.MainTicketList = BetListDataAccessHelper.ReadMainAffTickets(mainTable, string.Empty).ToList();

                var referenceTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();
                result.TicketDataList = BetListDataAccessHelper.ReadReferenceTickets(referenceTable, string.Empty).ToList();
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
                result.MainTicketList = BetListDataAccessHelper.ReadMainAffTickets(mainTable, string.Empty).ToList();
            }

            return result;
        }

        public CashOutTicketHistory GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey)
        {
            var result = new CashOutTicketHistory();

            using (var objectDb = new ObjectDb(spkey))
            {
                objectDb.AddInParameter("TransId", DbType.Int64, criteria.TransId);
                objectDb.AddInParameter("RefNo", DbType.Int64, criteria.RefNo);
                objectDb.AddInParameter("BetType", DbType.Int32, criteria.BetType);
                objectDb.AddInParameter("OriginalTransId", DbType.Int64, criteria.RefNo);
                var ds = objectDb.ExecuteDataSet();

                var mainTable = ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                var cashOutTable = ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();

                result.OriginalTicket = BetListDataAccessHelper.ReadMainAffTickets(mainTable, string.Empty);
                result.CashOutTicket = ReadCashOutTicketDetailData(cashOutTable);
            }

            return result;
        }

        private static IEnumerable<CashOutTicket> ReadCashOutTicketDetailData(DataTable dataTable)
        {
            var columns = new List<BetListColumn>
            {
                new BetListColumn
                {
                    Name = "price",
                    HasData = dataTable.Columns.Contains("price"),
                    MappingName = "Price",
                    Type = DbType.Decimal
                }
            };
            return BetListDataAccessHelper.ReadMainTickets<CashOutTicket>(dataTable, columns);
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
    }
}