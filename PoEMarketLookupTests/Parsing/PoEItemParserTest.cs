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
        public class Item : IPoEItem
        {
            public Rarity Rarity { get; }
            public string Base { get; }

            public Item(PoEItemBuilder builder)
            {
                Rarity = builder.Rarity;
                Base = builder.Base;
            }
        }

        public class ItemBuilder : PoEItemBuilder
        {
            public override IPoEItem Build()
            {
                return new Item(this);
            }
        }

        public class ItemParser : PoEItemParser
        {
            public ItemParser(string rawItemText) : base(rawItemText)
            {
                itemBuilder = new ItemBuilder();
            }

            public override IPoEItem Parse()
            {
                ParseInfoSection();

                return itemBuilder.Build();
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
        public void CanParseNormalItemRarity()
        {
            var p = new ItemParser(PoEItemData.Armor.GLOVES_AR);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Normal, item.Rarity);
        }

        [TestMethod]
        public void CanParseMagicItemRarity()
        {
            var p = new ItemParser(PoEItemData.Armor.BOOTS_MAGIC_UNID);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Magic, item.Rarity);
        }
    }
}
