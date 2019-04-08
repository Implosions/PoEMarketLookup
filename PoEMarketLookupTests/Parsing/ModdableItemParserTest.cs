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
        public class ModdableItem : IPoEItem
        {
            public Rarity Rarity { get; }
            public string Base { get; }
            public string Name { get; }
            public int Quality { get; }
            public int LevelRequirement { get; }
            public int StrengthRequirement { get; }
            public int DexterityRequirement { get; }
            public int IntelligenceRequirement { get; }
            public SocketGroup Sockets { get; }
            public int ItemLevel { get; }
            public Mod[] ImplicitMods { get; }
            public Mod[] ExplicitMods { get; }
            public bool Corrupted { get; }
            public bool Shaper { get; }
            public bool Elder { get; }
            public bool Synthesised { get; }
            public bool Mirrored { get; }
            public Mod Enchantment { get; }

            public ModdableItem(PoEItemBuilder builder)
            {
                Rarity = builder.Rarity;
                Base = builder.Base;
                Name = builder.Name;
                Quality = builder.Quality;
                LevelRequirement = builder.LevelRequirement;
                StrengthRequirement = builder.StrengthRequirement;
                DexterityRequirement = builder.DexterityRequirement;
                IntelligenceRequirement = builder.IntelligenceRequirement;
                Sockets = builder.Sockets;
                ItemLevel = builder.ItemLevel;
                ImplicitMods = builder.ImplicitMods;
                ExplicitMods = builder.ExplicitMods;
                Corrupted = builder.Corrupted;
                Shaper = builder.Shaper;
                Elder = builder.Elder;
                Synthesised = builder.Synthesised;
                Mirrored = builder.Mirrored;
                Enchantment = builder.Enchantment;
            }
        }

        public class ModdableItemBuilder : PoEItemBuilder
        {
            public override IPoEItem Build()
            {
                return new ModdableItem(this);
            }
        }

        public class MockModdableItemParser : ModdableItemParser
        {
            public MockModdableItemParser(string rawItemText) : base(rawItemText)
            {
                itemBuilder = new ModdableItemBuilder();
            }

            public override IPoEItem Parse()
            {
                ParseModdableItemSections();

                return itemBuilder.Build();
            }
        }
        #endregion

        [TestMethod]
        public void CanParseItemQuality()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_QUAL_EV);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(20, item.Quality);
        }

        [TestMethod]
        public void CanParseItemLevelRequirement()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_AR);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(11, item.LevelRequirement);
        }

        [TestMethod]
        public void CanParseItemAttributeRequirements()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(1, item.StrengthRequirement);
            Assert.AreEqual(2, item.DexterityRequirement);
            Assert.AreEqual(3, item.IntelligenceRequirement);
        }

        [TestMethod]
        public void CanParseItemSockets()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_ES);
            var item = (ModdableItem)p.Parse();
            var sg = item.Sockets;

            Assert.AreEqual(6, sg.Sockets);
            Assert.AreEqual(6, sg.LargestLink);
        }

        [TestMethod]
        public void CanParseItemLevel()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_AR);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(33, item.ItemLevel);
        }

        [TestMethod]
        public void CanParseImplicitMods()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_ES_WITH_IMPLICIT);
            var item = (ModdableItem)p.Parse();
            var mod = item.ImplicitMods[0];

            Assert.AreEqual("#% increased Spell Damage", mod.Affix);
            Assert.AreEqual(13, mod.AffixValues[0]);
        }

        [TestMethod]
        public void CanParseRareItemExplicitMods()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var item = (ModdableItem)p.Parse();

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
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(true, item.Corrupted);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseShaperItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_SHAPER);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(true, item.Shaper);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseElderItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_ELDER);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(true, item.Elder);
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseSynthesisedItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BODY_SYNTHESISED);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(true, item.Synthesised);
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseMirroredItems()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BOOTS_MIRRORED);
            var item = (ModdableItem)p.Parse();

            Assert.AreEqual(true, item.Mirrored);
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseItemsWithNote()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.SHIELD_WITH_NOTE);
            var item = (ModdableItem)p.Parse();
            
            Assert.AreEqual(1, item.ImplicitMods.Length);
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseEnchantments()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.BOOTS_ENCHANTED);
            var item = (ModdableItem)p.Parse();
            var enchant = "Adds 1 to 56 Lightning Damage if you haven't Killed Recently";

            Assert.AreEqual(enchant, item.Enchantment.ToString());
            Assert.AreEqual("+8 to Dexterity", item.ImplicitMods[0].ToString());
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseUniques()
        {
            var p = new MockModdableItemParser(PoEItemData.Armor.GLOVES_STORMS_GIFT);
            var item = (ModdableItem)p.Parse();
            
            Assert.AreEqual("+8% to Fire Resistance", item.ImplicitMods[0].ToString());
            Assert.AreEqual(5, item.ExplicitMods.Length);
        }
    }
}
