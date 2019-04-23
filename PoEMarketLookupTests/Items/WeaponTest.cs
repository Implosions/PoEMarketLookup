﻿using System;
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
    }
}