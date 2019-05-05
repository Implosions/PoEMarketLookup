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

        [TestMethod]
        public void CanParseModdableItemSections()
        {
            var p = new MapParser(PoEItemData.Map.MAP_MAGIC);
            var item = p.Parse();

            Assert.AreEqual(63, item.ItemLevel);
            Assert.AreEqual("Monsters fire 2 additional Projectiles", item.ExplicitMods[0].ToString());
            Assert.AreEqual("Monsters gain an Endurance Charge on Hit", item.ExplicitMods[1].ToString());
        }
    }
}
