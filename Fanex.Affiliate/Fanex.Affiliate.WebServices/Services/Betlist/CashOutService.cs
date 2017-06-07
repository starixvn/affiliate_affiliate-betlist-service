// ----------------------------------------------------------------------
// <copyright file="CashOutService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Services.Betlist
{
    using System.Collections.Generic;
    using System.Linq;
    using BetList.Core.Builder;
    using BetList.Core.TicketHelper;
    using Common.Helper;
    using Criteria.BetList;
    using DTO.BetList;
    using Fanex.Affiliate.WebServices.DataAccess;
    using Resources;
    using Utilities;

    public class CashOutService : ICashOutService
    {
        private readonly ICashOutBetListDataAccess _cashOutBetListDataAccess;

        public CashOutService()
            : this(null)
        {
        }

        private CashOutService(ICashOutBetListDataAccess cashOutBetListDataAccess)
        {
            _cashOutBetListDataAccess = cashOutBetListDataAccess ?? new CashOutBetListDataAccess();
        }

        public CashOutTicketDetail GetBetListCashOutDetail(BetListDetailCriteria criteria, string spkey, bool isCashOut)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var tickets = _cashOutBetListDataAccess.GetBetListCashOutDetail(criteria, spkey);

            if (tickets.OriginalTicket.Any())
            {
                return new CashOutTicketDetail
                {
                    OriginalTextList = BuildOriginalTicketText(tickets.OriginalTicket, criteria),
                    CashOutTextList = BuildCashOutTicketText(tickets.CashOutTicket, criteria),
                    IsHighlightOriginalTicket = tickets.OriginalTicket.Any(x => x.TransId == criteria.TransId)
                || IsCashOutBetTicket(criteria.BetdaqId, criteria.BetType),
                    IsCashOut = isCashOut
                };
            }

            return new CashOutTicketDetail
            {
                OriginalTextList = new List<TicketText>(),
                CashOutTextList = new List<CashOutTicketText>(),
                IsCashOut = isCashOut
            };
        }

        private static IEnumerable<TicketText> BuildOriginalTicketText(IEnumerable<AffTicket> tickets, BetListDetailCriteria criteria)
        {
            var builder = new BetListHTMLBuilder();
            var ticketTextList = new List<TicketText>();
            var ticketHelper = new TicketHelper
            {
                IsShowOddsWithChoice = false,
                HideStatusCashOutHistory = true
            };

            foreach (var ticket in tickets)
            {
                var statusHtml = builder.CreateStatusBuilder(ticket.BetTypeId).Render(ticket, ticketHelper, null, true);
                statusHtml.StatusResult = null;

                ticket.Stake *= criteria.ExchangeRate;
                ticket.ActualStake *= criteria.ExchangeRate;
                ticket.OriginalStake *= criteria.ExchangeRate;
                var ticketText = new TicketText
                {
                    TransTime = builder.BuildTrans(ticket),
                    Status = statusHtml.ToString(),
                    Choice = builder.BuildChoice(ticket, ticketHelper, null, false),
                    Odds = builder.BuildOdds(ticket, null, CacheRef.GetOddsTypeName),
                    Stake = builder.BuildStake(ticket),
                    WinLoss = builder.BuildCustomerWL(ticket, criteria.ExchangeRate)
                };

                ticketTextList.Add(ticketText);
            }

            return ticketTextList;
        }

        private static IEnumerable<CashOutTicketText> BuildCashOutTicketText(IEnumerable<CashOutTicket> tickets, BetListDetailCriteria criteria)
        {
            var builder = new BetListHTMLBuilder();
            var ticketTextList = new List<CashOutTicketText>();
            var ticketHelper = new TicketHelper
            {
                HideStatusCashOutHistory = true
            };

            foreach (var ticket in tickets)
            {
                var betListTicket = new AffTicket
                {
                    BetTypeId = ticket.BetTypeId,
                    Status = ticket.Status
                };
                var statusHtml = builder.CreateStatusBuilder(ticket.BetTypeId).Render(betListTicket, ticketHelper, null, true);
                statusHtml.StatusResult = null;

                ticket.Stake *= criteria.ExchangeRate;
                ticket.Winlost *= criteria.ExchangeRate;
                ticket.Price *= criteria.ExchangeRate;
                var ticketText = new CashOutTicketText
                {
                    TransTime = Lang.RefNo + ": " + ticket.TransId + "<div class='time'>" + ticket.TransDate + "</div>",
                    Stake = ticket.Stake.ToString("0"),
                    Price = ticket.Price.ToString("0"),
                    WinLoss = ticket.Winlost.ToString("0.00"),
                    Status = statusHtml.ToString(),
                    IsHighlight = criteria.TransId == ticket.TransId
                };

                ticketTextList.Add(ticketText);
            }

            return ticketTextList;
        }

        private static bool IsCashOutBetTicket(long betdaqid, int bettype)
        {
            var cashOutBetTypes = new int[] { 900, 912 };
            return betdaqid == 1 && !cashOutBetTypes.Contains(bettype);
        }
    }
}