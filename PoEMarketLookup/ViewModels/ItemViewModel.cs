using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using System.Collections.Generic;

namespace PoEMarketLookup.ViewModels
{
    public class ItemViewModel
    {
        public PoEItemType ItemType { get; set; }
        public Rarity ItemRarity { get; set; }
        public string ItemBase { get; set; }
        public string ItemName { get; set; }

        public ItemStat<double> WeaponDPS { get; set; }
        public ItemStat<double> WeaponPDPS { get; set; }
        public ItemStat<double> WeaponEDPS { get; set; }
        public ItemStat<double> WeaponAPS { get; set; }
        public ItemStat<double> WeaponCritChance { get; set; }

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
        public ItemStat<bool> FracturedItem { get; set; }

        public ItemStat<int> SocketCount { get; set; }
        public ItemStat<int> Link { get; set; }
        public ItemStat<int> TotalResistances { get; set; }
        public ItemStat<int> TotalLife { get; set; }
        public ItemStat<int> ItemLevel { get; set; }
        public ItemStat<int> ItemQuality { get; set; }

        public static ItemViewModel CreateViewModel(PoEItem item)
        {
            var vm = new ItemViewModel
            {
                ItemBase = item.Base,
                ItemType = item.Category,
                ItemStats = new List<ItemField>()
            };

            if(item is CorruptableItem ci)
            {
                vm.CorruptedItem = new ItemStat<bool>("Corrupted", ci.Corrupted);

                if (ci is Gem g)
                {
                    vm.ItemLevel = new ItemStat<int>("Level", g.Level);
                    vm.ItemQuality = new ItemStat<int>("Quality", g.Quality);
                }
            }
            if(item is ModdableItem mi)
            {
                vm.ItemImplicits = WrapMods(mi.ImplicitMods);
                vm.ItemExplicits = WrapMods(mi.ExplicitMods);
                vm.ItemName = mi.Name;
                vm.ItemRarity = mi.Rarity;

                vm.ShaperBase = new ItemStat<bool>("Shaper", mi.Shaper);
                vm.ElderBase = new ItemStat<bool>("Elder", mi.Elder);
                vm.MirroredItem = new ItemStat<bool>("Mirrored", mi.Mirrored);
                vm.SynthesisedItem = new ItemStat<bool>("Synthesised", mi.Synthesised);
                vm.FracturedItem = new ItemStat<bool>("Fractured", mi.Fractured);

                vm.SocketCount = new ItemStat<int>("Sockets", mi.Sockets == null ? 0 : mi.Sockets.Sockets);
                vm.Link = new ItemStat<int>("Link", mi.Sockets == null ? 0 : mi.Sockets.LargestLink);
                vm.TotalLife = new ItemStat<int>("Total Life", mi.TotalLife);
                vm.TotalResistances = new ItemStat<int>("Total Resistances", 
                    mi.FireResistance
                    + mi.ColdResistance
                    + mi.LightningResistance
                    + mi.ChaosResistance);

                if (item is Weapon weapon)
                {
                    vm.WeaponDPS = new ItemStat<double>("Total DPS", weapon.GetTotalDPS(!weapon.Corrupted));
                    vm.WeaponPDPS = new ItemStat<double>("PDPS", weapon.GetPhysicalDPS(!weapon.Corrupted));
                    vm.WeaponEDPS = new ItemStat<double>("EDPS", weapon.GetElementalDPS());
                    vm.WeaponAPS = new ItemStat<double>("APS", weapon.AttacksPerSecond);
                    vm.WeaponCritChance = new ItemStat<double>("Crit Chance", weapon.CriticalStrikeChance);
                }
                else if(item is Armor armor)
                {
                    vm.ArmorAR = new ItemStat<int>("Armour", 
                        armor.Corrupted ? armor.Armour : armor.GetNormalizedArmourValue());
                    vm.ArmorEV = new ItemStat<int>("Evasion", 
                        armor.Corrupted ? armor.EvasionRating : armor.GetNormalizedEvasionValue());
                    vm.ArmorES = new ItemStat<int>("Energy Shield", 
                        armor.Corrupted ? armor.EnergyShield : armor.GetNormalizedEnergyShieldValue());

                    if(armor.Enchantment != null)
                    {
                        vm.ItemEnchant = new ItemModContainer(armor.Enchantment);
                    }
                }
            }

            vm.AddPropertiesToStatsList();
            vm.AutoCheckProperties();

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

        private void AddPropertiesToStatsList()
        {
            AddIfGreaterThan(TotalLife, 0);
            AddIfGreaterThan(TotalResistances, 0);

            AddIfNotNull(WeaponDPS);
            AddIfNotNull(WeaponPDPS);
            AddIfNotNull(WeaponEDPS);
            AddIfNotNull(WeaponAPS);
            AddIfNotNull(WeaponCritChance);

            AddIfNotNull(ArmorAR);
            AddIfNotNull(ArmorEV);
            AddIfNotNull(ArmorES);

            AddIfNotNull(ItemLevel);
            AddIfNotNull(ItemQuality);

            AddIfTrue(ShaperBase);
            AddIfTrue(ElderBase);
            AddIfTrue(CorruptedItem);
            AddIfTrue(MirroredItem);
            AddIfTrue(SynthesisedItem);
            AddIfTrue(FracturedItem);

            AddIfGreaterThan(SocketCount, 0);
            AddIfGreaterThan(Link, 0);
        }

        private void AddIfNotNull(ItemField field)
        {
            if(field != null)
            {
                ItemStats.Add(field);
            }
        }

        private void AddIfGreaterThan<T>(ItemStat<T> stat, T value) where T : System.IComparable<T>
        {
            if (stat != null && stat.Value.CompareTo(value) > value.CompareTo(stat.Value))
            {
                ItemStats.Add(stat);
            }
        }

        private void AddIfTrue(ItemStat<bool> stat)
        {
            if (stat != null && stat.Value)
            {
                ItemStats.Add(stat);
            }
        }

        private void AutoCheckProperties()
        {
            CheckIfGreaterThan(TotalLife, 39);
            CheckIfGreaterThan(TotalResistances, 29);
            CheckIfGreaterThan(SocketCount, 5);
            CheckIfGreaterThan(Link, 4);
            CheckIfGreaterThan(ItemQuality, 9);

            CheckIfTrue(ShaperBase);
            CheckIfTrue(ElderBase);
            CheckIfTrue(CorruptedItem);
            CheckIfTrue(MirroredItem);
            CheckIfTrue(SynthesisedItem);
            CheckIfTrue(FracturedItem);

            if (ItemRarity == Rarity.Unique)
            {
                foreach (var ex in ItemExplicits)
                {
                    ex.Checked = true;
                }
            }

            if(ItemLevel != null)
            {
                ItemLevel.Checked = true;
            }
        }

        private void CheckIfGreaterThan<T>(ItemStat<T> stat, T value) where T : System.IComparable<T>
        {
            if (stat != null && stat.Value.CompareTo(value) > value.CompareTo(stat.Value))
            {
                stat.Checked = true;
            }
        }

        private void CheckIfTrue(ItemStat<bool> stat)
        {
            if(stat != null && stat.Value)
            {
                stat.Checked = true;
            }
        }
    }
}
