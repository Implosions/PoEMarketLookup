using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookupTests.Items
{
    [TestClass]
    public class WeaponTest
    {
        [TestMethod]
        public void GetPhysicalDPSReturnsWeaponPhysicalDPS()
        {
            var weapon = new Weapon
            {
                AttacksPerSecond = 1.5,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                }           
            };

            Assert.AreEqual(150, weapon.GetPhysicalDPS());
        }

        [TestMethod]
        public void GetElementalDPSReturnsWeaponTotalElementalDPS()
        {
            var weapon = new Weapon
            {
                AttacksPerSecond = 1.5,
                FireDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                },
                ColdDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                },
                LightningDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                },
            };

            Assert.AreEqual(450, weapon.GetElementalDPS());
        }

        [TestMethod]
        public void GetTotalDPSReturnsWeaponTotalDPS()
        {
            var weapon = new Weapon
            {
                AttacksPerSecond = 1.5,
                FireDamage = new DamageRange(),
                ColdDamage = new DamageRange(),
                LightningDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                },
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                },
                ChaosDamage = new DamageRange
                {
                    BottomEnd = 75,
                    TopEnd = 125
                },
            };

            Assert.AreEqual(450, weapon.GetTotalDPS());
        }

        [TestMethod]
        public void GetNormalizedPhysicalDamageReturnsPhysicalDamageRangeWithValuesNormalizedAt20QualityFrom0Quality()
        {
            var weapon = new Weapon()
            {
                Quality = 0,
                PhysicalDamage = new DamageRange()
                {
                    BottomEnd = 10,
                    TopEnd = 100
                }
            };

            var dr = weapon.GetNormalizedPhysicalDamage();

            Assert.AreEqual(12, dr.BottomEnd);
            Assert.AreEqual(120, dr.TopEnd);
        }

        [TestMethod]
        public void GetNormalizedPhysicalDamageReturnsPhysicalDamageRangeWithValuesNormalizedAt20QualityFrom10Quality()
        {
            var weapon = new Weapon()
            {
                Quality = 10,
                PhysicalDamage = new DamageRange()
                {
                    BottomEnd = 11,
                    TopEnd = 110
                }
            };

            var dr = weapon.GetNormalizedPhysicalDamage();

            Assert.AreEqual(12, dr.BottomEnd);
            Assert.AreEqual(120, dr.TopEnd);
        }

        [TestMethod]
        public void GetNormalizedPhysicalDamageReturnsUnchangedPhysicalDamageIfQualityIsGreaterThanOrEqualTo20()
        {
            var weapon = new Weapon()
            {
                Quality = 30,
                PhysicalDamage = new DamageRange()
                {
                    BottomEnd = 13,
                    TopEnd = 130
                }
            };

            var dr = weapon.GetNormalizedPhysicalDamage();

            Assert.AreEqual(13, dr.BottomEnd);
            Assert.AreEqual(130, dr.TopEnd);
        }

        [TestMethod]
        public void GetNormalizedPhysicalDamageReturnsCorrectDamageValuesIfitemHasAnIncreasedPhysicalDamageExplicitMod()
        {
            var weapon = new Weapon()
            {
                Quality = 0,
                PhysicalDamage = new DamageRange()
                {
                    BottomEnd = 20,
                    TopEnd = 200
                },
                ExplicitMods = new Mod[]
                {
                    Mod.Parse("100% Increased Physical Damage")
                }
            };

            var dr = weapon.GetNormalizedPhysicalDamage();

            Assert.AreEqual(22, dr.BottomEnd);
            Assert.AreEqual(220, dr.TopEnd);
        }

        [TestMethod]
        public void GetNormalizedPhysicalDamageReturnsCorrectDamageValuesIfitemHasAnIncreasedPhysicalDamageImplicitMod()
        {
            var weapon = new Weapon()
            {
                Quality = 0,
                PhysicalDamage = new DamageRange()
                {
                    BottomEnd = 20,
                    TopEnd = 200
                },
                ImplicitMods = new Mod[]
                {
                    Mod.Parse("100% Increased Physical Damage")
                }
            };

            var dr = weapon.GetNormalizedPhysicalDamage();

            Assert.AreEqual(22, dr.BottomEnd);
            Assert.AreEqual(220, dr.TopEnd);
        }
    }
}
