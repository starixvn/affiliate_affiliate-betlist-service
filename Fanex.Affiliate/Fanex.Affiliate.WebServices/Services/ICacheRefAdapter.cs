namespace Fanex.Affiliate.WebServices.Services
{
    public interface ICacheRefAdapter
    {
        string[] GetCombinationTypeCombo(string betTeam, string betCheck, decimal handicap1, decimal handicap2, string language);
    }
}
