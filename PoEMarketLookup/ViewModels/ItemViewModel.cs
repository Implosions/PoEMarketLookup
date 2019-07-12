using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System.Collections.Generic;

namespace PoEMarketLookup.ViewModels
{
    public class ItemViewModel
    {
        public PoEItemType ItemType { get; set; }
        public string ItemBase { get; set; }
        public string ItemName { get; set; }

        public ItemStat<double> WeaponDPS { get; set; }
        public ItemStat<double> WeaponPDPS { get; set; }
        public ItemStat<double> WeaponEDPS { get; set; }
        public ItemStat<double> WeaponAPS { get; set; }

        public ItemStat<int> ArmorAR { get; set; }
        public ItemStat<int> ArmorEV { get; set; }
        public ItemStat<int> ArmorES { get; set; }

        public ItemModContainer ItemEnchant { get; set; }
        public IList<ItemField> ItemStats { get; set; }
        public IList<ItemModContainer> ItemImplicits { get; set; }
        public IList<ItemModContainer> ItemExplicits { get; set; }

        public ItemStat<bool> ShaperBase { get; set; }
        public ItemStat<bool> ElderBase { get; set; }
        public ItemStat<bool> CorruptedItem { get; set; }
        public ItemStat<bool> MirroredItem { get; set; }
        public ItemStat<bool> SynthesisedItem { get; set; }

        public ItemStat<int> SocketCount { get; set; }

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

                vm.ShaperBase = new ItemStat<bool>("Shaper", mi.Shaper);
                vm.ElderBase = new ItemStat<bool>("Elder", mi.Elder);
                vm.CorruptedItem = new ItemStat<bool>("Corrupted", mi.Corrupted);
                vm.MirroredItem = new ItemStat<bool>("Mirrored", mi.Mirrored);
                vm.SynthesisedItem = new ItemStat<bool>("Synthesised", mi.Synthesised);

                vm.SocketCount = new ItemStat<int>("Sockets", mi.Sockets == null ? 0 : mi.Sockets.Sockets);

                if (item is Weapon weapon)
                {
                    vm.WeaponDPS = new ItemStat<double>("Total DPS", weapon.GetTotalDPS(!weapon.Corrupted));
                    vm.WeaponPDPS = new ItemStat<double>("PDPS", weapon.GetPhysicalDPS(!weapon.Corrupted));
                    vm.WeaponEDPS = new ItemStat<double>("EDPS", weapon.GetElementalDPS());
                    vm.WeaponAPS = new ItemStat<double>("APS", weapon.AttacksPerSecond);

                    vm.ItemStats = new List<ItemField>
                    {
                        vm.WeaponDPS,
                        vm.WeaponPDPS,
                        vm.WeaponEDPS,
                        vm.WeaponAPS
                    };
                }
                else if(item is Armor armor)
                {
                    vm.ArmorAR = new ItemStat<int>("Armour", 
                        armor.Corrupted ? armor.Armour : armor.GetNormalizedArmourValue());
                    vm.ArmorEV = new ItemStat<int>("Evasion", 
                        armor.Corrupted ? armor.EvasionRating : armor.GetNormalizedEvasionValue());
                    vm.ArmorES = new ItemStat<int>("Energy Shield", 
                        armor.Corrupted ? armor.EnergyShield : armor.GetNormalizedEnergyShieldValue());

                    vm.ItemStats = new List<ItemField>
                    {
                        vm.ArmorAR,
                        vm.ArmorEV,
                        vm.ArmorES
                    };

                    if(armor.Enchantment != null)
                    {
                        vm.ItemEnchant = new ItemModContainer(armor.Enchantment);
                    }
                }

                if(vm.ItemStats == null)
                {
                    vm.ItemStats = new List<ItemField>();
                }

                if (vm.ShaperBase.Value)
                {
                    vm.ItemStats.Add(vm.ShaperBase);
                    vm.ShaperBase.Checked = true;
                }

                if (vm.ElderBase.Value)
                {
                    vm.ItemStats.Add(vm.ElderBase);
                    vm.ElderBase.Checked = true;
                }

                if (vm.CorruptedItem.Value)
                {
                    vm.ItemStats.Add(vm.CorruptedItem);
                    vm.CorruptedItem.Checked = true;
                }

                if (vm.MirroredItem.Value)
                {
                    vm.ItemStats.Add(vm.MirroredItem);
                    vm.MirroredItem.Checked = true;
                }

                if (vm.SynthesisedItem.Value)
                {
                    vm.ItemStats.Add(vm.SynthesisedItem);
                    vm.SynthesisedItem.Checked = true;
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
