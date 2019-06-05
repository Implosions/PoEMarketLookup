using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.ViewModels;
using PoEMarketLookupTests.Parsing;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        private class MockViewModel : MainWindowViewModel
        {
            public string Clipboard { get; set; }

            protected override string GetClipboard()
            {
                return Clipboard;
            }
        }

        [TestMethod]
        public void SettingItemViewModelFiresPropertyChangedEvent()
        {
            var propertyChanged = false;
            var vm = new MockViewModel();
            vm.PropertyChanged += delegate { propertyChanged = true; };
            vm.ItemViewModel = null;

            Assert.IsTrue(propertyChanged);
        }

        [TestMethod]
        public void ExecutingPasteButtonCommandCreatesNewItemViewModelUsingItemTextFromTheClipboard()
        {
            var vm = new MockViewModel();
            vm.Clipboard = PoEItemData.Weapon.DEBEONS_DIRGE;
            vm.PasteFromClipboardCommand.Execute(null);

            Assert.IsNotNull(vm.ItemViewModel);
        }

        [TestMethod]
        public void ItemViewModelIsSetToNullIfNewViewModelCreationFails()
        {
            var vm = new MockViewModel();
            vm.Clipboard = string.Empty;
            vm.PasteFromClipboardCommand.Execute(null);

            Assert.IsNull(vm.ItemViewModel);
        }
    }
}
