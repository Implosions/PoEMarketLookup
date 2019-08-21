using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
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

        [TestMethod]
        public void FactoryReturnsArmorParserWithArmorInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Armor.BODY_ES);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(ArmorParser));
        }

        [TestMethod]
        public void FactoryReturnsFlaskParserWithFlaskInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Flask.GRANITE_MAGIC);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(FlaskParser));
        }

        [TestMethod]
        public void FactoryReturnsMapParserWithMapInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Map.MAP_MAGIC);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(MapParser));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowFormatExceptionIfItemIsNotInTheCorrectFormat()
        {
            var f = new PoEItemParserFactory(string.Empty);
            f.GetParser();
        }

        [TestMethod]
        public void FactoryReturnsPoEItemParserWithFragmentInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Fragment.SAC_MIDNIGHT);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(PoEItemParser<PoEItem>));
        }

        [TestMethod]
        public void FactoryReturnsPoEItemParserWithProhpecyInput()
        {
            var f = new PoEItemParserFactory(PoEItemData.Prophecy.KINGS_PATH);
            var p = f.GetParser();

            Assert.IsInstanceOfType(p, typeof(PoEItemParser<PoEItem>));
        }
    }
}
