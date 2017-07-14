namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System.Collections.Generic;

    public class CashOutTicketDetail
    {
        public IEnumerable<TicketText> OriginalTextList { get; set; }

        public IEnumerable<CashOutTicketText> CashOutTextList { get; set; }

        public bool IsHighlightOriginalTicket { get; set; }

        public bool IsCashOut { get; set; }
    }
}