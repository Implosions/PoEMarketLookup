using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PoEMarketLookup.Web
{
    public class PoEJsonConverter
    {
        private static readonly IDictionary<PoEItemType, string> _itemCategoryDefinitions = new ReadOnlyDictionary<PoEItemType, string>(
            new Dictionary<PoEItemType, string>()
            {
                { PoEItemType.Currency, "currency" },
                { PoEItemType.Gem, "gem" },
                { PoEItemType.Flask, "flask" },
                { PoEItemType.Map, "map" },
                { PoEItemType.Sword1H, "weapon.onesword" },
                { PoEItemType.Axe1H, "weapon.oneaxe" },
                { PoEItemType.Mace1H, "weapon.onemace" },
                { PoEItemType.Dagger, "weapon.dagger" },
                { PoEItemType.Claw, "weapon.claw" },
                { PoEItemType.Sceptre, "weapon.sceptre" },
                { PoEItemType.Wand, "weapon.wand" },
                { PoEItemType.Sword2H, "weapon.twosword" },
                { PoEItemType.Axe2H, "weapon.twoaxe" },
                { PoEItemType.Mace2H, "weapon.twomace" },
                { PoEItemType.Staff, "weapon.staff" },
                { PoEItemType.Bow, "weapon.bow" },
                { PoEItemType.FishingRod, "weapon.rod" },
                { PoEItemType.Amulet, "accessory.amulet" },
                { PoEItemType.Ring, "accessory.ring" },
                { PoEItemType.Belt, "accessory.belt" },
                { PoEItemType.Quiver, "armour.quiver" },
                { PoEItemType.Jewel, "jewel" },
                { PoEItemType.Helmet, "armour.helmet" },
                { PoEItemType.Gloves, "armour.gloves" },
                { PoEItemType.Boots, "armour.boots" },
                { PoEItemType.BodyArmor, "armour.chest" },
                { PoEItemType.Shield, "armour.shield" }
            });

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
            filters.Add(CreateTypeFilters());
            
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

            if (_vm.WeaponDPS != null && _vm.WeaponDPS.Checked)
            {
                stats.Add(new JProperty("dps", 
                    CreateStatValuesObj(_vm.WeaponDPS.Value)));
            }

            if (_vm.WeaponEDPS != null && _vm.WeaponEDPS.Checked)
            {
                stats.Add(new JProperty("edps", 
                    CreateStatValuesObj(_vm.WeaponEDPS.Value)));
            }

            if (_vm.WeaponPDPS != null && _vm.WeaponPDPS.Checked)
            {
                stats.Add(new JProperty("pdps", 
                    CreateStatValuesObj(_vm.WeaponPDPS.Value)));
            }

            if (_vm.WeaponAPS != null && _vm.WeaponAPS.Checked)
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

            if (_vm.ArmorAR != null && _vm.ArmorAR.Checked)
            {
                stats.Add(new JProperty("ar",
                    CreateStatValuesObj(_vm.ArmorAR.Value)));
            }

            if (_vm.ArmorEV != null && _vm.ArmorEV.Checked)
            {
                stats.Add(new JProperty("ev",
                    CreateStatValuesObj(_vm.ArmorEV.Value)));
            }

            if (_vm.ArmorES != null && _vm.ArmorES.Checked)
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
                    var value = new JObject()
                    {
                        new JProperty("min", _vm.ItemEnchant.Mod.GetAverageValue())
                    };

                    var enchantFilter = new JObject()
                    {
                        new JProperty("id", "enchant." + id),
                        new JProperty("value", value)
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
                filters.Add(CreateItemPropertyFilter("shaper_item", _vm.ShaperBase.Value));
            }

            if(_vm.ElderBase != null && _vm.ElderBase.Checked)
            {
                filters.Add(CreateItemPropertyFilter("elder_item", _vm.ElderBase.Value));
            }

            if(_vm.CorruptedItem != null && _vm.CorruptedItem.Checked)
            {
                filters.Add(CreateItemPropertyFilter("corrupted", _vm.CorruptedItem.Value));
            }

            if(_vm.MirroredItem != null && _vm.MirroredItem.Checked)
            {
                filters.Add(CreateItemPropertyFilter("mirrored", _vm.MirroredItem.Value));
            }

            if(_vm.SynthesisedItem != null && _vm.SynthesisedItem.Checked)
            {
                filters.Add(CreateItemPropertyFilter("synthesised_item", _vm.SynthesisedItem.Value));
            }

            return new JProperty("misc_filters", new JObject()
            {
                new JProperty("filters", filters)
            });
        }

        private JProperty CreateItemPropertyFilter(string name, bool val)
        {
            var option = new JObject()
            {
                new JProperty("option", val)
            };

            return new JProperty(name, option);
        }

        private JProperty CreateTypeFilters()
        {
            var filters = new JObject();

            if(_itemCategoryDefinitions.ContainsKey(_vm.ItemType))
            {
                var category = new JObject()
                {
                    new JProperty("option", _itemCategoryDefinitions[_vm.ItemType])
                };

                filters.Add(new JProperty("category", category));
            }

            return new JProperty("type_filters", new JObject()
            {
                new JProperty("filters", filters)
            });
        }
    }
}
