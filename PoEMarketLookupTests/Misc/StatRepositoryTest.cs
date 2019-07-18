using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE;

namespace PoEMarketLookupTests.Misc
{
    [TestClass]
    public class StatRepositoryTest
    {
        [TestMethod]
        public void GetStatIdTriesModAffixWithoutFirstCharacterIfTheIdIsNotFoundInitially()
        {
            var repository = StatRepository.GetRepository();
            var id = repository.GetStatId("+# to maximum Life");

            Assert.IsNotNull(id);
        }
    }
}
