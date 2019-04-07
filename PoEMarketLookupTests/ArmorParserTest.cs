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

        [TestMethod]
        public void ArmorParserParseReturnsArmorObjWithMultipleArmorValues()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(500, a.Armour);
            Assert.AreEqual(450, a.EvasionRating);
            Assert.AreEqual(400, a.EnergyShield);
        }

        [TestMethod]
        public void ArmorParserParseCanParseArmorQuality()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_QUAL_EV);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(20, a.Quality);
            Assert.AreEqual(646, a.EvasionRating);
        }

        [TestMethod]
        public void ArmorParserParseCanParseItemLevelRequirement()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(11, a.LevelRequirement);
        }

        [TestMethod]
        public void ArmorParserParseCanParseItemAttributeRequirements()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_AR_EV_ES);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(1, a.StrengthRequirement);
            Assert.AreEqual(2, a.DexterityRequirement);
            Assert.AreEqual(3, a.IntelligenceRequirement);
        }

        [TestMethod]
        public void ArmorParserCanParseItemSockets()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_ES);
            var a = (Armor)ap.Parse();
            var sg = a.Sockets;
            
            Assert.AreEqual(6, sg.Sockets);
            Assert.AreEqual(6, sg.LargestLink);
        }

        [TestMethod]
        public void ArmorParserCanParseItemLevel()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = (Armor)ap.Parse();
            
            Assert.AreEqual(33, a.ItemLevel);
        }

        [TestMethod]
        public void ArmorParserCanParseItemRarity()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_AR);
            var a = ap.Parse();

            Assert.AreEqual(Rarity.Normal, a.Rarity);
        }

        [TestMethod]
        public void ArmorParserCanParseImplicitMods()
        {
            var ap = new ArmorParser(PoEItemData.Armor.SHIELD_ES_WITH_IMPLICIT);
            var a = (Armor)ap.Parse();
            var mod = a.ImplicitMods[0];

            Assert.AreEqual("#% increased Spell Damage", mod.Affix);
            Assert.AreEqual(13, mod.AffixValues[0]);
        }

        [TestMethod]
        public void ArmorParserCanParseRareItemName()
        {
            var ap = new ArmorParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var item = (Armor)ap.Parse();

            Assert.AreEqual("Fossilised Spirit Shield", item.Base);
            Assert.AreEqual("Carrion Duty", item.Name);
        }

        [TestMethod]
        public void ArmorParserCanParseRareItemExplicitMods()
        {
            var ap = new ArmorParser(PoEItemData.Armor.SHIELD_ES_RARE);
            var a = (Armor)ap.Parse();

            string[] mods = new string[] { "+25 to maximum Energy Shield",
                                           "+104 to maximum Life",
                                           "+7% to all Elemental Resistances",
                                           "+26% to Cold Resistance",
                                           "+20% to Lightning Resistance" };

            Assert.AreEqual("15% increased Spell Damage", a.ImplicitMods[0].ToString());
            Assert.AreEqual(mods[0], a.ExplicitMods[0].ToString());
            Assert.AreEqual(mods[1], a.ExplicitMods[1].ToString());
            Assert.AreEqual(mods[2], a.ExplicitMods[2].ToString());
            Assert.AreEqual(mods[3], a.ExplicitMods[3].ToString());
            Assert.AreEqual(mods[4], a.ExplicitMods[4].ToString());
        }

        [TestMethod]
        public void ArmorParserCanParseAmulets()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.AMULET_RARE);
            var a = (Armor)ap.Parse();

            Assert.AreEqual("+30 to Strength", a.ImplicitMods[0].ToString());
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseRings()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.RING_RARE);
            var a = (Armor)ap.Parse();

            Assert.AreEqual("+20% to Fire Resistance", a.ImplicitMods[0].ToString());
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseBelts()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.BELT_RARE);
            var a = (Armor)ap.Parse();

            Assert.AreEqual("+19 to maximum Energy Shield", a.ImplicitMods[0].ToString());
            Assert.AreEqual(4, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseCorruptedItems()
        {
            var ap = new ArmorParser(PoEItemData.Armor.GLOVES_CORRUPTED);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(true, a.Corrupted);
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseTalismans()
        {
            var ap = new ArmorParser(PoEItemData.Accessories.AMULET_TALISMAN);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(1, a.TalismanTier);
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseShaperItems()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_SHAPER);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(true, a.Shaper);
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseElderItems()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_ELDER);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(true, a.Elder);
            Assert.AreEqual(6, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseSynthesisedItems()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BODY_SYNTHESISED);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(true, a.Synthesised);
            Assert.AreEqual(5, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseMirroredItems()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BOOTS_MIRRORED);
            var a = (Armor)ap.Parse();

            Assert.AreEqual(true, a.Mirrored);
            Assert.AreEqual(5, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseItemsWithNote()
        {
            var ap = new ArmorParser(PoEItemData.Armor.SHIELD_WITH_NOTE);
            var a = (Armor)ap.Parse();
            
            Assert.AreEqual(1, a.ImplicitMods.Length);
            Assert.AreEqual(5, a.ExplicitMods.Length);
        }

        [TestMethod]
        public void ArmorParserCanParseEnchantments()
        {
            var ap = new ArmorParser(PoEItemData.Armor.BOOTS_ENCHANTED);
            var a = (Armor)ap.Parse();
            var enchant = "Adds 1 to 56 Lightning Damage if you haven't Killed Recently";

            Assert.AreEqual(enchant, a.Enchantment.ToString());
            Assert.AreEqual("+8 to Dexterity", a.ImplicitMods[0].ToString());
            Assert.AreEqual(5, a.ExplicitMods.Length);
        }
    }
}
