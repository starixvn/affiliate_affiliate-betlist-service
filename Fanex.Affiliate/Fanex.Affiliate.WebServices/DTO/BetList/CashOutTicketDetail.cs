namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System.Collections.Generic;

    public class CashOutTicketDetail
    {
        public IList<TicketText> OriginalTextList { get; set; }

        public IList<CashOutTicketText> CashOutTextList { get; set; }

        public bool IsHighlightOriginalTicket { get; set; }

        public bool IsCashOut { get; set; }
    }
}