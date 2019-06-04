using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.ViewModels;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class ItemViewModelTest
    {
        private class MockPoEItem : PoEItem
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
    }
}
