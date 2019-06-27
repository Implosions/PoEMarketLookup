using Newtonsoft.Json.Linq;
using PoEMarketLookup.ViewModels;

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
                filters.Add(CreateWeaponStatsFilters());
            }
            else if(category >= 400)
            {
                filters.Add(CreateArmorStatsFilters());
            }

            var stats = new JArray()
            {
                new JObject()
            };

            var query = new JObject()
            {
                new JProperty("status", "any"),
                new JProperty("filters", filters),
                new JProperty("stats", stats)
            };

            var root = new JObject
            {
                new JProperty("query", query)
            };

            return root.ToString();
        }

        private JProperty CreateWeaponStatsFilters()
        {

            var stats = new JObject();

            if (_vm.WeaponDPS.Checked)
            {
                stats.Add(new JProperty("dps", 
                    CreateStatValuesObj(_vm.WeaponDPS.Value)));
            }

            if (_vm.WeaponEDPS.Checked)
            {
                stats.Add(new JProperty("edps", 
                    CreateStatValuesObj(_vm.WeaponEDPS.Value)));
            }

            if (_vm.WeaponPDPS.Checked)
            {
                stats.Add(new JProperty("pdps", 
                    CreateStatValuesObj(_vm.WeaponPDPS.Value)));
            }

            if (_vm.WeaponAPS.Checked)
            {
                stats.Add(new JProperty("aps", 
                    CreateStatValuesObj(_vm.WeaponAPS.Value)));
            }

            var filters = new JObject()
            {
                new JProperty("filters", stats)
            };

            var root = new JProperty("weapon_filters", filters);
            
            return root;
        }

        private JProperty CreateArmorStatsFilters()
        {
            var stats = new JObject();

            if (_vm.ArmorAR.Checked)
            {
                stats.Add(new JProperty("ar",
                    CreateStatValuesObj(_vm.ArmorAR.Value)));
            }

            if (_vm.ArmorEV.Checked)
            {
                stats.Add(new JProperty("ev",
                    CreateStatValuesObj(_vm.ArmorEV.Value)));
            }

            if (_vm.ArmorES.Checked)
            {
                stats.Add(new JProperty("es",
                    CreateStatValuesObj(_vm.ArmorES.Value)));
            }

            var filters = new JObject()
            {
                new JProperty("filters", stats)
            };

            return new JProperty("armour_filters", filters);
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
