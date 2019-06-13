using Newtonsoft.Json.Linq;
using PoEMarketLookup.ViewModels;
using System.Collections.Generic;

namespace PoEMarketLookup.Web
{
    public static class PoEJsonConverter
    {
        public static string SerializeSearchParameters(ItemViewModel item)
        {
            var filters = new JObject();
            int category = (int)item.ItemType;

            if (category >= 200 && category < 300)
            {
                filters.Add(CreateWeaponStatsProp(item.ItemStats));
            }

            var query = new JObject()
            {
                new JProperty("status", "any"),
                new JProperty("filters", filters)
            };

            var root = new JObject
            {
                new JProperty("query", query)
            };

            return root.ToString();
        }

        private static JProperty CreateWeaponStatsProp(IList<ItemStat> stats)
        {
            var dpsMin = new JObject()
            {
                new JProperty("min", stats[0].Value * .9),
                new JProperty("max", stats[0].Value * 1.1)
            };

            var dps = new JObject()
            {
                new JProperty("dps", dpsMin)
            };

            var filters = new JObject()
            {
                new JProperty("filters", dps)
            };

            var root = new JProperty("weapon_filters", filters);
            
            return root;
        }
    }
}
