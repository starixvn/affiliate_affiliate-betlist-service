namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class CashOutTicketHistory
    {
        public CashOutTicketHistory()
        {
            OriginalTicket = new List<AffTicket>();
            CashOutTicket = new List<CashOutTicket>();
        }

        public IEnumerable<AffTicket> OriginalTicket { get; set; }

        public IEnumerable<CashOutTicket> CashOutTicket { get; set; }
    }
}