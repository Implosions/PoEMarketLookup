using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class EnchantableItemParserTest
    {
        private class MockItem : EnchantableItem
        {
        }

        private class MockParser : EnchantableItemParser<MockItem>
        {
            public MockParser(string rawItem) : base(rawItem)
            {
            }
        }

        [TestMethod]
        public void CanParseSingleEnchant()
        {
            var p = new MockParser(PoEItemData.Armor.HELMET_ENCHANTED);
            var item = p.Parse();
            var enchant = "25% increased Cleave Damage";

            Assert.AreEqual(enchant, item.Enchantments[0].ToString());
        }

        [TestMethod]
        public void CanParseDoubleEnchant()
        {
            var p = new MockParser(PoEItemData.Armor.HELMET_DOUBLE_ENCHANTED);
            var item = p.Parse();

            Assert.AreEqual("1% increased Cleave Damage", item.Enchantments[0].ToString());
            Assert.AreEqual("2% increased Cleave Damage", item.Enchantments[1].ToString());
        }
    }
}
