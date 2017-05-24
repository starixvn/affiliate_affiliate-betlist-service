namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class CashOutTicketDetail
    {
        public CashOutTicketDetail()
        {
            OriginalTicket = new List<AffTicket>();
            CashOutTicket = new List<CashOutTicket>();
        }

        public IList<AffTicket> OriginalTicket { get; set; }

        public IList<CashOutTicket> CashOutTicket { get; set; }
    }
}