using System;

namespace FlyerMe.BLL.CreateFlyer
{
    [Serializable]
    public class PricesListItem
    {
        public Int32 MarketId { get; set; }

        public String Market { get; set; }

        public Int32 ListSize { get; set; }

        public Decimal Price { get; set; }

        public Int32 Index { get; set; }
    }
}