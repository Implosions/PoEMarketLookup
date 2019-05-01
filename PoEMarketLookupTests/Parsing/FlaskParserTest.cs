using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class FlaskParserTest
    {
        [TestMethod]
        public void CanParseItemInformation()
        {
            var p = new FlaskParser(PoEItemData.Flask.MANA);
            var item = p.Parse();

            Assert.AreEqual(PoEMarketLookup.PoE.Items.Components.Rarity.Normal, item.Rarity);
            Assert.AreEqual("Colossal Mana Flask", item.Base);
        }
    }
}
