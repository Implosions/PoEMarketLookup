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

        [TestMethod]
        public void FindItemTypeReturnsDaggerTypeForDaggers()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Dagger"));

            Assert.AreEqual(PoEItemType.Dagger, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsClawTypeForClaws()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Claw"));

            Assert.AreEqual(PoEItemType.Claw, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsSceptreTypeForSceptres()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Sceptre"));

            Assert.AreEqual(PoEItemType.Sceptre, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsWandTypeForWands()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Wand"));

            Assert.AreEqual(PoEItemType.Wand, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsSword2HTypeFor2HSwords()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Two Handed Sword"));

            Assert.AreEqual(PoEItemType.Sword2H, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsAxe2HTypeFor2HAxes()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Two Handed Axe"));

            Assert.AreEqual(PoEItemType.Axe2H, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsMace2HTypeFor2HMaces()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Two Handed Mace"));

            Assert.AreEqual(PoEItemType.Mace2H, type);
        }
    }
}
