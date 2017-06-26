// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using BetList.Core.Entities;
    using Criteria.BetList;
    using DataAccess;
    using DTO.BetList;
    using Fanex.Common.Helper;

    public class BetListService : IBetListService
    {
        private readonly IBetListDataAccess _betlistDataAccess;

        public BetListService()
            : this(null)
        {
        }

        private BetListService(IBetListDataAccess betlistDataAccess)
        {
            _betlistDataAccess = betlistDataAccess ?? new BetListDataAccess();
        }

        public TicketResult GetBetList(BetListCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetList(criteria);
        }

        public TicketResult GetBetListParlay(BetListParlayCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListParlay(criteria);
        }

        public SystemParlayData GetSystemParlayDetail(BetListSystemParlayDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetSystemParlayDetail(criteria);
        }

        public TicketResult GetBetListDetail(BetListDetailCriteria criteria, string spkey)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            return _betlistDataAccess.GetBetListDetail(criteria, spkey);
        }

        public BetListCriteria GenerateBetlistCriteria(BetlistSOAP betlistSOAP)
        {
            var affiliateGroup = betlistSOAP.AffiliateName.Trim() == "12bet" ? "_12bet" : string.Empty;
            var balanceUpMoveBetCriteria = new BalanceUpMoveBetCriteria { AffiliateGroup = affiliateGroup };
            GetBalanceUpMoveBetDate(balanceUpMoveBetCriteria);
            var customerInfoBetList = GetPlayerIbcInfo(affiliateGroup, betlistSOAP.AffiliateName + "$" + betlistSOAP.Username);

            BetListCriteria criteria;
            if (betlistSOAP.FromDate < balanceUpMoveBetCriteria.BalanceUpDay)
            {
                criteria = new BetListArchiveCriteria
                {
                    CustId = customerInfoBetList.CustId,
                    FromDate = betlistSOAP.FromDate,
                    ToDate = betlistSOAP.ToDate,
                    AffiliateGroup = affiliateGroup
                };
            }
            else
            {
                criteria = new BetListNormalCriteria
                {
                    CustId = customerInfoBetList.CustId,
                    FromDate = betlistSOAP.FromDate,
                    ToDate = betlistSOAP.ToDate,
                    MoveBetsDay = balanceUpMoveBetCriteria.MoveBetsDay,
                    AffiliateGroup = affiliateGroup
                };
            }

            AssignAllShowProductForCriteria(criteria, betlistSOAP.Product, betlistSOAP.SupportedProducts);

            return criteria;
        }

        public void GetBalanceUpMoveBetDate(BalanceUpMoveBetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            _betlistDataAccess.GetBalanceUpMoveBetDate(criteria);
        }

        private static void AssignAllShowProductForCriteria(BetListCriteria criteria, string products, IEnumerable<Product> supportedProducts)
        {
            var productList = products.Trim() == "All" ?
                supportedProducts.Select(t => t.ProductName).ToArray() :
                products.Split(',');

            foreach (var pro in productList)
            {
                if (supportedProducts.Any(t => t.ProductName.Equals(pro.Trim())))
                {
                    AssignShowProductForCriteria(criteria, pro);
                }
            }
        }

        [SuppressMessage("SonarQube", "S1541:Methods and properties should not be too complex", Justification = "Reviewed")]
        private static void AssignShowProductForCriteria(BetListCriteria criteria, string product)
        {
            switch (product.ToLowerInvariant())
            {
                case "sportsbook":
                    criteria.ShowSB = true;
                    break;

                case "rng casino":
                    criteria.ShowCS = true;
                    break;

                case "racing":
                    criteria.ShowRB = true;
                    break;

                case "number game":
                    criteria.ShowNG = true;
                    break;

                case "keno":
                    criteria.ShowKN = true;
                    break;

                case "virtual sports":
                    criteria.ShowVS = true;
                    break;

                case "live casino":
                    criteria.ShowLC = true;
                    break;

                case "ag casino":
                    criteria.ShowAG = true;
                    break;

                case "gold deluxe":
                    criteria.ShowGD = true;
                    break;

                case "colossus bets":
                    criteria.ShowCB = true;
                    break;

                case "cock fighting":
                    criteria.ShowCF = true;
                    break;

                default:
                    throw new KeyNotFoundException($"Not found product {product}");
            }
        }

        private CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername)
        {
            return _betlistDataAccess.GetPlayerIbcInfo(affiliateGroup, playerUsername);
        }
    }
}