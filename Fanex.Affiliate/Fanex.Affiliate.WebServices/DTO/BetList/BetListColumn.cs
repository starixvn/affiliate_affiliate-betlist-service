using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Fanex.Affiliate.WebServices.Enumerations;

namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    public class BetListColumn
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
        public bool IsNullable { get; set; }
        public bool HasData { get; set; }
        public string MappingName { get; set; }

    }
}