// ----------------------------------------------------------------------
// <copyright file="AffBetListHtmlBuilder.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// ------------------------------------------------------------------------

namespace Fanex.Affiliate.WebServices.Utilities
{
    using System;
    using System.Collections.Generic;
    using BetList.Core.App_GlobalResources;
    using BetList.Core.Builder;
    using BetList.Core.Entities;
    using Criteria.BetList;
    using DTO.BetList;
    using Fanex.Affiliate.WebServices.Resources;

    public static class AffBetListHtmlBuilder
    {
        public static string BuildAffBetListResultStatus(
            this BetListHTMLBuilder builder,
            AffTicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            BetListCriteria criteria,
            string username)
        {
            const string StatusTemp = "<div class='status'>{0}</div>";
            var statusText = GetStatusText(ticket);
            var statusNormalText = string.Format(StatusTemp, statusText);
            ticket.UserName = username;

            switch (ticket.BetTypeId)
            {
                case 71:
                    string casinoHandTemplate = "<div id='casino-hand-history-{0}'";
                    casinoHandTemplate += "class='result casino-hand-history-link' url-param='{1}' onclick='BetList.ViewCasinoHandHistory({0});'>{2}</div>";
                    return statusNormalText + string.Format(
                        casinoHandTemplate,
                        ticket.RefNo,
                        GetCasinoHandHistoryParam(ticket, criteria, username),
                        Lang.Results);

                case 72:
                case 73:
                    return statusNormalText;

                default:
                    return builder.BuildStatus(ticket, ticketHelper, ticketData, false);
            }
        }

        public static string BuildCustomerWL(this BetListHTMLBuilder builder, Ticket ticket, decimal exchangeRate)
        {
            var running = string.Compare(ticket.Status, "running", StringComparison.OrdinalIgnoreCase) == 0;
            const string PositiveNumberFormat = "<div class='underdog'>{0:#,##0.00}</div>";
            const string PegativeNumberFormat = "<div class='underdog' style='color:red;'>({0:#,##0.00})</div>";

            var winlostNumberFormat = ticket.Winlost < 0 ? PegativeNumberFormat : PositiveNumberFormat;
            var winloss = running
                            ? "-"
                            : string.Format(winlostNumberFormat, Math.Abs(ticket.Winlost) * exchangeRate);

            var playerCommNumberFormat = ticket.PlayerComm < 0 ? PegativeNumberFormat : PositiveNumberFormat;
            winloss += running
                ? string.Empty
                : string.Format(playerCommNumberFormat, Math.Abs(ticket.PlayerComm) * exchangeRate);

            return winloss;
        }

        private static string GetCasinoHandHistoryParam(
            AffTicket ticket,
            BetListCriteria criteria,
            string username)
        {
            var param = string.Empty;
            param += "?RefNo=" + ticket.RefNo;
            param += "&FromDate=" + criteria.FromDate;
            param += "&ToDate=" + criteria.ToDate;
            param += "&SystemId=" + (string.IsNullOrEmpty(ticket.UserName) ? username : ticket.UserName);
            param += "&ExternalId=" + (string.IsNullOrEmpty(ticket.ExternalId) ? string.Empty : ticket.ExternalId);
            param += "&CustId=" + (ticket.CustId == 0 ? criteria.CustId : ticket.CustId);
            param += "&CurrencyId=" + criteria.CurrencyId.ToString();
            param += "&CurrencyName=" + criteria.Currency;
            param += "&BaseCurrency=" + criteria.BaseCurrency;

            return param;
        }

        private static string GetStatusText(AffTicket ticket)
        {
            var statusText = string.Empty;

            switch (ticket.Status.ToLower())
            {
                case BetStatus.Running:
                    statusText = CoreBetList.running;
                    break;

                case BetStatus.Reject:
                    statusText = CoreBetList.reject;
                    break;

                case BetStatus.Won:
                    statusText = CoreBetList.won;
                    break;

                case BetStatus.Lose:
                    statusText = CoreBetList.lose;
                    break;

                case BetStatus.Draw:
                    statusText = CoreBetList.draw;
                    break;

                case BetStatus.Waiting:
                    statusText = CoreBetList.waiting;
                    break;

                case BetStatus.Void:
                    statusText = CoreBetList.voided;
                    break;

                case BetStatus.Refund:
                    statusText = CoreBetList.refund;
                    break;
            }

            return statusText;
        }
    }
}