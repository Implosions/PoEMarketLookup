using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class ModdableItemParserTest
    {
        #region mocks
        public class MockModdableItem : ModdableItem
        {
        }

        public class MockModdableItemParser : ModdableItemParser<ModdableItem>
        {
            public MockModdableItemParser(string rawItemText) : base(rawItemText)
            {
                item = new MockModdableItem();
            }

            public override ModdableItem Parse()
            {
                ParseModdableItemSections();

                return item;
            }
        }
        #endregion

        [TestMethod]
        public void CanParseItemQuality()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_QUAL_EV);
            var item = p.Parse();

            Assert.AreEqual(20, item.Quality);
        }

        [TestMethod]
        public void CanParseItemLevelRequirement()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_AR);
            var item = p.Parse();

            Assert.AreEqual(11, item.LevelRequirement);
        }

        [TestMethod]
        public void CanParseItemAttributeRequirements()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var item = p.Parse();

            Assert.AreEqual(1, item.StrengthRequirement);
            Assert.AreEqual(2, item.DexterityRequirement);
            Assert.AreEqual(3, item.IntelligenceRequirement);
        }

        [TestMethod]
        public void CanParseItemSockets()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_ES);
            var item = p.Parse();
            var sg = item.Sockets;

            Assert.AreEqual(6, sg.Sockets);
            Assert.AreEqual(6, sg.LargestLink);
        }

        [TestMethod]
        public void CanParseItemLevel()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_AR);
            var item = p.Parse();

            Assert.AreEqual(33, item.ItemLevel);
        }

        [TestMethod]
        public void CanParseImplicitMods()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_ES_WITH_IMPLICIT);
            var item = p.Parse();
            var mod = item.ImplicitMods[0];

            Assert.AreEqual("#% increased Spell Damage", mod.Affix);
            Assert.AreEqual(13, mod.AffixValues[0]);
        }

        [TestMethod]
        public void CanParseRareItemExplicitMods()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var item = p.Parse();

            string[] explicits = new string[] 
            {
                "+25 to maximum Energy Shield",
                "+104 to maximum Life",
                "+7% to all Elemental Resistances",
                "+26% to Cold Resistance",
                "+20% to Lightning Resistance"
            };

            Assert.AreEqual("15% increased Spell Damage", item.ImplicitMods[0].ToString());
            Assert.AreEqual(explicits[0], item.ExplicitMods[0].ToString());
            Assert.AreEqual(explicits[1], item.ExplicitMods[1].ToString());
            Assert.AreEqual(explicits[2], item.ExplicitMods[2].ToString());
            Assert.AreEqual(explicits[3], item.ExplicitMods[3].ToString());
            Assert.AreEqual(explicits[4], item.ExplicitMods[4].ToString());
        }

        [TestMethod]
        public void CanParseCorruptedItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_CORRUPTED);
            var item = p.Parse();

            Assert.AreEqual(true, item.Corrupted);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseShaperItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_SHAPER);
            var item = p.Parse();

            Assert.AreEqual(true, item.Shaper);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseElderItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_ELDER);
            var item = p.Parse();

            Assert.AreEqual(true, item.Elder);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseSynthesisedItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_SYNTHESISED);
            var item = p.Parse();

            Assert.AreEqual(true, item.Synthesised);
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseMirroredItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BOOTS_MIRRORED);
            var item = p.Parse();

            Assert.AreEqual(true, item.Mirrored);
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseItemsWithNote()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_WITH_NOTE);
            var item = p.Parse();
            
            Assert.AreEqual(1, item.ImplicitMods.Length);
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }
        
        [TestMethod]
        public void CanParseUniques()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_STORMS_GIFT);
            var item = p.Parse();
            
            Assert.AreEqual("+8% to Fire Resistance", item.ImplicitMods[0].ToString());
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseNormalItemRarity()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_AR);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Normal, item.Rarity);
        }

        [TestMethod]
        public void CanParseMagicItemRarity()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BOOTS_MAGIC_UNID);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Magic, item.Rarity);
        }

        [TestMethod]
        public void CanParseRareItemRarity()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Rare, item.Rarity);
        }

        [TestMethod]
        public void CanParseUniqueItemRarity()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_STORMS_GIFT);
            var item = p.Parse();

            Assert.AreEqual(Rarity.Unique, item.Rarity);
        }

        [TestMethod]
        public void CanParseTotalLifeFromMaxLifeMod()
        {
            var p = new MockModdableItemParser(PoEItemData.Accessories.AMULET_LIFE);
            var item = p.Parse();

            Assert.AreEqual(100, item.TotalLife);
        }
    }
}
