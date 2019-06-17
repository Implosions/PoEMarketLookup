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

            var dps = new JObject();

            if (_vm.WeaponDPS.Checked)
            {
                dps.Add(new JProperty("dps", CreateStatValuesObj(_vm.WeaponDPS.Value)));
            }

            if (_vm.WeaponEDPS.Checked)
            {
                dps.Add(new JProperty("edps", CreateStatValuesObj(_vm.WeaponEDPS.Value)));
            }

            var filters = new JObject()
            {
                new JProperty("filters", dps)
            };

            var root = new JProperty("weapon_filters", filters);
            
            return root;
        }

        private JObject CreateStatValuesObj(double stat)
        {
            return new JObject()
            {
                new JProperty("min", stat * .9),
                new JProperty("max", stat * 1.1)
            };
        }
    }
}
