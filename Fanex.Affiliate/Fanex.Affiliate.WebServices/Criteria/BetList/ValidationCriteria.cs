namespace Fanex.Affiliate.WebServices.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Bet List Criteria.
    /// </summary>
    public class ValidationCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public string AffiliateName { get; set; }

        public string AdminName { get; set; }

        public int AffId { get; set; }

        public string MemberName { get; set; }

        public bool BelongLevel1 { get; set; }

        public bool BelongLevel2 { get; set; }

        public override string GetSettingKey()
        {
            return "affiliate_" + AffiliateName.ToLower() + "_CheckBelonging";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}