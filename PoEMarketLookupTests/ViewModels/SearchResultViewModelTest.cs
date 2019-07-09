using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PoEMarketLookup.ViewModels;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class SearchResultViewModelTest
    {
        private string _testJsonResult;

        public SearchResultViewModelTest()
        {
            _testJsonResult = new JObject()
            {
                new JProperty("id", "foo"),
                new JProperty("total", 100)
            }.ToString();
        }

        [TestMethod]
        public void CreateViewModelSetsId()
        {
            var vm = SearchResultsViewModel.CreateViewModel(_testJsonResult, null);

            Assert.AreEqual("foo", vm.Id);
        }

        [TestMethod]
        public void CreateViewModelSetsTotal()
        {
            var vm = SearchResultsViewModel.CreateViewModel(_testJsonResult, null);

            Assert.AreEqual(100, vm.Total);
        }

        [TestMethod]
        public void CreateViewModelSetsLeague()
        {
            var vm = SearchResultsViewModel.CreateViewModel(_testJsonResult, "foo");

            Assert.AreEqual("foo", vm.League);
        }

        [TestMethod]
        public void SearchURLReturnsCorrectSearchURL()
        {
            string url = @"http://www.pathofexile.com/trade/search/Foo/bar";

            var vm = new SearchResultsViewModel()
            {
                League = "Foo",
                Id = "bar"
            };

            Assert.AreEqual(url, vm.SearchURL);
        }
    }
}
