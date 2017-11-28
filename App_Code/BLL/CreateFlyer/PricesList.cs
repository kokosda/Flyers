using System;
using System.Collections.Generic;
using System.Linq;

namespace FlyerMe.BLL.CreateFlyer
{
    [Serializable]
    public class PricesList
    {
        public PricesList()
        {
            Items = new List<PricesListItem>();
        }

        public List<PricesListItem> Items { get; set; }

        public Decimal TotalPrice { get; set; }

        public String Markets { get; set; }

        public String Prices { get; set; }

        public static PricesList FromPricesList(Dictionary<Int32, FlyerMeDS.fly_sp_GetListSizeRow> pricesList)
        {
            PricesList result = null;

            if (pricesList != null && pricesList.Count > 0)
            {
                var items = pricesList.Values.Select(p => new PricesListItem
                                                            {
                                                                ListSize = p.listsize,
                                                                Market = p.market,
                                                                MarketId = p.marketid,
                                                                Price = p.price
                                                            })
                                            .ToList();

                result = new PricesList
                                {
                                    Items = items,
                                    TotalPrice = items.Sum(i => i.Price),
                                    Markets = String.Join("|", items.Select(i => i.Market).ToArray())
                                };
            }

            return result;
        }

        public static PricesList FromParameters(Order order, String marketType, String markets)
        {
            PricesList result = null;

            if ((!String.IsNullOrEmpty(marketType)) && (!String.IsNullOrEmpty(markets)))
            {
                var data = new FlyerBLL().GetMarketList(order.market_state, marketType, order.type)
                                         .Cast<FlyerMeDS.fly_sp_GetListSizeRow>();
                var md = data.ToDictionary(k => k.market.ToLower(), v => v);
                var ml = data.ToList();
                var itemNames = markets.Split('|');

                if (itemNames.Length > 0)
                {
                    result = new PricesList();

                    FlyerMeDS.fly_sp_GetListSizeRow row;

                    foreach (var name in itemNames)
                    {
                        if (md.ContainsKey(name.ToLower()))
                        {
                            row = md[name.ToLower()];
                            result.Items.Add(new PricesListItem
                                                    {
                                                        ListSize = row.listsize,
                                                        Market = row.market,
                                                        MarketId = row.marketid,
                                                        Price = row.price,
                                                        Index = ml.IndexOf(row)
                                                    });

                            result.TotalPrice += row.price;
                        }
                    }

                    if (result.Items.Count > 0)
                    {
                        result.Markets = String.Join("|", result.Items.Select(i => i.Market).ToArray());
                        result.Prices = String.Join(",", result.Items.Select(i => i.Index.ToString()).ToArray());
                    }
                }
            }

            return result;
        }
    }
}