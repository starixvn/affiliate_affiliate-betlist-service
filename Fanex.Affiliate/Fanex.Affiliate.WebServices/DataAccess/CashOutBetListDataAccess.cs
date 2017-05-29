namespace Fanex.Affiliate.WebServices.DataAccess
{
    using System.Collections.Generic;
    using System.Data;
    using Criteria.BetList;
    using Data;
    using DataAccess.Helpers;
    using DTO.BetList;

    public class CashOutBetListDataAccess : ICashOutBetListDataAccess
    {
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
    }
}