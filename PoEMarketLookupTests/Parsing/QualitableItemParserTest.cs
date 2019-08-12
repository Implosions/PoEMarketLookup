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

        private class MockParser : QualitableItemParser<QualitableItem>
        {
            public MockParser(string rawItem) : base(rawItem)
            {
                item = new MockItem();
            }

            public override QualitableItem Parse()
            {
                ParseItemQuality();

                return item;
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
