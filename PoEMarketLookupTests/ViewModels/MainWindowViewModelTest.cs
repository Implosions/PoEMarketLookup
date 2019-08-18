using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PoEMarketLookup.ViewModels;
using PoEMarketLookup.Web;
using PoEMarketLookupTests.Parsing;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        private class MockWebClient : IWebClient
        {
            public bool SearchReturnNull { get; set; }
            public bool SearchThrowException { get; set; }
            private static readonly string _searchJson = CreateSearchJsonReturn();
            private static readonly string _fetchJson = CreateFetchJsonReturn();

#pragma warning disable CS1998
            public async Task<string> SearchAsync(string league, ItemViewModel vm, double lowerBound, double upperBound)
#pragma warning restore CS1998
            {
                if (SearchReturnNull)
                {
                    return null;
                }

                if (SearchThrowException)
                {
                    throw new Exception();
                }

                return _searchJson;
            }

#pragma warning disable CS1998
            public async Task<string> FetchListingsAsync(string[] hashes)
#pragma warning restore CS1998
            {
                return _fetchJson;
            }

            private static string CreateSearchJsonReturn()
            {
                var root = new JObject();

                root.CreateProperty("id")
                    .Value = "foobar";

                root.CreateProperty("total")
                    .Value = 3;

                root.CreateProperty("result")
                    .Value = new JArray()
                    {
                        "a",
                        "b",
                        "c"
                    };

                return root.ToString();
            }

            private static string CreateFetchJsonReturn()
            {
                var root = new JObject();

                root.CreateProperty("result")
                    .Value = new JArray()
                    {
                        CreateListing(1),
                        CreateListing(3)
                    };

                return root.ToString();
            }

            private static JObject CreateListing(int amount)
            {
                var price = new JObject();

                price.CreateProperty("amount")
                     .Value = amount;

                price.CreateProperty("currency")
                     .Value = "chaos";

                var root = new JObject();

                root.CreateProperty("info")
                    .CreateObject()
                    .CreateProperty("price")
                    .SetValue(price);

                return root;
            }
        }

        private class MockViewModel : MainWindowViewModel
        {
            public string Clipboard { get; set; }
            public bool SearchCannotConnect
            {
                set
                {
                    ((MockWebClient)WebClient).SearchThrowException = value;
                }
            }
            public bool SearchFailure
            {
                set
                {
                    ((MockWebClient)WebClient).SearchReturnNull = value;
                }
            }
            public bool SelectedLeagueSaved { get; set; }

            public MockViewModel()
            {
                WebClient = new MockWebClient();
            }

            protected override string GetClipboard()
            {
                return Clipboard;
            }

            protected override void SaveLeagueSelectionIndex()
            {
                SelectedLeagueSaved = true;
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

            Assert.AreEqual("Hardcore", 
                ((SearchResultsViewModel)_mockVM.ResultsViewModel).League);
        }

        [TestMethod]
        public async Task SearchCommandReplacesSearchResultViewModelWithNewResult()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.IsNotNull(_mockVM.ResultsViewModel);
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

        [TestMethod]
        public void SelectedLeagueIndexIsSavedWhenSet()
        {
            var vm = new MockViewModel()
            {
                SelectedLeagueIndex = 1
            };

            Assert.IsTrue(vm.SelectedLeagueSaved);
        }

        [TestMethod]
        public void SelectedLeagueIndexIsNotSavedIfNewValueIsTheSameAsTheOldValue()
        {
            var vm = new MockViewModel()
            {
                SelectedLeagueIndex = 0
            };

            Assert.IsFalse(vm.SelectedLeagueSaved);
        }

        [TestMethod]
        public async Task SearchCommandUsesParsedIdFromClientForResultsId()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual("foobar",
                ((SearchResultsViewModel)_mockVM.ResultsViewModel).Id);
        }

        [TestMethod]
        public async Task SearchCommandUsesParsedTotalFromClientForResultsTotal()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual(3,
                ((SearchResultsViewModel)_mockVM.ResultsViewModel).Total);
        }

        [TestMethod]
        public async Task SearchCommandSetsResultsMinPriceValue()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual("1 chaos",
                ((SearchResultsViewModel)_mockVM.ResultsViewModel).MinimumListingPrice);
        }

        [TestMethod]
        public async Task SearchCommandSetsResultsMaxPriceValue()
        {
            await _mockVM.SearchCommand.ExecuteAsync();

            Assert.AreEqual("3 chaos",
                ((SearchResultsViewModel)_mockVM.ResultsViewModel).MaximumListingPrice);
        }
    }
}
