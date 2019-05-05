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

        [TestMethod]
        public void CanParseItemModdableItemSections()
        {
            var p = new FlaskParser(PoEItemData.Flask.GRANITE_MAGIC);
            var item = p.Parse();

            Assert.AreEqual(27, item.LevelRequirement);
            Assert.AreEqual(80, item.ItemLevel);
            Assert.AreEqual(1, item.ExplicitMods.Length);
            Assert.AreEqual("Adds Knockback to Melee Attacks during Flask effect", item.ExplicitMods[0].ToString());
        }

        [TestMethod]
        public void CanParseFlaskMaxCharges()
        {
            var p = new FlaskParser(PoEItemData.Flask.GRANITE_MAGIC);
            var item = p.Parse();

            Assert.AreEqual(60, item.MaxCharges);
        }

        [TestMethod]
        public void CanParseFlaskChargesConsumedOnUse()
        {
            var p = new FlaskParser(PoEItemData.Flask.GRANITE_MAGIC);
            var item = p.Parse();

            Assert.AreEqual(30, item.ChargesConsumedOnUse);
        }

        [TestMethod]
        public void CanParseFlaskChargesInfoWithQualityFlask()
        {
            var p = new FlaskParser(PoEItemData.Flask.MANA_MAGIC_QUALITY);
            var item = p.Parse();

            Assert.AreEqual(12, item.ChargesConsumedOnUse);
            Assert.AreEqual(32, item.MaxCharges);
        }

        [TestMethod]
        public void CanParseFlaskChargesInfoWithChemistsFlask()
        {
            var p = new FlaskParser(PoEItemData.Flask.TOPAZ_MAGIC);
            var item = p.Parse();

            Assert.AreEqual(22, item.ChargesConsumedOnUse);
            Assert.AreEqual(60, item.MaxCharges);
        }
    }
}
