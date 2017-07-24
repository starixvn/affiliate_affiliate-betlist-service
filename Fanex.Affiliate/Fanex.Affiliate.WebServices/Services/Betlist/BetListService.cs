// ----------------------------------------------------------------------
// <copyright file="BetListService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BetList.Core.Entities;
    using Criteria.BetList;
    using DataAccess;
    using DTO.BetList;
    using Enumerations;
    using Fanex.Common.Helper;

    public class BetListService : IBetListService
    {
        private readonly IBetListDataAccess _betlistDataAccess;

        private static readonly Dictionary<string, Action<BetListCriteria>> ProductShowingHandler = new Dictionary<string, Action<BetListCriteria>>
        {
            { ProductName.Sportsbook, (criteria) => { criteria.ShowSB = true; } },
            { ProductName.RngCasino, (criteria) => { criteria.ShowCS = true; } },
            { ProductName.Racing, (criteria) => { criteria.ShowRB = true; } },
            { ProductName.NumberGame, (criteria) => { criteria.ShowNG = true; } },
            { ProductName.Keno, (criteria) => { criteria.ShowKN = true; } },
            { ProductName.VirtualSports, (criteria) => { criteria.ShowVS = true; } },
            { ProductName.LiveCasino, (criteria) => { criteria.ShowLC = true; } },
            { ProductName.AgCasino, (criteria) => { criteria.ShowAG = true; } },
            { ProductName.GoldDeluxe, (criteria) => { criteria.ShowGD = true; } },
            { ProductName.ColossusBets, (criteria) => { criteria.ShowCB = true; } },
            { ProductName.CockFighting, (criteria) => { criteria.ShowCF = true; } }
        };

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

            foreach (var product in productList)
            {
                var productName = product.ToLowerInvariant();
                if (ProductShowingHandler.ContainsKey(productName) && supportedProducts.Any(t => t.ProductName.Equals(product.Trim())))
                {
                    ProductShowingHandler[productName].Invoke(criteria);
                }
            }
        }

        private CustomerInfoBetList GetPlayerIbcInfo(string affiliateGroup, string playerUsername)
        {
            return _betlistDataAccess.GetPlayerIbcInfo(affiliateGroup, playerUsername);
        }
    }
}