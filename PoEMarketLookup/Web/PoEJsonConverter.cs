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

        private static readonly IList<string> _weaponLocalMods = new ReadOnlyCollection<string>(
            new List<string>
            {
                "Adds # to # Chaos Damage",
                "# to Accuracy Rating",
                "#% increased Attack Speed",
                "Adds # to # Lightning Damage",
                "Adds # to # Fire Damage",
                "Adds # to # Physical Damage",
                "Adds # to # Cold Damage",
                "#% chance to Poison on Hit"
            });

        private static readonly IList<string> _armorLocalMods = new ReadOnlyCollection<string>(
            new List<string>
            {
                "# to maximum Energy Shield",
                "#% increased Evasion Rating",
                "#% increased Armour"
            });

        private ItemViewModel _vm;
        private StatRepository _statRepo = StatRepository.GetRepository();

        private readonly double _lowerPercentage;
        private readonly double _upperPercentage;

        public PoEJsonConverter(ItemViewModel vm, double lowerBound = 90, double upperBound = 110)
        {
            _vm = vm;
            _lowerPercentage = lowerBound / 100.0;
            _upperPercentage = upperBound / 100.0;
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

            if(_vm.ItemRarity == Rarity.Unique)
            {
                query.Add(new JProperty("name", _vm.ItemName));
                query.Add(new JProperty("type", _vm.ItemBase));
            }

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
                new JProperty("min", stat * _lowerPercentage),
                new JProperty("max", stat * _upperPercentage)
            };
        }

        private JArray CreateItemStatFilters()
        {
            var filters = new JArray();

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
                string id = _statRepo.GetStatId(stat);

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

            AddStatsToFilter(filters, _vm.ItemImplicits, "implicit.");
            AddStatsToFilter(filters, _vm.ItemExplicits, "explicit.");

            return filters;
        }

        private void AddStatsToFilter(JArray filters, IList<ItemModContainer> affixes, string idPrefix)
        {
            if (affixes == null)
            {
                return;
            }

            foreach (var container in affixes)
            {
                if (!container.Checked)
                {
                    continue;
                }

                string stat = container.Mod.Affix;
                int type = (int)_vm.ItemType;

                if((type >= 200 && type < 300 && _weaponLocalMods.Contains(stat)) // If weapon and stat is a weapon local mod
                    || (type >= 400 && _armorLocalMods.Contains(stat))) // or armor and is a armor local mod
                {
                    stat += " (Local)";
                }

                string id = _statRepo.GetStatId(stat);

                if (id == null)
                {
                    continue;
                }

                filters.Add(new JObject()
                {
                    new JProperty("id", idPrefix + id),
                    new JProperty("value", CreateStatValuesObj(container.Mod.GetAverageValue()))
                });
            }
        }

        private JProperty CreateSocketFilters()
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

            if (_vm.FracturedItem != null && _vm.FracturedItem.Checked)
            {
                filters.Add(CreateItemPropertyFilter("fractured_item", _vm.FracturedItem.Value));
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
