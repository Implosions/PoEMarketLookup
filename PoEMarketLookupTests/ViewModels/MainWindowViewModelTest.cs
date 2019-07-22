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

            protected async override Task<SearchResultsViewModel> RequestItemSearch(string league, ItemViewModel vm)
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
        public void ExecutingPasteButtonCommandCreatesNewItemViewModelUsingItemTextFromTheClipboard()
        {
            var vm = new MockViewModel();
            vm.Clipboard = PoEItemData.Weapon.DEBEONS_DIRGE;
            vm.PasteFromClipboardCommand.Execute(null);

            Assert.IsNotNull(vm.ItemVM);
        }

        [TestMethod]
        public void ItemViewModelIsSetToErrorViewModelIfNewViewModelCreationFails()
        {
            var vm = new MockViewModel();
            vm.Clipboard = string.Empty;
            vm.PasteFromClipboardCommand.Execute(null);

            Assert.IsTrue(vm.ItemVM is ErrorViewModel);
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

            Assert.AreEqual(_mockVM.ItemVM, _mockVM.SearchedVM);
        }

        [TestMethod]
        public void SearchCommandReplacesSearchResultViewModelWithNewResult()
        {
            _mockVM.SearchCommand.Execute(null);

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
        public void ItemViewModelHasErrorMessageIfItemCreationFails()
        {
            var vm = new MockViewModel();
            vm.Clipboard = string.Empty;
            vm.PasteFromClipboardCommand.Execute(null);
            var error = (ErrorViewModel)vm.ItemVM;

            Assert.AreEqual("Item data is not in the correct format", error.ErrorMessage);
        }

        [TestMethod]
        public void SearchResultsIsSetToErrorViewModelIfRequestFails()
        {
            var vm = new MockViewModel()
            {
                SearchFailure = true
            };
            vm.SearchCommand.Execute(null);

            Assert.IsTrue(vm.ResultsViewModel is ErrorViewModel);
        }

        [TestMethod]
        public void SearchResultsHasErrorMessageOnFailure()
        {
            var vm = new MockViewModel()
            {
                SearchFailure = true
            };
            vm.SearchCommand.Execute(null);
            var error = (ErrorViewModel)vm.ResultsViewModel;

            Assert.AreEqual("Problem requesting search results", error.ErrorMessage);
        }

        [TestMethod]
        public void SearchResultsIsSetToErrorViewModelIfClientThrowsException()
        {
            var vm = new MockViewModel()
            {
                SearchCannotConnect = true
            };
            vm.SearchCommand.Execute(null);

            Assert.IsTrue(vm.ResultsViewModel is ErrorViewModel);
        }

        [TestMethod]
        public void ErrorMessageIsSetIfClientThrowsException()
        {
            var vm = new MockViewModel()
            {
                SearchCannotConnect = true
            };
            vm.SearchCommand.Execute(null);
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
        public void PasteButtonCommandInvokesSearchCommandCanExecuteChangedEvent()
        {
            bool changed = false;
            var vm = new MockViewModel();
            vm.SearchCommand.CanExecuteChanged += delegate { changed = true; };
            vm.PasteFromClipboardCommand.Execute(null);

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
    }
}
