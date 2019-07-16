using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE;
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

            filters.Add(CreateSocketFilters());
            filters.Add(CreateMiscFilters());
            
            var stats = new JArray()
            {
                new JObject()
                {
                    new JProperty("type", "and"),
                    new JProperty("filters", CreateItemStatFilters())
                }
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

        private JArray CreateItemStatFilters()
        {
            var filters = new JArray();
            var repo = StatRepository.GetRepository();

            if(_vm.TotalLife != null && _vm.TotalLife.Checked)
            {
                filters.Add(new JObject()
                {
                    new JProperty("id", "pseudo.pseudo_total_life"),
                    new JProperty("value", CreateStatValuesObj(_vm.TotalLife.Value))
                });
            }

            if(_vm.TotalResistances != null && _vm.TotalResistances.Checked)
            {
                filters.Add(new JObject()
                {
                    new JProperty("id", "pseudo.pseudo_total_resistance"),
                    new JProperty("value", CreateStatValuesObj(_vm.TotalResistances.Value))
                });
            }

            if (_vm.ItemEnchant != null && _vm.ItemEnchant.Checked)
            {
                string stat = _vm.ItemEnchant.Mod.Affix;
                string id = repo.GetStatId(stat);

                if(id != null)
                {
                    var enchantFilter = new JObject()
                    {
                        new JProperty("id", "enchant." + id)
                    };

                    filters.Add(enchantFilter);
                }
            }

            if(_vm.ItemImplicits != null)
            {
                foreach(var container in _vm.ItemImplicits)
                {
                    if (!container.Checked)
                    {
                        continue;
                    }

                    string stat = container.Mod.Affix;
                    string id = repo.GetStatId(stat);

                    if(id == null)
                    {
                        continue;
                    }

                    filters.Add(new JObject()
                    {
                        new JProperty("id", "implicit." + id)
                    });
                }
            }

            if(_vm.ItemExplicits != null)
            {
                foreach(var container in _vm.ItemExplicits)
                {
                    if (!container.Checked)
                    {
                        continue;
                    }

                    string stat = container.Mod.Affix;
                    string id = repo.GetStatId(stat);

                    if(id == null)
                    {
                        continue;
                    }

                    filters.Add(new JObject()
                    {
                        new JProperty("id", "explicit." + id)
                    });
                }
            }

            return filters;
        }

        public JProperty CreateSocketFilters()
        {
            var filters = new JObject();

            if(_vm.SocketCount != null && _vm.SocketCount.Checked)
            {
                var val = new JObject()
                {
                    new JProperty("min", _vm.SocketCount.Value)
                };

                filters.Add(new JProperty("sockets", val));
            }

            if(_vm.Link != null && _vm.Link.Checked)
            {
                var val = new JObject()
                {
                    new JProperty("min", _vm.Link.Value)
                };

                filters.Add(new JProperty("links", val));
            }

            return new JProperty("socket_filters", new JObject()
            {
                new JProperty("filters", filters)
            });
        }

        private JProperty CreateMiscFilters()
        {
            var filters = new JObject();

            if(_vm.ShaperBase != null && _vm.ShaperBase.Checked)
            {
                var val = new JObject()
                {
                    new JProperty("option", _vm.ShaperBase.Value)
                };

                filters.Add(new JProperty("shaper_item", val));
            }

            if(_vm.ElderBase != null && _vm.ElderBase.Checked)
            {
                filters.Add(new JProperty("elder_item"));
            }

            return new JProperty("misc_filters", new JObject()
            {
                new JProperty("filters", filters)
            });
        }
    }
}
