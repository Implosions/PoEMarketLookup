using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class PoEItemParserFactoryTest
    {
        [TestMethod]
        public void FactoryReturnsCurrencyParserWithCurrencyInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Currency.EXALTED_ORB);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(CurrencyParser));
        }

        [TestMethod]
        public void FactoryReturnsGemParserWithGemInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Gem.DIVINE_IRE);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(GemParser));
        }

        [TestMethod]
        public void FactoryReturnsWeaponParserWithWeaponInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Weapon.SWORD_REBUKE_OF_THE_VAAL);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(WeaponParser));
        }

        [TestMethod]
        public void FactoryReturnsAccessoryParserWithAccessoryInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Accessories.AMULET_RARE);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(AccessoryParser));
        }
    }
}
