using Newtonsoft.Json.Linq;
using PoEMarketLookup.ViewModels;
using System.Collections.Generic;

namespace PoEMarketLookup.Web
{
    public class PoEJsonConverter
    {
        private ItemViewModel _vm;

        public PoEJsonConverter(ItemViewModel vm)
        {
            _vm = vm;
        }

        public string SerializeSearchParameters()
        {
            var filters = new JObject();
            int category = (int)_vm.ItemType;

            if (category >= 200 && category < 300)
            {
                filters.Add(CreateWeaponStatsProp());
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

        private JProperty CreateWeaponStatsProp()
        {
            var totalDPS = _vm.WeaponDPS.Value;

            var dpsTotal = new JObject()
            {
                new JProperty("min", totalDPS * .9),
                new JProperty("max", totalDPS * 1.1)
            };

            var dps = new JObject()
            {
                new JProperty("dps", dpsTotal)
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
