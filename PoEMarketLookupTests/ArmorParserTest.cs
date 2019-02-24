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
    }
}
