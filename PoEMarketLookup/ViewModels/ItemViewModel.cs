using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System.Collections.Generic;

namespace PoEMarketLookup.ViewModels
{
    public class ItemViewModel
    {
        public PoEItemType ItemType { get; private set; }
        public string ItemBase { get; private set; }
        public string ItemName { get; private set; }

        public ItemStat WeaponDPS { get; private set; }
        public ItemStat WeaponPDPS { get; private set; }
        public ItemStat WeaponEDPS { get; private set; }
        public ItemStat WeaponAPS { get; private set; }

        public ItemStat ArmorAR { get; private set; }
        public ItemStat ArmorEV { get; private set; }
        public ItemStat ArmorES { get; private set; }

        public ItemModContainer ItemEnchant { get; private set; }
        public IList<ItemStat> ItemStats { get; private set; }
        public IList<ItemModContainer> ItemImplicits { get; private set; }
        public IList<ItemModContainer> ItemExplicits { get; private set; }

        private ItemViewModel() { }

        public static ItemViewModel CreateViewModel(PoEItem item)
        {
            var vm = new ItemViewModel
            {
                ItemBase = item.Base,
                ItemType = item.Category
            };

            if(item is ModdableItem mi)
            {
                vm.ItemImplicits = WrapMods(mi.ImplicitMods);
                vm.ItemExplicits = WrapMods(mi.ExplicitMods);
                vm.ItemName = mi.Name;

                if(item is Weapon weapon)
                {
                    vm.WeaponDPS = new ItemStat("Total DPS", weapon.GetTotalDPS(!weapon.Corrupted));
                    vm.WeaponPDPS = new ItemStat("PDPS", weapon.GetPhysicalDPS(!weapon.Corrupted));
                    vm.WeaponEDPS = new ItemStat("EDPS", weapon.GetElementalDPS());
                    vm.WeaponAPS = new ItemStat("APS", weapon.AttacksPerSecond);

                    vm.ItemStats = new List<ItemStat>
                    {
                        vm.WeaponDPS,
                        vm.WeaponPDPS,
                        vm.WeaponEDPS,
                        vm.WeaponAPS
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

                    if(armor.Enchantment != null)
                    {
                        vm.ItemEnchant = new ItemModContainer(armor.Enchantment);
                    }
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
