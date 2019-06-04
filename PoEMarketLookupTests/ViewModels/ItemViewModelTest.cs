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
    }
}
