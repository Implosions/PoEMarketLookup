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
            var a = ap.Parse();

            Assert.AreEqual(39, a.Armour);
        }

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithCorrectEvasionValue()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_EV);
            var a = ap.Parse();

            Assert.AreEqual(324, a.EvasionRating);
        }

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithCorrectESValue()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_ES);
            var a = ap.Parse();

            Assert.AreEqual(104, a.EnergyShield);
        }

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithMultipleArmorValues()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var a = ap.Parse();

            Assert.AreEqual(500, a.Armour);
            Assert.AreEqual(450, a.EvasionRating);
            Assert.AreEqual(400, a.EnergyShield);
        }

        [TestMethod]
        public void CanParseBootEnchantments()
        {
            var p = new ArmorParser(PoEItemData.Armor.BOOTS_ENCHANTED);
            var item = p.Parse();
            var enchant = "Adds 1 to 56 Lightning Damage if you haven't Killed Recently";

            Assert.AreEqual(enchant, item.Enchantment.ToString());
        }

        [TestMethod]
        public void CanParseHelmetEnchantments()
        {
            var p = new ArmorParser(PoEItemData.Armor.HELMET_ENCHANTED);
            var item = p.Parse();
            var enchant = "25% increased Cleave Damage";

            Assert.AreEqual(enchant, item.Enchantment.ToString());
        }

        [TestMethod]
        public void CanParseGloveEnchantments()
        {
            var p = new ArmorParser(PoEItemData.Armor.GLOVES_ENCHANTED);
            var item = p.Parse();
            var enchant = "Trigger Word of Ire when Hit";

            Assert.AreEqual(enchant, item.Enchantment.ToString());
        }
    }
}
