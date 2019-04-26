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

        private string CreateTestArmor(string baseItem)
        {
            return PoEItemData.Armor.ARMOR_TEMPLATE.Replace("$", baseItem);
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

        [TestMethod]
        public void FindItemTypeReturnsStaffTypeForStaves()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Staff"));

            Assert.AreEqual(PoEItemType.Staff, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBowTypeForBows()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Bow"));

            Assert.AreEqual(PoEItemType.Bow, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsFishingRodTypeForFishingRods()
        {
            var type = Utils.FindItemType(CreateTestWeapon("Fishing Rod"));

            Assert.AreEqual(PoEItemType.FishingRod, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsAmuletTypeForAmulets()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.AMULET_RARE);

            Assert.AreEqual(PoEItemType.Amulet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsAmuletTypeForTalismans()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.AMULET_TALISMAN);

            Assert.AreEqual(PoEItemType.Amulet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsRingTypeForRings()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.RING_RARE);

            Assert.AreEqual(PoEItemType.Ring, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBeltTypeForBelts()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.BELT_RARE);

            Assert.AreEqual(PoEItemType.Belt, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBeltTypeForRusticSashes()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.BELT_FAMINE_BIND);

            Assert.AreEqual(PoEItemType.Belt, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBeltTypeForStygianVises()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.BELT_STYGIAN_VISE_RARE);

            Assert.AreEqual(PoEItemType.Belt, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsQuiverTypeForQuivers()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.QUIVER_RARE);

            Assert.AreEqual(PoEItemType.Quiver, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsJewelTypeForJewels()
        {
            var type = Utils.FindItemType(PoEItemData.Accessories.JEWEL_RARE);

            Assert.AreEqual(PoEItemType.Jewel, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForHelmets()
        {
            var type = Utils.FindItemType(CreateTestArmor("Cone Helmet"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForHats()
        {
            var type = Utils.FindItemType(CreateTestArmor("Iron Hat"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForBurgonets()
        {
            var type = Utils.FindItemType(CreateTestArmor("Ezomyte Burgonet"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForCaps()
        {
            var type = Utils.FindItemType(CreateTestArmor("Leather Cap"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForTricornes()
        {
            var type = Utils.FindItemType(CreateTestArmor("Tricorne"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForHoods()
        {
            var type = Utils.FindItemType(CreateTestArmor("Leather Hood"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForPelts()
        {
            var type = Utils.FindItemType(CreateTestArmor("Lion Pelt"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForCirclets()
        {
            var type = Utils.FindItemType(CreateTestArmor("Vine Circlet"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForCages()
        {
            var type = Utils.FindItemType(CreateTestArmor("Torture Cage"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForHelms()
        {
            var type = Utils.FindItemType(CreateTestArmor("Battered Helm"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForSallets()
        {
            var type = Utils.FindItemType(CreateTestArmor("Sallet"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForBascinets()
        {
            var type = Utils.FindItemType(CreateTestArmor("Nightmare Bascinet"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForCoifs()
        {
            var type = Utils.FindItemType(CreateTestArmor("Rusted Coif"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForCrowns()
        {
            var type = Utils.FindItemType(CreateTestArmor("Great Crown"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsHelmetTypeForMasks()
        {
            var type = Utils.FindItemType(CreateTestArmor("Vaal Mask"));

            Assert.AreEqual(PoEItemType.Helmet, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsGlovesTypeForGloves()
        {
            var type = Utils.FindItemType(CreateTestArmor("Slink Gloves"));

            Assert.AreEqual(PoEItemType.Gloves, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsGlovesTypeForGauntlets()
        {
            var type = Utils.FindItemType(CreateTestArmor("Iron Gauntlets"));

            Assert.AreEqual(PoEItemType.Gloves, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsGlovesTypeForMitts()
        {
            var type = Utils.FindItemType(CreateTestArmor("Carnal Mitts"));

            Assert.AreEqual(PoEItemType.Gloves, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBootsTypeForBoots()
        {
            var type = Utils.FindItemType(CreateTestArmor("Slink Boots"));

            Assert.AreEqual(PoEItemType.Boots, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBootsTypeForGreaves()
        {
            var type = Utils.FindItemType(CreateTestArmor("Iron Greaves"));

            Assert.AreEqual(PoEItemType.Boots, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBootsTypeForShoes()
        {
            var type = Utils.FindItemType(CreateTestArmor("Wool Shoes"));

            Assert.AreEqual(PoEItemType.Boots, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBootsTypeForSlippers()
        {
            var type = Utils.FindItemType(CreateTestArmor("Arcanist Slippers"));

            Assert.AreEqual(PoEItemType.Boots, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBodyArmorTypeForVests()
        {
            var type = Utils.FindItemType(CreateTestArmor("Plate Vest"));

            Assert.AreEqual(PoEItemType.BodyArmor, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBodyArmorTypeForChestplates()
        {
            var type = Utils.FindItemType(CreateTestArmor("Chestplate"));

            Assert.AreEqual(PoEItemType.BodyArmor, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBodyArmorTypeForPlates()
        {
            var type = Utils.FindItemType(CreateTestArmor("Astral Plate"));

            Assert.AreEqual(PoEItemType.BodyArmor, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBodyArmorTypeForJerkins()
        {
            var type = Utils.FindItemType(CreateTestArmor("Shabby Jerkin"));

            Assert.AreEqual(PoEItemType.BodyArmor, type);
        }

        [TestMethod]
        public void FindItemTypeReturnsBodyArmorTypeForLeathers()
        {
            var type = Utils.FindItemType(CreateTestArmor("Strapped Leather"));

            Assert.AreEqual(PoEItemType.BodyArmor, type);
        }
    }
}
