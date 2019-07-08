using System;
using System.Threading.Tasks;
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
            public string SearchedLeague { get; set; }
            public ItemViewModel SearchedVM { get; set; }

            protected override string GetClipboard()
            {
                return Clipboard;
            }

            protected override Task RequestItemSearch(string league, ItemViewModel vm)
            {
                SearchedLeague = league;
                SearchedVM = vm;

                return base.RequestItemSearch(league, vm);
            }
        }

        private MockViewModel _mockVM;

        [TestInitialize]
        public void SetMockVM()
        {
            _mockVM = new MockViewModel()
            {
                Leagues = new string[]
                {
                    "Standard",
                    "Hardcore"
                },
                SelectedLeagueIndex = 1,
                ItemViewModel = new ItemViewModel()
            };
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

        [TestMethod]
        public void SearchCommandUsesSelectedLeagueForSearch()
        {
            _mockVM.SearchCommand.Execute(null);

            Assert.AreEqual("Hardcore", _mockVM.SearchedLeague);
        }

        [TestMethod]
        public void SearchCommandUsesItemViewModelForSearch()
        {
            _mockVM.SearchCommand.Execute(null);

            Assert.AreEqual(_mockVM.ItemViewModel, _mockVM.SearchedVM);
        }
    }
}
