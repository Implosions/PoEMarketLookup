using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;

namespace PoEMarketLookupTests.Items
{
    [TestClass]
    public class ArmorTest
    {
        [TestMethod]
        public void GetNormalizedArmourValueReturnsQualityNormalizedArmourValueAt20QualityFor0QualityItems()
        {
            var armor = new Armor()
            {
                Armour = 100
            };

            Assert.AreEqual(120, armor.GetNormalizedArmourValue());
        }

        [TestMethod]
        public void GetNormalizedEvasionValueReturnsQualityNormalizedEvasionValueAt20QualityFor0QualityItems()
        {
            var armor = new Armor()
            {
                EvasionRating = 100
            };

            Assert.AreEqual(120, armor.GetNormalizedEvasionValue());
        }

        [TestMethod]
        public void GetNormalizedEnergyShieldValueReturnsQualityNormalizedEnergyShieldValueAt20QualityFor0QualityItems()
        {
            var armor = new Armor()
            {
                EnergyShield = 100
            };

            Assert.AreEqual(120, armor.GetNormalizedEnergyShieldValue());
        }

        [TestMethod]
        public void GetNormalizedArmourValueReturnsQualityNormalizedArmourValueAt20QualityFor10QualityItems()
        {
            var armor = new Armor()
            {
                Quality = 10,
                Armour = 110
            };

            Assert.AreEqual(120, armor.GetNormalizedArmourValue());
        }

        [TestMethod]
        public void GetNormalizedEvasionValueReturnsQualityNormalizedEvasionValueAt20QualityFor10QualityItems()
        {
            var armor = new Armor()
            {
                Quality = 10,
                EvasionRating = 110
            };

            Assert.AreEqual(120, armor.GetNormalizedEvasionValue());
        }

        [TestMethod]
        public void GetNormalizedEnergyShieldValueReturnsQualityNormalizedEnergyShieldValueAt20QualityFor10QualityItems()
        {
            var armor = new Armor()
            {
                Quality = 10,
                EnergyShield = 110
            };

            Assert.AreEqual(120, armor.GetNormalizedEnergyShieldValue());
        }
    }
}
