using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class PoEItemParserTest
    {
        [TestMethod]
        public void CanParseBaseItem()
        {
            var p = new PoEItemParser<PoEItem>(PoEItemData.Currency.EXALTED_ORB);
            var item = p.Parse();

            Assert.AreEqual("Exalted Orb", item.Base);
        }
    }
}
