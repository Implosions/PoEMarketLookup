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
    }
}
