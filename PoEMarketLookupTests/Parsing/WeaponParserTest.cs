using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class WeaponParserTest
    {
        [TestMethod]
        public void CanParseWeaponItemInfo()
        {
            var p = new WeaponParser(PoEItemData.Weapon.SWORD_REBUKE_OF_THE_VAAL);
            var item = (Weapon)p.Parse();

            Assert.AreEqual(Rarity.Unique, item.Rarity);
            Assert.AreEqual("Vaal Blade", item.Base);
            Assert.AreEqual("Rebuke of the Vaal", item.Name);
        }

        [TestMethod]
        public void CanParseItemMods()
        {
            var p = new WeaponParser(PoEItemData.Weapon.SWORD_REBUKE_OF_THE_VAAL);
            var item = (Weapon)p.Parse();

            Assert.AreEqual(0, item.Quality);
            Assert.AreEqual(64, item.LevelRequirement);
            Assert.AreEqual(113, item.StrengthRequirement);
            Assert.AreEqual(113, item.DexterityRequirement);
            Assert.AreEqual(3, item.Sockets.Sockets);
            Assert.AreEqual(75, item.ItemLevel);
            Assert.AreEqual("+460 to Accuracy Rating", item.ImplicitMods[0].ToString());
            Assert.AreEqual(6, item.ExplicitMods.Length);
        }

        [TestMethod]
        public void CanParseWeaponType()
        {
            var p = new WeaponParser(PoEItemData.Weapon.SWORD_REBUKE_OF_THE_VAAL);
            var item = (Weapon)p.Parse();

            Assert.AreEqual("One Handed Sword", item.Type);
        }
    }
}
