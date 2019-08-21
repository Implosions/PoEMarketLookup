using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class QualitableItemParserTest
    {
        private class MockItem : QualitableItem
        {
        }

        private class MockParser : QualitableItemParser<MockItem>
        {
            public MockParser(string rawItem) : base(rawItem)
            {
            }
        }

        [TestMethod]
        public void CanParseQuality()
        {
            var p = new MockParser(PoEItemData.Gem.DIVINE_IRE);
            var item = p.Parse();

            Assert.AreEqual(11, item.Quality);
        }
    }
}
