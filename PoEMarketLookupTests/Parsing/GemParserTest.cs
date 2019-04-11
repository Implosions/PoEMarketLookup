using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class GemParserTest
    {
        [TestMethod]
        public void CanParseGemBase()
        {
            var p = new GemParser(PoEItemData.Gem.DIVINE_IRE);
            var item = p.Parse();

            Assert.AreEqual("Divine Ire", item.Base);
        }

        [TestMethod]
        public void CanParseGemLevel()
        {
            var p = new GemParser(PoEItemData.Gem.DIVINE_IRE);
            var item = p.Parse();

            Assert.AreEqual(1, item.Level);
        }

        [TestMethod]
        public void CanParseGemQuality()
        {
            var p = new GemParser(PoEItemData.Gem.DIVINE_IRE);
            var item = p.Parse();

            Assert.AreEqual(11, item.Quality);
        }

        [TestMethod]
        public void CanParseGemExperience()
        {
            var p = new GemParser(PoEItemData.Gem.DIVINE_IRE);
            var item = p.Parse();

            Assert.AreEqual(1, item.Experience);
        }
    }
}
