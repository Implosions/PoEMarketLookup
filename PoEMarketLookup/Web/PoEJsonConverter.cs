using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.ViewModels;
using System;
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

        private readonly ItemViewModel _vm;
        private readonly StatRepository _statRepo = StatRepository.GetRepository();

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
            var root = new JObject();

            root.CreateProperty("sort")
                .CreateObject()
                .CreateProperty("price")
                .Value = "asc";

            // Query
            var query = root.CreateProperty("query")
                            .CreateObject();

            query.CreateProperty("status")
                 .CreateObject()
                 .CreateProperty("option")
                 .Value = "any";

            if (_vm.ItemRarity == Rarity.Unique)
            {
                query.CreateProperty("name")
                     .Value = _vm.ItemName;
                query.CreateProperty("type")
                     .Value = _vm.ItemBase;
            }
            else if (_vm.ItemType == PoEItemType.Gem || _vm.ItemType == PoEItemType.Fragment)
            {
                query.CreateProperty("type")
                     .Value = _vm.ItemBase;
            }
            else if (_vm.ItemType== PoEItemType.Prophecy)
            {
                query.CreateProperty("name")
                     .Value = _vm.ItemBase;
                query.CreateProperty("type")
                     .Value = "Prophecy";
            }

            query.CreateProperty("stats")
                 .SetValue(new JArray())
                 .Add(new JObject()
                 {
                     new JProperty("type", "and"),
                     new JProperty("filters", CreateItemStatFilters())
                 });

            // Filters
            var filters = query.CreateProperty("filters")
                               .CreateObject();

            if (_vm.ItemType.IsWeapon())
            {
                filters.Add(
                    CreateFilterCategory("weapon_filters", CreateWeaponFilters()));
            }
            else if (_vm.ItemType.IsArmor())
            {
                filters.Add(
                    CreateFilterCategory("armour_filters", CreateArmorFilters()));
            }

            filters.Add(
                    CreateFilterCategory("socket_filters", CreateSocketFilters()));
            filters.Add(
                CreateFilterCategory("misc_filters", CreateMiscFilters()));
            filters.Add(
                CreateFilterCategory("type_filters", CreateTypeFilters()));
            filters.Add(
                CreateFilterCategory("trade_filters", CreateSaleTypeFilters()));

            return root.ToString(Formatting.None);
        }

        private JObject CreateWeaponFilters()
        {

            var filters = new JObject();

            if (IsChecked(_vm.WeaponDPS))
            {
                filters.CreateProperty("dps")
                     .SetValue(CreateMinAndMaxObject(_vm.WeaponDPS.Value));
            }

            if (IsChecked(_vm.WeaponEDPS))
            {
                filters.CreateProperty("edps")
                     .SetValue(CreateMinAndMaxObject(_vm.WeaponEDPS.Value));
            }

            if (IsChecked(_vm.WeaponPDPS))
            {
                filters.CreateProperty("pdps")
                     .SetValue(CreateMinAndMaxObject(_vm.WeaponPDPS.Value));
            }

            if (IsChecked(_vm.WeaponAPS))
            {
                filters.CreateProperty("aps")
                     .SetValue(CreateMinAndMaxObject(_vm.WeaponAPS.Value));
            }

            if (IsChecked(_vm.WeaponCritChance))
            {
                filters.CreateProperty("crit")
                       .SetValue(CreateMinAndMaxObject(_vm.WeaponCritChance.Value));
            }

            return filters;
        }

        private JObject CreateArmorFilters()
        {
            var filters = new JObject();

            if (IsChecked(_vm.ArmorAR))
            {
                filters.CreateProperty("ar")
                       .SetValue(CreateMinAndMaxObject(_vm.ArmorAR.Value));
            }

            if (IsChecked(_vm.ArmorEV))
            {
                filters.CreateProperty("ev")
                       .SetValue(CreateMinAndMaxObject(_vm.ArmorEV.Value));
            }

            if (IsChecked(_vm.ArmorES))
            {
                filters.CreateProperty("es")
                       .SetValue(CreateMinAndMaxObject(_vm.ArmorES.Value));
            }

            return filters;
        }

        private JArray CreateItemStatFilters()
        {
            var filters = new JArray();

            if(IsChecked(_vm.TotalLife))
            {
                filters.Add(new JObject()
                {
                    new JProperty("id", "pseudo.pseudo_total_life"),
                    new JProperty("value", CreateMinAndMaxObject(_vm.TotalLife.Value))
                });
            }

            if(IsChecked(_vm.TotalResistances))
            {
                filters.Add(new JObject()
                {
                    new JProperty("id", "pseudo.pseudo_total_resistance"),
                    new JProperty("value", CreateMinAndMaxObject(_vm.TotalResistances.Value))
                });
            }

            if (IsChecked(_vm.ItemEnchant))
            {
                var enchant = CreateAffixObject(_vm.ItemEnchant.Mod.Affix, "enchant");

                if(enchant != null)
                {
                    enchant.CreateProperty("value")
                       .CreateObject()
                       .CreateProperty("min")
                       .Value = _vm.ItemEnchant.Mod.GetAverageValue();

                    filters.Add(enchant);
                }
            }

            filters.AddAll(CreateAffixList(_vm.ItemImplicits, "implicit"));
            filters.AddAll(CreateAffixList(_vm.ItemExplicits, "explicit"));

            return filters;
        }

        private JObject CreateSocketFilters()
        {
            var filters = new JObject();

            if(IsChecked(_vm.SocketCount))
            {
                filters.CreateProperty("sockets")
                       .CreateObject()
                       .CreateProperty("min")
                       .Value = _vm.SocketCount.Value;
            }

            if(_vm.Link != null && _vm.Link.Checked)
            {
                filters.CreateProperty("links")
                       .CreateObject()
                       .CreateProperty("min")
                       .Value = _vm.Link.Value;
            }

            return filters;
        }

        private JObject CreateMiscFilters()
        {
            var filters = new JObject();

            if(IsChecked(_vm.ShaperBase))
            {
                filters.CreateProperty("shaper_item")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _vm.ShaperBase.Value;
            }

            if(IsChecked(_vm.ElderBase))
            {
                filters.CreateProperty("elder_item")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _vm.ElderBase.Value;
            }

            if(IsChecked(_vm.CorruptedItem))
            {
                filters.CreateProperty("corrupted")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _vm.CorruptedItem.Value;
            }

            if(IsChecked(_vm.MirroredItem))
            {
                filters.CreateProperty("mirrored")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _vm.MirroredItem.Value;
            }

            if(IsChecked(_vm.SynthesisedItem))
            {
                filters.CreateProperty("synthesised_item")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _vm.SynthesisedItem.Value;
            }

            if (IsChecked(_vm.FracturedItem))
            {
                filters.CreateProperty("fractured_item")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _vm.FracturedItem.Value;
            }

            if(IsChecked(_vm.ItemLevel))
            {
                filters.CreateProperty("gem_level")
                       .SetValue(CreateMinAndMaxObject(_vm.ItemLevel.Value));
            }

            if(IsChecked(_vm.ItemQuality))
            {
                filters.CreateProperty("quality")
                       .SetValue(CreateMinAndMaxObject(_vm.ItemQuality.Value));
            }

            return filters;
        }

        private JObject CreateTypeFilters()
        {
            var filters = new JObject();

            if(_itemCategoryDefinitions.ContainsKey(_vm.ItemType))
            {
                filters.CreateProperty("category")
                       .CreateObject()
                       .CreateProperty("option")
                       .Value = _itemCategoryDefinitions[_vm.ItemType];
            }

            return filters;
        }

        private JObject CreateSaleTypeFilters()
        {
            var filters = new JObject();

            filters.CreateProperty("sale_type")
                   .CreateObject()
                   .CreateProperty("option")
                   .Value = "priced";

            filters.CreateProperty("indexed")
                   .CreateObject()
                   .CreateProperty("option")
                   .Value = "1week";

            filters.CreateProperty("price")
                   .CreateObject()
                   .CreateProperty("min")
                   .Value = .00001;

            return filters;
        }

        private JProperty CreateFilterCategory(string category, JToken value)
        {
            var filter = new JProperty(category);
            filter.CreateObject()
                  .CreateProperty("filters")
                  .Value = value;

            return filter;
        }

        private JObject CreateMinAndMaxObject(double stat)
        {
            return new JObject()
            {
                new JProperty("min", Math.Round(stat * _lowerPercentage, 2)),
                new JProperty("max", Math.Round(stat * _upperPercentage, 2))
            };
        }

        private JObject CreateAffixObject(string affix, string prefix)
        {
            string id = _statRepo.GetStatId(affix);

            if(id == null)
            {
                return null;
            }

            var obj = new JObject();

            obj.CreateProperty("id")
               .Value = prefix + '.' + id;

            return obj;
        }

        private IList<JObject> CreateAffixList(IList<ItemModContainer> mods, string prefix)
        {
            var affixObjs = new List<JObject>();

            if (mods == null)
            {
                return affixObjs;
            }

            foreach (var container in mods)
            {
                if (!container.Checked)
                {
                    continue;
                }

                string stat = container.Mod.Affix;
                if ((_vm.ItemType.IsWeapon() && _weaponLocalMods.Contains(stat)) // If weapon and stat is a weapon local mod
                    || (_vm.ItemType.IsArmor() && _armorLocalMods.Contains(stat))) // or armor and is a armor local mod
                {
                    stat += " (Local)";
                }

                var affix = CreateAffixObject(stat, prefix);

                if(affix == null)
                {
                    continue;
                }

                affix.CreateProperty("value")
                     .SetValue(CreateMinAndMaxObject(container.Mod.GetAverageValue()));

                affixObjs.Add(affix);
            }

            return affixObjs;
        }

        private bool IsChecked(ItemField field)
        {
            return field != null && field.Checked;
        }
    }
}
