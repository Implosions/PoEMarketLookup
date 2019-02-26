using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests
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
        public void ArmorParserParseCanParseArmorQuality()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_QUAL_EV);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(20, a.Quality);
            Assert.AreEqual(646, a.EvasionRating);
        }

        [TestMethod]
        public void ArmorParserParseCanParseItemLevelRequirement()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(11, a.LevelRequirement);
        }

        [TestMethod]
        public void ArmorParserParseCanParseItemAttributeRequirements()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(1, a.StrengthRequirement);
            Assert.AreEqual(2, a.DexterityRequirement);
            Assert.AreEqual(3, a.IntelligenceRequirement);
        }

        [TestMethod]
        public void ArmorParserCanParseItemSockets()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_ES);
            var a = (Armor)ap.Parse();
            var sg = a.Sockets;
            
            Assert.AreEqual(6, sg.Sockets);
            Assert.AreEqual(6, sg.LargestLink);
        }

        [TestMethod]
        public void ArmorParserCanParseItemLevel()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = (Armor)ap.Parse();
            
            Assert.AreEqual(33, a.ItemLevel);
        }

        [TestMethod]
        public void ArmorParserCanParseItemRarity()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = ap.Parse();

            Assert.AreEqual("Normal", a.Rarity);
        }
    }
}
