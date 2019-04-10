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
    }
}
