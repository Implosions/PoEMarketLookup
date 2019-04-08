using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class ArmorParserTest
    {
        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithCorrectArmourValue()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(39, a.Armour);
        }

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithCorrectEvasionValue()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_EV);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(324, a.EvasionRating);
        }

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithCorrectESValue()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_ES);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(104, a.EnergyShield);
        }

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithMultipleArmorValues()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(500, a.Armour);
            Assert.AreEqual(450, a.EvasionRating);
            Assert.AreEqual(400, a.EnergyShield);
        }

        [TestMethod]
        public void ArmorParserCanParseRareItemName()
        {
            var ap = new ArmorParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var item = (Armor)ap.Parse();

            Assert.AreEqual("Fossilised Spirit Shield", item.Base);
            Assert.AreEqual("Carrion Duty", item.Name);
        }

        [TestMethod]
        public void ArmorParserCanParseAmulets()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.AMULET_RARE);
            var a = (Armor)ap.Parse();

            Assert.AreEqual("+30 to Strength", a.ImplicitMods[0].ToString());
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseRings()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.RING_RARE);
            var a = (Armor)ap.Parse();

            Assert.AreEqual("+20% to Fire Resistance", a.ImplicitMods[0].ToString());
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseBelts()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.BELT_RARE);
            var a = (Armor)ap.Parse();

            Assert.AreEqual("+19 to maximum Energy Shield", a.ImplicitMods[0].ToString());
            Assert.AreEqual(4, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseTalismans()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.AMULET_TALISMAN);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(1, a.TalismanTier);
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }
    }
}
