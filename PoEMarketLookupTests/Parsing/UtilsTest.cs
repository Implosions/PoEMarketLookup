using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class UtilsTest
    {
        private string CreateTestWeapon(string weaponType)
        {
            return PoEItemData.Weapon.WEAPON_TEMPLATE.Replace("$", weaponType);
        }

        [TestMethod]
        public void FindItemTypeReturnsUnknownTypeIfItemIsUnrecognized()
        {
            var type = Utils.FindItemType(string.Empty);

            Assert.AreEqual(PoEItemType.Unknown, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsCurrencyTypeForCurrencyItem()
        {
            var type = Utils.FindItemType(PoEItemData.Currency.EXALTED_ORB);

            Assert.AreEqual(PoEItemType.Currency, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsGemTypeForGemItem()
        {
            var type = Utils.FindItemType(PoEItemData.Gem.DIVINE_IRE);

            Assert.AreEqual(PoEItemType.Gem, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsSword1HTypeFor1HSwords()
        {
            var type = Utils.FindItemType(PoEItemData.Weapon.SWORD_REBUKE_OF_THE_VAAL);

            Assert.AreEqual(PoEItemType.Sword1H, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsAxe1HTypeFor1HAxes()
        {
            var type = Utils.FindItemType(CreateTestWeapon("One Handed Axe"));

            Assert.AreEqual(PoEItemType.Axe1H, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsMace1HTypeFor1HMaces()
        {
            var type = Utils.FindItemType(CreateTestWeapon("One Handed Mace"));

            Assert.AreEqual(PoEItemType.Mace1H, type);
        }
    }
}
