// ----------------------------------------------------------------------
// <copyright file="IResultService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Result
{
    using System.Collections.Generic;
    using Criteria.BetList;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Sportsbook.DTO;

    public interface IResultService
    {
        string GetResultHtml(BetListResultsCriteria criteria, SportType sportType, ResultSetting setting);

        string GetParlayResultHtml(long refNo, ResultSetting setting);
    }
}