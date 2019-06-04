﻿using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System.Collections.Generic;

namespace PoEMarketLookup.ViewModels
{
    public class ItemViewModel
    {
        public string ItemBase { get; private set; }
        public string ItemName { get; private set; }

        public ItemModContainer ItemEnchant { get; private set; }
        public IList<ItemStat> ItemStats { get; private set; }
        public IList<ItemModContainer> ItemImplicits { get; private set; }
        public IList<ItemModContainer> ItemExplicits { get; private set; }

        private ItemViewModel() { }

        public static ItemViewModel CreateViewModel(PoEItem item)
        {
            var vm = new ItemViewModel
            {
                ItemBase = item.Base
            };

            if(item is ModdableItem mi)
            {
                vm.ItemImplicits = WrapMods(mi.ImplicitMods);
                vm.ItemExplicits = WrapMods(mi.ExplicitMods);

                if(item is Weapon weapon)
                {
                    vm.ItemStats = new List<ItemStat>
                    {
                        new ItemStat("Total DPS", weapon.GetTotalDPS(!weapon.Corrupted)),
                        new ItemStat("PDPS", weapon.GetPhysicalDPS(!weapon.Corrupted)),
                        new ItemStat("EDPS", weapon.GetElementalDPS()),
                        new ItemStat("APS", weapon.AttacksPerSecond)
                    };
                }
                else if(item is Armor armor)
                {
                    vm.ItemStats = new List<ItemStat>
                    {
                        new ItemStat("Armour", armor.Corrupted ? armor.Armour : armor.GetNormalizedArmourValue()),
                        new ItemStat("Evasion", armor.Corrupted ? armor.EvasionRating : armor.GetNormalizedEvasionValue()),
                        new ItemStat("Energy Shield", armor.Corrupted ? armor.EnergyShield : armor.GetNormalizedEnergyShieldValue())
                    };
                }
            }

            return vm;
        }

        private static IList<ItemModContainer> WrapMods(Mod[] mods)
        {
            if(mods == null)
            {
                return null;
            }

            var wrappedMods = new List<ItemModContainer>();

            foreach(var mod in mods)
            {
                wrappedMods.Add(new ItemModContainer(mod));
            }

            return wrappedMods;
        }
    }
}
