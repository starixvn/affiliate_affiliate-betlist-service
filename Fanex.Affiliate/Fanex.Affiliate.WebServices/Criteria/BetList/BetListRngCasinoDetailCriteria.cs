namespace Fanex.Affiliate.WebServices.Criteria
{
    public class BetListRngCasinoDetailCriteria : BetListCriteria
    {
        public long RefNo { get; set; }

        public override string GetSettingKey()
        {
            return "Aff_BetList_RNG_Detail";
        }
    }
}