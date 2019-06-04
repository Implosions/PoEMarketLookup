using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.ViewModels;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class ItemViewModelTest
    {
        private class MockPoEItem : PoEItem
        {
        }

        private class MockModdableItem : ModdableItem
        {
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithItemsBase()
        {
            var item = new MockPoEItem()
            {
                Base = "Foo"
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual("Foo", vm.ItemBase);
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithItemsImplicitModsWrappedInItemModContainers()
        {
            var mods = new Mod[]
            {
                Mod.Parse("Foo"),
                Mod.Parse("Bar")
            };

            var item = new MockModdableItem()
            {
                ImplicitMods = mods
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(mods[0], vm.ItemImplicits[0].Mod);
            Assert.AreEqual(mods[1], vm.ItemImplicits[1].Mod);
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithItemsExplicitModsWrappedInItemModContainers()
        {
            var mods = new Mod[]
            {
                Mod.Parse("Foo"),
                Mod.Parse("Bar")
            };

            var item = new MockModdableItem()
            {
                ExplicitMods = mods
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(mods[0], vm.ItemExplicits[0].Mod);
            Assert.AreEqual(mods[1], vm.ItemExplicits[1].Mod);
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithWeaponStatsIfInputIsAWeapon()
        {
            var item = new Weapon();
            var vm = ItemViewModel.CreateViewModel(item);
            
            Assert.AreEqual("Total DPS", vm.ItemStats[0].Name);
            Assert.AreEqual("PDPS", vm.ItemStats[1].Name);
            Assert.AreEqual("EDPS", vm.ItemStats[2].Name);
            Assert.AreEqual("APS", vm.ItemStats[3].Name);
        }

        [TestMethod]
        public void WeaponStatsTotalDPSEqualsTheNormalizedTotalDPS()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2f,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 10,
                    TopEnd = 100
                }
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetTotalDPS(true), vm.ItemStats[0].Value);
        }

        [TestMethod]
        public void WeaponStatsTotalDPSEqualsNotNormlizedTotalDPSIfCorrupted()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2f,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 10,
                    TopEnd = 100
                },
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetTotalDPS(false), vm.ItemStats[0].Value);
        }
    }
}
