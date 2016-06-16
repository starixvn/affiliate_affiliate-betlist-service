using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    public class Product
    {
        public byte ProductId { get; set; }

        public string ProductName { get; set; }

        public string OriginalProductName { get; set; }

        public int TotalQualifiedMembers { get; set; }
    }
}