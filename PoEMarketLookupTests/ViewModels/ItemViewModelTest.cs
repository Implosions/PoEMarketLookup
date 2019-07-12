using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.ViewModels;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class ItemViewModelTest
    {
        private class MockPoEItem : PoEItem
        {
        }

        private class MockModdableItem : ModdableItem
        {
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithItemsBase()
        {
            var item = new MockPoEItem()
            {
                Base = "Foo"
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual("Foo", vm.ItemBase);
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithItemsImplicitModsWrappedInItemModContainers()
        {
            var mods = new Mod[]
            {
                Mod.Parse("Foo"),
                Mod.Parse("Bar")
            };

            var item = new MockModdableItem()
            {
                ImplicitMods = mods
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(mods[0], vm.ItemImplicits[0].Mod);
            Assert.AreEqual(mods[1], vm.ItemImplicits[1].Mod);
        }

        [TestMethod]
        public void CreateViewModelReturnsViewModelWithItemsExplicitModsWrappedInItemModContainers()
        {
            var mods = new Mod[]
            {
                Mod.Parse("Foo"),
                Mod.Parse("Bar")
            };

            var item = new MockModdableItem()
            {
                ExplicitMods = mods
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(mods[0], vm.ItemExplicits[0].Mod);
            Assert.AreEqual(mods[1], vm.ItemExplicits[1].Mod);
        }

        [TestMethod]
        public void WeaponStatsTotalDPSEqualsTheNormalizedTotalDPS()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 10,
                    TopEnd = 100
                }
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetTotalDPS(true), vm.WeaponDPS.Value);
        }

        [TestMethod]
        public void WeaponStatsTotalDPSEqualsNotNormalizedTotalDPSIfCorrupted()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 10,
                    TopEnd = 100
                },
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetTotalDPS(false), vm.WeaponDPS.Value);
        }

        [TestMethod]
        public void WeaponStatsPhysicalDPSEqualsNormalizedPhysicalDPS()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 10,
                    TopEnd = 100
                }
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetPhysicalDPS(true), vm.WeaponPDPS.Value);
        }

        [TestMethod]
        public void WeaponStatsPhysicalDPSEqualsNotNormalizedPhyiscalDPSIfCorrupted()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2,
                PhysicalDamage = new DamageRange
                {
                    BottomEnd = 10,
                    TopEnd = 100
                },
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetPhysicalDPS(false), vm.WeaponPDPS.Value);
        }

        [TestMethod]
        public void WeaponStatsEDPSEqualsWeaponElementalDPS()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 2,
                LightningDamage = new DamageRange
                {
                    BottomEnd = 1,
                    TopEnd = 100
                }
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetElementalDPS(), vm.WeaponEDPS.Value);
        }

        [TestMethod]
        public void WeaponStatsAPSEqualsWeaponAttacksPerSecond()
        {
            var item = new Weapon()
            {
                AttacksPerSecond = 1.65,
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.AttacksPerSecond, vm.WeaponAPS.Value);
        }

        [TestMethod]
        public void ArmorArmourStatIsQualityNormalized()
        {
            var item = new Armor()
            {
                Armour = 1000
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetNormalizedArmourValue(), vm.ArmorAR.Value);
        }

        [TestMethod]
        public void ArmorArmourStatIsNotQualityNormalizedIfCorrupted()
        {
            var item = new Armor()
            {
                Armour = 1000,
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.Armour, vm.ArmorAR.Value);
        }

        [TestMethod]
        public void ArmorEvasionStatIsQualityNormalized()
        {
            var item = new Armor()
            {
                EvasionRating = 1000
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetNormalizedEvasionValue(), vm.ArmorEV.Value);
        }

        [TestMethod]
        public void ArmorEvasionStatIsNotQualityNormalizedIfCorrupted()
        {
            var item = new Armor()
            {
                EvasionRating = 1000,
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.EvasionRating, vm.ArmorEV.Value);
        }

        [TestMethod]
        public void ArmorEnergyShieldStatIsQualityNormalized()
        {
            var item = new Armor()
            {
                EnergyShield = 1000
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.GetNormalizedEnergyShieldValue(), vm.ArmorES.Value);
        }

        [TestMethod]
        public void ArmorEnergyShieldStatIsNotQualityNormalizedIfCorrupted()
        {
            var item = new Armor()
            {
                EnergyShield = 1000,
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.EnergyShield, vm.ArmorES.Value);
        }

        [TestMethod]
        public void ArmorItemEnchantIsSetIfEnchantExists()
        {
            var item = new Armor()
            {
                Enchantment = Mod.Parse("Foo")
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.Enchantment, vm.ItemEnchant.Mod);
        }

        [TestMethod]
        public void ModdableItemsNameIsSet()
        {
            var item = new MockModdableItem()
            {
                Name = "Foo"
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.Name, vm.ItemName);
        }

        [TestMethod]
        public void ItemTypeIsSet()
        {
            var item = new MockModdableItem()
            {
                Category = PoEItemType.Amulet
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(item.Category, vm.ItemType);
        }

        [TestMethod]
        public void WeaponStatsContainsTotalDPS()
        {
            var item = new Weapon();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.WeaponDPS));
        }

        [TestMethod]
        public void WeaponStatsContainsEDPS()
        {
            var item = new Weapon();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.WeaponEDPS));
        }

        [TestMethod]
        public void WeaponStatsContainsPDPS()
        {
            var item = new Weapon();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.WeaponPDPS));
        }

        [TestMethod]
        public void WeaponStatsContainsAPS()
        {
            var item = new Weapon();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.WeaponAPS));
        }

        [TestMethod]
        public void ArmorStatsContainsAR()
        {
            var item = new Armor();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.ArmorAR));
        }

        [TestMethod]
        public void ArmorStatsContainsEV()
        {
            var item = new Armor();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.ArmorEV));
        }

        [TestMethod]
        public void ArmorStatsContainsES()
        {
            var item = new Armor();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.ArmorES));
        }

        [TestMethod]
        public void ShaperBaseIsSetForModdableItems()
        {
            var item = new MockModdableItem()
            {
                Shaper = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ShaperBase.Value);
        }

        [TestMethod]
        public void ElderBaseIsSetForModdableItems()
        {
            var item = new MockModdableItem()
            {
                Elder = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ElderBase.Value);
        }

        [TestMethod]
        public void CorruptedPropertyIsSetForModdableItems()
        {
            var item = new MockModdableItem()
            {
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.CorruptedItem.Value);
        }

        [TestMethod]
        public void MirroredPropertyIsSetForModdableItems()
        {
            var item = new MockModdableItem()
            {
                Mirrored = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.MirroredItem.Value);
        }

        [TestMethod]
        public void SynthesisedPropertyIsSetForModdableItems()
        {
            var item = new MockModdableItem()
            {
                Synthesised = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.SynthesisedItem.Value);
        }

        [TestMethod]
        public void ShaperStatIsAddedToStatsListIfTrue()
        {
            var item = new MockModdableItem()
            {
                Shaper = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.ShaperBase));
        }

        [TestMethod]
        public void ElderStatIsAddedToStatsListIfTrue()
        {
            var item = new MockModdableItem()
            {
                Elder = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.ElderBase));
        }

        [TestMethod]
        public void CorruptedStatIsAddedToStatsListIfTrue()
        {
            var item = new MockModdableItem()
            {
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.CorruptedItem));
        }

        [TestMethod]
        public void MirroredStatIsAddedToStatsListIfTrue()
        {
            var item = new MockModdableItem()
            {
                Mirrored = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.MirroredItem));
        }

        [TestMethod]
        public void SynthesisedStatIsAddedToStatsListIfTrue()
        {
            var item = new MockModdableItem()
            {
                Synthesised = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ItemStats.Contains(vm.SynthesisedItem));
        }

        [TestMethod]
        public void ShaperStatPropertyIsCheckedIfTrue()
        {
            var item = new MockModdableItem()
            {
                Shaper = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ShaperBase.Checked);
        }

        [TestMethod]
        public void ElderStatPropertyIsCheckedIfTrue()
        {
            var item = new MockModdableItem()
            {
                Elder = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.ElderBase.Checked);
        }

        [TestMethod]
        public void CorruptedStatPropertyIsCheckedIfTrue()
        {
            var item = new MockModdableItem()
            {
                Corrupted = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.CorruptedItem.Checked);
        }

        [TestMethod]
        public void MirroredStatPropertyIsCheckedIfTrue()
        {
            var item = new MockModdableItem()
            {
                Mirrored = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.MirroredItem.Checked);
        }

        [TestMethod]
        public void SynthesisedStatPropertyIsCheckedIfTrue()
        {
            var item = new MockModdableItem()
            {
                Synthesised = true
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.IsTrue(vm.SynthesisedItem.Checked);
        }

        [TestMethod]
        public void SocketCountIsZeroIfSocketGroupIsNull()
        {
            var item = new MockModdableItem();
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(0, vm.SocketCount.Value);
        }

        [TestMethod]
        public void SocketCountIsEqualToItemSocketCount()
        {
            var item = new MockModdableItem()
            {
                Sockets = SocketGroup.Parse("B B")
            };
            var vm = ItemViewModel.CreateViewModel(item);

            Assert.AreEqual(2, vm.SocketCount.Value);
        }
    }
}
