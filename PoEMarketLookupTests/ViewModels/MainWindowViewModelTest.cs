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
            public SearchResultsViewModel SearchedResults { get; set; }
            public bool SearchFailure { get; set; }
            public bool SearchCannotConnect { get; set; }

            protected override string GetClipboard()
            {
                return Clipboard;
            }

#pragma warning disable CS1998
            protected async override Task<SearchResultsViewModel> RequestItemSearch(string league, ItemViewModel vm)
#pragma warning restore CS1998
            {
                if (SearchFailure)
                {
                    return null;
                }

                if (SearchCannotConnect)
                {
                    throw new Exception();
                }

                SearchedLeague = league;
                SearchedVM = vm;
                SearchedResults = new SearchResultsViewModel();

                return SearchedResults;
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
                ItemVM = new ItemViewModel()
            };
        }

        [TestMethod]
        public void SettingItemViewModelFiresPropertyChangedEvent()
        {
            var propertyChanged = false;
            var vm = new MockViewModel();
            vm.PropertyChanged += delegate { propertyChanged = true; };
            vm.ItemVM = null;

            Assert.IsTrue(propertyChanged);
        }

        [TestMethod]
        public async Task ExecutingPasteButtonCommandCreatesNewItemViewModelUsingItemTextFromTheClipboard()
        {
            var vm = new MockViewModel()
            {
                Clipboard = PoEItemData.Weapon.DEBEONS_DIRGE
            };
            await vm.PasteFromClipboardCommand.ExecuteAsync();

            Assert.IsNotNull(vm.ItemVM);
        }

        [TestMethod]
        public async Task ItemViewModelIsSetToErrorViewModelIfNewViewModelCreationFails()
        {
            var vm = new MockViewModel()
            {
                Clipboard = string.Empty
            };
            await vm.PasteFromClipboardCommand.ExecuteAsync();

            Assert.IsTrue(vm.ItemVM is ErrorViewModel);
        }

        [TestMethod]
        public async Task SearchCommandUsesSelectedLeagueForSearch()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual("Hardcore", _mockVM.SearchedLeague);
        }

        [TestMethod]
        public async Task SearchCommandUsesItemViewModelForSearch()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual(_mockVM.ItemVM, _mockVM.SearchedVM);
        }

        [TestMethod]
        public async Task SearchCommandReplacesSearchResultViewModelWithNewResult()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual(_mockVM.ResultsViewModel, _mockVM.SearchedResults);
        }

        [TestMethod]
        public void SettingResultsViewModelFiresPropertyChangedEvent()
        {
            var propertyChanged = false;
            var vm = new MockViewModel();
            vm.PropertyChanged += delegate { propertyChanged = true; };
            vm.ResultsViewModel = null;

            Assert.IsTrue(propertyChanged);
        }

        [TestMethod]
        public async Task ItemViewModelHasErrorMessageOnFormatException()
        {
            var vm = new MockViewModel()
            {
                Clipboard = string.Empty
            };
            await vm.PasteFromClipboardCommand.ExecuteAsync();
            var error = (ErrorViewModel)vm.ItemVM;

            Assert.AreEqual("Item data is not in the correct format", error.ErrorMessage);
        }

        [TestMethod]
        public async Task SearchResultsIsSetToErrorViewModelIfRequestFails()
        {
            var vm = new MockViewModel()
            {
                SearchFailure = true
            };
            await vm.SearchCommand.ExecuteAsync();

            Assert.IsTrue(vm.ResultsViewModel is ErrorViewModel);
        }

        [TestMethod]
        public async Task SearchResultsHasErrorMessageOnFailure()
        {
            var vm = new MockViewModel()
            {
                SearchFailure = true
            };
            await vm.SearchCommand.ExecuteAsync();
            var error = (ErrorViewModel)vm.ResultsViewModel;

            Assert.AreEqual("Problem requesting search results", error.ErrorMessage);
        }

        [TestMethod]
        public async Task SearchResultsIsSetToErrorViewModelIfClientThrowsException()
        {
            var vm = new MockViewModel()
            {
                SearchCannotConnect = true
            };
            await vm.SearchCommand.ExecuteAsync();

            Assert.IsTrue(vm.ResultsViewModel is ErrorViewModel);
        }

        [TestMethod]
        public async Task ErrorMessageIsSetIfClientThrowsException()
        {
            var vm = new MockViewModel()
            {
                SearchCannotConnect = true
            };
            await vm.SearchCommand.ExecuteAsync();
            var error = (ErrorViewModel)vm.ResultsViewModel;

            Assert.AreEqual("Could not connect to pathofexile.com", error.ErrorMessage);
        }

        [TestMethod]
        public void CanSearchReturnsTrueIfItemVMIsNotNull()
        {
            var vm = new MockViewModel()
            {
                ItemVM = new ItemViewModel()
            };

            Assert.IsTrue(vm.CanSearch);
        }

        [TestMethod]
        public void CanSearchReturnsFalseIfItemVMIsErrorViewModel()
        {
            var vm = new MockViewModel()
            {
                ItemVM = new ErrorViewModel(null)
            };

            Assert.IsFalse(vm.CanSearch);
        }

        [TestMethod]
        public async Task PasteButtonCommandInvokesSearchCommandCanExecuteChangedEvent()
        {
            bool changed = false;
            var vm = new MockViewModel();
            vm.SearchCommand.CanExecuteChanged += delegate { changed = true; };
            await vm.PasteFromClipboardCommand.ExecuteAsync();

            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void SearchCommandUsesCanSearchPropertyToDetermineIfItCanExecute()
        {
            var vm = new MockViewModel();

            Assert.IsFalse(vm.SearchCommand.CanExecute(null));

            vm.ItemVM = new ItemViewModel();

            Assert.IsTrue(vm.SearchCommand.CanExecute(null));
        }

        [TestMethod]
        public async Task ItemViewModelHasErrorViewModelOnAllExceptions()
        {
            var vm = new MockViewModel()
            {
                Clipboard = PoEItemData.Accessories.AMULET_ALL_ATT + PoEItemData.Armor.BODY_SYNTHESISED,
            };
            await vm.PasteFromClipboardCommand.ExecuteAsync();

            Assert.IsTrue(vm.ItemVM is ErrorViewModel);
        }

        [TestMethod]
        public async Task ItemViewModelHasErrorMessageOnAllExceptions()
        {
            var vm = new MockViewModel()
            {
                Clipboard = PoEItemData.Accessories.AMULET_ALL_ATT + PoEItemData.Armor.BODY_SYNTHESISED,
            };
            await vm.PasteFromClipboardCommand.ExecuteAsync();
            var error = (ErrorViewModel)vm.ItemVM;

            Assert.AreEqual("An error occured while parsing the item", error.ErrorMessage);
        }
    }
}
