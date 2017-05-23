namespace Fanex.Affiliate.WebServices.Criteria
{
    public class BetListDetailCriteria : BetListCriteria
    {
        public long RefNo { get; set; }

        public decimal ExchangeRate { get; set; }

        public long TranId { get; set; }

        public long OriginalTransId { get; set; }

        public long BetType { get; set; }
    }
}