using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class AccessoryParserTest
    {
        [TestMethod]
        public void CanParseAmulets()
        {
            var p = new AccessoryParser(PoEItemData.Accessories.AMULET_RARE);
            var item = p.Parse();

            Assert.AreEqual("+30 to Strength", item.ImplicitMods[0].ToString());
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseRings()
        {
            var p = new AccessoryParser(PoEItemData.Accessories.RING_RARE);
            var item = p.Parse();

            Assert.AreEqual("+20% to Fire Resistance", item.ImplicitMods[0].ToString());
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseBelts()
        {
            var p = new AccessoryParser(PoEItemData.Accessories.BELT_RARE);
            var item = p.Parse();

            Assert.AreEqual("+19 to maximum Energy Shield", item.ImplicitMods[0].ToString());
            Assert.AreEqual(4, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseTalismans()
        {
            var p = new AccessoryParser(PoEItemData.Accessories.AMULET_TALISMAN);
            var item = p.Parse();

            Assert.AreEqual(1, item.TalismanTier);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseJewels()
        {
            var p = new AccessoryParser(PoEItemData.Accessories.JEWEL_RARE);
            var item = p.Parse();
            var mods = new string[]
            {
                "14% increased Totem Damage",
                "4% increased Attack Speed while holding a Shield",
                "+8% to Chaos Resistance",
                "+12% to Fire and Cold Resistances"
            };

            Assert.AreEqual(4, item.ExplicitMods.Length);
            Assert.AreEqual(mods[0], item.ExplicitMods[0].ToString());
            Assert.AreEqual(mods[1], item.ExplicitMods[1].ToString());
            Assert.AreEqual(mods[2], item.ExplicitMods[2].ToString());
            Assert.AreEqual(mods[3], item.ExplicitMods[3].ToString());
        }
    }
}
