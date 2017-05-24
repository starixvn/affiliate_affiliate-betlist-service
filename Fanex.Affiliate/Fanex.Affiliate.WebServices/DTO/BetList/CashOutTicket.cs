namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System;
    using Fanex.BetList.Core.Entities;

    [Serializable]
    public class CashOutTicket : Ticket
    {
        public decimal Price { get; set; }
    }
}