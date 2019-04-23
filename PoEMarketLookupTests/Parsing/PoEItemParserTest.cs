using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class PoEItemParserTest
    {
        #region mocks
        public class Item : PoEItem
        {
        }

        public class ItemParser : PoEItemParser<Item>
        {
            public ItemParser(string rawItemText) : base(rawItemText)
            {
                item = new Item();
            }

            public override Item Parse()
            {
                ParseInfoSection();

                return item;
            }
        }
        #endregion

        [TestMethod]
        public void CanParseBaseItem()
        {
            var p = new ItemParser(PoEItemData.Currency.EXALTED_ORB);
            var item = p.Parse();

            Assert.AreEqual("Exalted Orb", item.Base);
        }

        [TestMethod]
        public void CanParseRareItemName()
        {
            var p = new ItemParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var item = p.Parse();

            Assert.AreEqual("Carrion Duty", item.Name);
        }

        [TestMethod]
        public void CanParseUniqueItemName()
        {
            var p = new ItemParser(PoEItemData.Armor.GLOVES_STORMS_GIFT);
            var item = p.Parse();

            Assert.AreEqual("Storm's Gift", item.Name);
        }
    }
}
