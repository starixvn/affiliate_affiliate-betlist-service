namespace Fanex.Affiliate.WebServices.Utilities
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Hosting;
    using System.Xml;
    using BetList.Core.Builder;
    using BetList.Core.Entities;
    using BetList.Core.TicketHelper;
    using Criteria.BetList;
    using Enumerations;
    using Fanex.Affiliate.WebServices.DTO.BetList;
    using RazorEngine;
    using RazorEngine.Templating;

    public static class AffiliateApiBetListHelper
    {
        private const int CashOutBetdaqId = 0;

        public static string BindDataToTemplate(string templatePath, object model, string key = "key")
        {
            var template = File.ReadAllText(templatePath);
            return Engine.Razor.RunCompile(template, key, model: model);
        }

        public static IList<TicketText> BuildTicketTextList(
            TicketResult ticketResult,
            BetListCriteria criteria,
            string username,
            decimal exchangeRate = 1,
            int startIndex = 1,
            bool showOdds = false)
        {
            var builder = new BetListHTMLBuilder();
            var ticketTextList = new List<TicketText>();
            var ticketHelper = new TicketHelper();
            decimal sumWinloss = 0;

            foreach (var ticket in ticketResult.MainTicketList)
            {
                if (ticket.BetTypeId == (int)BetType.CombinationMixParlay)
                {
                    var custid = ticket.CustId != 0 ? ticket.CustId : criteria.CustId;
                    ticketHelper.ClientDetailSystemParlayFunction = GenerateClientDetailSystemParlayFunction(ticketHelper, criteria.BaseCurrency, ticket, custid);
                }

                ticketHelper.IsShowOddsWithChoice = showOdds;
                ticket.Stake *= exchangeRate;
                ticket.ActualStake *= exchangeRate;
                ReadOriginalStakeForCashOut(ticket, exchangeRate);

                var ticketDataList = ticketResult.TicketDataList.ToList();
                var ticketText = new TicketText
                {
                    Index = startIndex++,
                    ExternalId = ticket.ExternalId,
                    SystemId = ticket.UserName,
                    TransTime = builder.BuildTrans(ticket),
                    Status = builder.BuildAffBetListResultStatus(
                        ticket, ticketHelper, ticketDataList, criteria, username),
                    Choice = builder.BuildChoice(ticket, ticketHelper, ticketDataList, false),
                    Odds = builder.BuildOdds(ticket, ticketDataList, CacheRef.GetOddsTypeName),
                    Stake = builder.BuildStake(ticket),
                    WinLoss = builder.BuildCustomerWL(ticket, exchangeRate)
                };

                sumWinloss += ticket.Winlost;
                ticketTextList.Add(ticketText);
            }

            ticketResult.SumWinLoss = sumWinloss;

            return ticketTextList;
        }

        /// <summary>
        /// Exchange system parlay ticket from user's currency to affiliate's currency.
        /// </summary>
        /// <param name="systemParlayData">Original system parlay data object.</param>
        /// <param name="exchangeRate">Exchange rate.</param>
        public static void ExchangeSystemParlayTickets(SystemParlayData systemParlayData, decimal exchangeRate)
        {
            systemParlayData.Outstanding *= exchangeRate;
            foreach (var ticket in systemParlayData.TicketList)
            {
                ticket.Comm *= exchangeRate;
                ticket.PlayerComm *= exchangeRate;
                ticket.Stake *= exchangeRate;
                ticket.WinLost *= exchangeRate;
            }
        }

        public static string GetTicketDetailHtml(TicketResult ticketResults, BetListDetailCriteria criteria, string userName)
        {
            if (ticketResults.MainTicketList == null)
            {
                return string.Empty;
            }

            var templatePath = HostingEnvironment.MapPath("~/Views/BetList.cshtml");
            var ticketTextList = BuildTicketTextList(ticketResults, criteria, userName, criteria.ExchangeRate);

            foreach (var ticket in ticketTextList)
            {
                ticket.Stake = CurrencyFormatter.Format(ticket.Stake);
                ticket.WinLoss = CurrencyFormatter.Format(ticket.WinLoss);
            }

            var content = new
            {
                IsShowSystemIdAndExternalId = false,
                IsShowWinLoss = true,
                TicketTextList = ticketTextList,
                SumWinLoss = CurrencyFormatter.Format(ticketResults.SumWinLoss * criteria.ExchangeRate)
            };

            return AffiliateApiBetListHelper.BindDataToTemplate(templatePath, content);
        }

        public static string BuildHtmlString(IEnumerable<TicketText> tickets)
        {
            var result = new StringBuilder();
            var counter = 0;
            var length = tickets.Count();
            foreach (var ticket in tickets)
            {
                result.Append(ticket.Choice);
                if (counter != length - 1)
                {
                    result.Append("<div class='line'></div>");
                }

                counter++;
            }

            return result.ToString();
        }

        private static string GenerateClientDetailSystemParlayFunction(ITicketHelper tickerHelper, string baseCurrency, AffTicket ticket, long custid)
        {
            const string FormatDetail = @"showCombMPDetail({0},{1},{2},{3},&quot;{4}&quot;,&quot;{5}&quot;)";
            var comboName = tickerHelper.GetCombinationMPOfferName(ticket.BetTeam);
            var betTeam = tickerHelper.GetBetTypeNameById(ticket.BetTypeId);
            if (!string.IsNullOrEmpty(comboName))
            {
                betTeam += " - " + comboName;
            }

            return string.Format(
                FormatDetail,
                ticket.TransId,
                ticket.RefNo,
                ticket.BetId,
                custid,
                betTeam,
                baseCurrency);
        }

        private static void ReadOriginalStakeForCashOut(AffTicket ticket, decimal exchangeRate)
        {
            if (ticket.BetdaqId == CashOutBetdaqId && !string.IsNullOrEmpty(ticket.ExtraInfo))
            {
                // if BetdaqId is 0, the ticket has been cashed out => need to get Original Stake from ExtraInfo field

                var extraInfoXml = new XmlDocument();
                extraInfoXml.LoadXml(ticket.ExtraInfo);

                var originalStakeNote = extraInfoXml.GetElementsByTagName("p25");

                if (originalStakeNote.Count > 0 && originalStakeNote[0].Attributes["s"] != null)
                {
                    decimal originalStake;
                    ticket.OriginalStake = decimal.TryParse(originalStakeNote[0].Attributes["s"].Value, out originalStake) ? originalStake * exchangeRate : ticket.OriginalStake;
                }
            }
        }
    }
}