using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.ViewModels;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class SearchResultViewModelTest
    {
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
