using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
