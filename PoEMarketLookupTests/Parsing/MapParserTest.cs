using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class MapParserTest
    {
        [TestMethod]
        public void CanParseItemInfo()
        {
            var p = new MapParser(PoEItemData.Map.MAP_NORMAL);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Normal, item.Rarity);
            Assert.AreEqual("Fungal Hollow Map", item.Base);
        }
    }
}
