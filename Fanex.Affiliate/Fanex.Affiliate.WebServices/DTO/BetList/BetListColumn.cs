namespace Fanex.Affiliate.WebServices.DTO.BetList
{
    using System.Data;

    public class BetListColumn
    {
        public string Name { get; set; }

        public DbType Type { get; set; }

        public bool IsNullable { get; set; }

        public bool HasData { get; set; }

        public string MappingName { get; set; }
    }
}