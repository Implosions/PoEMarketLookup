using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

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

        [TestMethod]
        public void GetNormalizedArmourValueReturnsSameValueIfQualityIsGreaterThanOrEqualTo20()
        {
            var armor = new Armor()
            {
                Quality = 30,
                Armour = 100
            };

            Assert.AreEqual(100, armor.GetNormalizedArmourValue());
        }

        [TestMethod]
        public void GetNormalizedEvasionValueReturnsSameValueIfQualityIsGreaterThanOrEqualTo20()
        {
            var armor = new Armor()
            {
                Quality = 30,
                EvasionRating = 100
            };

            Assert.AreEqual(100, armor.GetNormalizedEvasionValue());
        }

        [TestMethod]
        public void GetNormalizedEnergyShieldValueReturnsSameValueIfQualityIsGreaterThanOrEqualTo20()
        {
            var armor = new Armor()
            {
                Quality = 30,
                EnergyShield = 100
            };

            Assert.AreEqual(100, armor.GetNormalizedEnergyShieldValue());
        }

        [TestMethod]
        public void GetNormalizedArmourValueReturnsCorrectArmourValueWithAnIncreasedArmourExplicitMod()
        {
            var armor = new Armor()
            {
                Armour = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Armour")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedArmourValue());
        }

        [TestMethod]
        public void GetNormalizedEvasionValueReturnsCorrectEvasionValueWithAnIncreasedEvasionExplicitMod()
        {
            var armor = new Armor()
            {
                EvasionRating = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Evasion Rating")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedEvasionValue());
        }

        [TestMethod]
        public void GetNormalizedEnergyShieldValueReturnsCorrectESValueWithAnIncreasedESExplicitMod()
        {
            var armor = new Armor()
            {
                EnergyShield = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Energy Shield")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedEnergyShieldValue());
        }

        [TestMethod]
        public void GetNormalizedDefensesOnlyAddStatIncreasesToThatSpecificDefenseType()
        {
            var armor = new Armor()
            {
                Armour = 110,
                EvasionRating = 110,
                EnergyShield = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Armour"),
                    Mod.Parse("10% Increased Evasion Rating"),
                    Mod.Parse("10% Increased Energy Shield")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedArmourValue());
            Assert.AreEqual(130, armor.GetNormalizedEvasionValue());
            Assert.AreEqual(130, armor.GetNormalizedEnergyShieldValue());
        }

        [TestMethod]
        public void GetNormalizedArmourAndEvasionValueReturnsCorrectArmourAndEvasionValuesWithAHybridExplicitMod()
        {
            var armor = new Armor()
            {
                Armour = 110,
                EvasionRating = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Armour and Evasion Rating")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedArmourValue());
            Assert.AreEqual(130, armor.GetNormalizedEvasionValue());
        }

        [TestMethod]
        public void GetNormalizedArmourAndEnergyShieldValueReturnsCorrectArmourAndEnergyShieldValuesWithAHybridExplicitMod()
        {
            var armor = new Armor()
            {
                Armour = 110,
                EnergyShield = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Armour and Energy Shield")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedArmourValue());
            Assert.AreEqual(130, armor.GetNormalizedEnergyShieldValue());
        }

        [TestMethod]
        public void GetNormalizedEvasionAndEnergyShieldValueReturnsCorrectEvasionAndEnergyShieldValuesWithAHybridExplicitMod()
        {
            var armor = new Armor()
            {
                EvasionRating = 110,
                EnergyShield = 110,
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("10% Increased Evasion Rating and Energy Shield")
                }
            };

            Assert.AreEqual(130, armor.GetNormalizedEvasionValue());
            Assert.AreEqual(130, armor.GetNormalizedEnergyShieldValue());
        }
    }
}
