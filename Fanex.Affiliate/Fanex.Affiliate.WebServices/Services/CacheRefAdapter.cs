using System;
using Fanex.RemotingRef;

namespace Fanex.Affiliate.WebServices.Services
{
    public class CacheRefAdapter : ICacheRefAdapter
    {
        private readonly Lazy<IRemotingRefService> _remotingRefService = new Lazy<IRemotingRefService>(() => new RemotingRefService());

        public IRemotingRefService RemotingRefService => _remotingRefService.Value;

        public string[] GetCombinationTypeCombo(string betTeam, string betCheck, decimal handicap1, decimal handicap2, string language)
        {
            return RemotingRefService.GetCombinationTypeCombo(betTeam, betCheck, Convert.ToInt32(handicap1), Convert.ToInt32(handicap2), language);
        }
    }
}