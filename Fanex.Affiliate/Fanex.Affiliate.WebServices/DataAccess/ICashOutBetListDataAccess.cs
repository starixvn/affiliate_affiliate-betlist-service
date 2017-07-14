using Fanex.Affiliate.WebServices.Criteria.BetList;
using Fanex.Affiliate.WebServices.DTO.BetList;

namespace Fanex.Affiliate.WebServices.DataAccess
{
    public interface ICashOutBetListDataAccess
    {
        CashOutTicketHistory GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey);
    }
}