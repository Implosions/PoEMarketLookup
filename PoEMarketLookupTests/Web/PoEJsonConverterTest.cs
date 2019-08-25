using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.ViewModels;
using PoEMarketLookup.Web;

namespace PoEMarketLookupTests.Web
{
    [TestClass]
    public class PoEJsonConverterTest
    {
        private ItemViewModel _testWeaponVM;
        private ItemViewModel _testArmorVM;
        private Mod _modAttAndCastSpd = Mod.Parse("16% increased Attack and Cast Speed if you've Killed Recently");
        private Mod _modAddedLightningDmg = Mod.Parse("Adds 10 to 90 Lightning Damage to Attacks");

        [TestInitialize]
        public void SetWeaponVM()
        {
            _testWeaponVM = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword1H,
                WeaponDPS = new ItemStat<double>("dps", 100),
                WeaponEDPS = new ItemStat<double>("edps", 150),
                WeaponPDPS = new ItemStat<double>("pdps", 200),
                WeaponAPS = new ItemStat<double>("aps", 1.5)
            };
            _testWeaponVM.WeaponDPS.Checked = true;
            _testWeaponVM.WeaponEDPS.Checked = true;
            _testWeaponVM.WeaponPDPS.Checked = true;
            _testWeaponVM.WeaponAPS.Checked = true;

            _testArmorVM = new ItemViewModel()
            {
                ItemType = PoEItemType.BodyArmor,
                ArmorAR = new ItemStat<int>("ar", 100),
                ArmorEV = new ItemStat<int>("ev", 200),
                ArmorES = new ItemStat<int>("es", 300)
            };
        }

        [TestMethod]
        public void SerializeSearchParametersHasQueryObject()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var query = jo.SelectToken("query", false);

            Assert.IsNotNull(query);
        }

        [TestMethod]
        public void SerializeSearchParametersQueryObjectHasStatusChildPropertyWithValueAny()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            string status = jo["query"]["status"]["option"].ToString();

            Assert.AreEqual("any", status);
        }

        [TestMethod]
        public void SerializeSearchParametersQueryObjectHasFiltersChildProperty()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var filters = jo["query"].SelectToken("filters", false);

            Assert.IsNotNull(filters);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersDPSMinValueIsCalculatedFromTheLowerBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, lowerBound:90);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["min"];
            double expectedDps = _testWeaponVM.WeaponDPS.Value * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersDPSMaxValueIsCalculatedFromTheUpperBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, upperBound:110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["max"];

            Assert.AreEqual(110, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersDPSIsOnlyIncludedIfChecked()
        {
            _testWeaponVM.WeaponDPS.Checked = false;
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var dps = jo["query"]["filters"]["weapon_filters"]["filters"].SelectToken("dps", false);

            Assert.IsNull(dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersEDPSMinValueIsCalculatedFromTheLowerBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, lowerBound:90);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["edps"]["min"];
            double expectedDps = _testWeaponVM.WeaponEDPS.Value * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersEDPSMaxValueIsCalculatedFromTheUpperBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, upperBound:110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["edps"]["max"];
            double expectedDps = _testWeaponVM.WeaponEDPS.Value * 1.1;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersEDPSIsOnlyIncludedIfChecked()
        {
            _testWeaponVM.WeaponEDPS.Checked = false;
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var dps = jo["query"]["filters"]["weapon_filters"]["filters"].SelectToken("edps", false);

            Assert.IsNull(dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersPDPSMinValueIsCalculatedFromTheLowerBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, lowerBound:90);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["pdps"]["min"];
            double expectedDps = _testWeaponVM.WeaponPDPS.Value * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersPDPSMaxValueIsCalculatedFromTheUpperBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, upperBound:110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["pdps"]["max"];

            Assert.AreEqual(220, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersPDPSIsOnlyIncludedIfChecked()
        {
            _testWeaponVM.WeaponPDPS.Checked = false;
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var dps = jo["query"]["filters"]["weapon_filters"]["filters"].SelectToken("pdps", false);

            Assert.IsNull(dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersAPSMinValueIsCalculatedFromTheLowerBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, lowerBound:90);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double aps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["aps"]["min"];
            double expectedAps = _testWeaponVM.WeaponAPS.Value * .9;

            Assert.AreEqual(expectedAps, aps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersAPSMaxValueIsCalculatedFromTheUpperBoundPercentage()
        {
            var converter = new PoEJsonConverter(_testWeaponVM, upperBound:110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double aps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["aps"]["max"];

            Assert.AreEqual(1.65, aps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersAPSIsOnlyIncludedIfChecked()
        {
            _testWeaponVM.WeaponAPS.Checked = false;
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var aps = jo["query"]["filters"]["weapon_filters"]["filters"].SelectToken("aps", false);

            Assert.IsNull(aps);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorFiltersHasArmourFiltersProperty()
        {
            var converter = new PoEJsonConverter(_testArmorVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var filter = jo["query"]["filters"].SelectToken("armour_filters", false);

            Assert.IsNotNull(filter);
        }

        [TestMethod]
        public void SerializeSearchParametersArmourFiltersHasFiltersProperty()
        {
            var converter = new PoEJsonConverter(_testArmorVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var filter = jo["query"]["filters"]["armour_filters"].SelectToken("filters", false);

            Assert.IsNotNull(filter);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorARMinAndMaxAreCalculatedFromLowerAndUpperBoundPercentageValues()
        {
            _testArmorVM.ArmorAR.Checked = true;
            var converter = new PoEJsonConverter(_testArmorVM, 90, 110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var ar = jo["query"]["filters"]["armour_filters"]["filters"]["ar"];

            Assert.AreEqual(_testArmorVM.ArmorAR.Value * .9, ar["min"]);
            Assert.AreEqual(_testArmorVM.ArmorAR.Value * 1.1, ar["max"]);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorARIsIncludedOnlyWhenChecked()
        {
            var converter = new PoEJsonConverter(_testArmorVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var filter = jo["query"]["filters"]["armour_filters"]["filters"].SelectToken("ar", false);

            Assert.IsNull(filter);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorEVMinAndMaxAreCalculatedFromLowerAndUpperBoundPercentageValues()
        {
            _testArmorVM.ArmorEV.Checked = true;
            var converter = new PoEJsonConverter(_testArmorVM, 90, 110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var ev = jo["query"]["filters"]["armour_filters"]["filters"]["ev"];

            Assert.AreEqual(_testArmorVM.ArmorEV.Value * .9, ev["min"]);
            Assert.AreEqual(_testArmorVM.ArmorEV.Value * 1.1, ev["max"]);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorEVIsIncludedOnlyWhenChecked()
        {
            var converter = new PoEJsonConverter(_testArmorVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var filter = jo["query"]["filters"]["armour_filters"]["filters"].SelectToken("ev", false);

            Assert.IsNull(filter);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorESMinAndMaxAreCalculatedFromLowerAndUpperBoundPercentageValues()
        {
            _testArmorVM.ArmorES.Checked = true;
            var converter = new PoEJsonConverter(_testArmorVM, 90, 110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var es = jo["query"]["filters"]["armour_filters"]["filters"]["es"];

            Assert.AreEqual(_testArmorVM.ArmorES.Value * .9, es["min"]);
            Assert.AreEqual(_testArmorVM.ArmorES.Value * 1.1, es["max"]);
        }

        [TestMethod]
        public void SerializeSearchParametersArmorESIsIncludedOnlyWhenChecked()
        {
            var converter = new PoEJsonConverter(_testArmorVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var filter = jo["query"]["filters"]["armour_filters"]["filters"].SelectToken("es", false);

            Assert.IsNull(filter);
        }

        [TestMethod]
        public void SerializeSearchParametersQueryHasStatsParameter()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("stats", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatsFirstObjectIsNotNull()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0];

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatsGroupHasTypeParameter()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0].SelectToken("type", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatsGroupTypeParameterHasValueOfAnd()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["type"];

            Assert.AreEqual("and", param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatsGroupHasFiltersParameter()
        {
            var converter = new PoEJsonConverter(new ItemViewModel());
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0].SelectToken("filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatGroupFiltersHasAnObjectWithAnIdParameterIfTheItemEnchantmentIsNotNull()
        {
            var vm = new ItemViewModel()
            {
                ItemEnchant = new ItemModContainer(_modAttAndCastSpd)
            };
            vm.ItemEnchant.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0].SelectToken("id", false);

            Assert.IsNotNull("param");
        }

        [TestMethod]
        public void SerializeSearchParametersEnchantStatFilterStartsHasAnEnchantPrefix()
        {
            var vm = new ItemViewModel()
            {
                ItemEnchant = new ItemModContainer(_modAttAndCastSpd)
            };
            vm.ItemEnchant.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.IsTrue(param.StartsWith("enchant."));
        }

        [TestMethod]
        public void SerializeSearchParametersEnchantStatFilterIdMatchesTheStatType()
        {
            var vm = new ItemViewModel()
            {
                ItemEnchant = new ItemModContainer(_modAttAndCastSpd)
            };
            vm.ItemEnchant.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.AreEqual("enchant.stat_4135304575", param);
        }

        [TestMethod]
        public void SerializeSearchParametersEnchantStatFilterIsEmptyIfThereIsNoCorrespondingStatId()
        {
            var vm = new ItemViewModel()
            {
                ItemEnchant = new ItemModContainer(
                    Mod.Parse("foo"))
            };
            vm.ItemEnchant.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(0, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersEnchantStatFilterIsEmptyIfNotChecked()
        {
            var vm = new ItemViewModel()
            {
                ItemEnchant = new ItemModContainer(_modAttAndCastSpd)
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(0, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFiltersHasNumberOfImplicitObjectsAdded()
        {
            var vm = new ItemViewModel()
            {
                ItemImplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd),
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemImplicits[0].Checked = true;
            vm.ItemImplicits[1].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(2, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterImplicitHasIdParameter()
        {
            var vm = new ItemViewModel()
            {
                ItemImplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemImplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0].SelectToken("id", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterImplicitHasImplicitPrefix()
        {
            var vm = new ItemViewModel()
            {
                ItemImplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemImplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.IsTrue(param.StartsWith("implicit."));
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterImplicitIdIsEqualToStatId()
        {
            var vm = new ItemViewModel()
            {
                ItemImplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemImplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.AreEqual("implicit.stat_4135304575", param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterImplicitIsIgnoredIfTheStatIdDoesNotExist()
        {
            var vm = new ItemViewModel()
            {
                ItemImplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(Mod.Parse("foo"))
                }
            };
            vm.ItemImplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(0, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterImplicitIsIgnoredIfItIsNotChecked()
        {
            var vm = new ItemViewModel()
            {
                ItemImplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(0, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterExplicitsAreAdded()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd),
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemExplicits[0].Checked = true;
            vm.ItemExplicits[1].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(2, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterExplicitParameterHasIdProperty()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemExplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0].SelectToken("id", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterExplicitIdHasExplicitPrefix()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemExplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.IsTrue(param.StartsWith("explicit."));
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterExplicitParameterHasStatId()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            vm.ItemExplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.AreEqual("explicit.stat_4135304575", param);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterExplicitsAreNotAddedIfIdIsNotFound()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(Mod.Parse("foo"))
                }
            };
            vm.ItemExplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(0, paramCount);
        }

        [TestMethod]
        public void SerializeSearchParametersStatFilterExplicitsAreNotAddedIfNotChecked()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAttAndCastSpd)
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int paramCount = jo["query"]["stats"][0]["filters"].Count();

            Assert.AreEqual(0, paramCount);
        }

        [TestMethod]
        public void StatsHasTotalLifeParameterIfChecked()
        {
            var vm = new ItemViewModel()
            {
                TotalLife = new ItemStat<int>("life", 0)
            };
            vm.TotalLife.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            string paramId = jo["query"]["stats"][0]["filters"][0].SelectToken("id", false).ToString();

            Assert.AreEqual("pseudo.pseudo_total_life", paramId);
        }

        [TestMethod]
        public void StatsTotalLifeParameterHasMinValueCalculatedFromTheLowerBoundPercentage()
        {
            var vm = new ItemViewModel()
            {
                TotalLife = new ItemStat<int>("life", 10)
            };
            vm.TotalLife.Checked = true;
            var converter = new PoEJsonConverter(vm, lowerBound:90);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int minVal = (int)jo["query"]["stats"][0]["filters"][0]["value"].SelectToken("min", false);

            Assert.AreEqual(9, minVal);
        }

        [TestMethod]
        public void StatsTotalLifeParameterHasMaxValueCalculatedFromTheUpperBoundPercentage()
        {
            var vm = new ItemViewModel()
            {
                TotalLife = new ItemStat<int>("life", 10)
            };
            vm.TotalLife.Checked = true;
            var converter = new PoEJsonConverter(vm, upperBound:110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int maxVal = (int)jo["query"]["stats"][0]["filters"][0]["value"].SelectToken("max", false);

            Assert.AreEqual(11, maxVal);
        }

        [TestMethod]
        public void StatsHasTotalResistancesParameterIfChecked()
        {
            var vm = new ItemViewModel()
            {
                TotalResistances = new ItemStat<int>("resists", 0)
            };
            vm.TotalResistances.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            string paramId = jo["query"]["stats"][0]["filters"][0].SelectToken("id", false).ToString();

            Assert.AreEqual("pseudo.pseudo_total_resistance", paramId);
        }

        [TestMethod]
        public void StatsTotalResistancesParameterHasMinValueCalculatedFromTheLowerBoundPercentage()
        {
            var vm = new ItemViewModel()
            {
                TotalResistances = new ItemStat<int>("resists", 10)
            };
            vm.TotalResistances.Checked = true;
            var converter = new PoEJsonConverter(vm, lowerBound:90);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int minVal = (int)jo["query"]["stats"][0]["filters"][0]["value"].SelectToken("min", false);

            Assert.AreEqual(9, minVal);
        }

        [TestMethod]
        public void StatsTotalResistancesParameterHasMaxValueCalculatedFromTheUpperBoundPercentage()
        {
            var vm = new ItemViewModel()
            {
                TotalResistances = new ItemStat<int>("resists", 10)
            };
            vm.TotalResistances.Checked = true;
            var converter = new PoEJsonConverter(vm, upperBound:110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int maxVal = (int)jo["query"]["stats"][0]["filters"][0]["value"].SelectToken("max", false);

            Assert.AreEqual(11, maxVal);
        }

        [TestMethod]
        public void FiltersHaveSocketFilter()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"].SelectToken("socket_filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SocketFiltersHasFiltersParam()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["socket_filters"].SelectToken("filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SocketsParamIsAddedIfChecked()
        {
            var vm = new ItemViewModel()
            {
                SocketCount = new ItemStat<int>("sockets", 0)
            };
            vm.SocketCount.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["socket_filters"]["filters"].SelectToken("sockets", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SocketsParamMinValueIsEqualToSocketsValue()
        {
            var vm = new ItemViewModel()
            {
                SocketCount = new ItemStat<int>("sockets", 1)
            };
            vm.SocketCount.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (int)jo["query"]["filters"]["socket_filters"]["filters"]["sockets"]["min"];

            Assert.AreEqual(1, param);
        }

        [TestMethod]
        public void LinksParamIsAddedIfChecked()
        {
            var vm = new ItemViewModel()
            {
                Link = new ItemStat<int>("link", 0)
            };
            vm.Link.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["socket_filters"]["filters"].SelectToken("links", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void LinksParamMinValueIsEqualToLinkValue()
        {
            var vm = new ItemViewModel()
            {
                Link = new ItemStat<int>("links", 1)
            };
            vm.Link.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (int)jo["query"]["filters"]["socket_filters"]["filters"]["links"]["min"];

            Assert.AreEqual(1, param);
        }

        [TestMethod]
        public void FiltersHaveMiscFilters()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"].SelectToken("misc_filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void MiscFiltersHaveFilters()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"].SelectToken("filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void ShaperItemIsSetIfChecked()
        {
            var vm = new ItemViewModel()
            {
                ShaperBase = new ItemStat<bool>("shaper", true)
            };
            vm.ShaperBase.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("shaper_item", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void ShaperItemOptionIsSet()
        {
            var vm = new ItemViewModel()
            {
                ShaperBase = new ItemStat<bool>("shaper", true)
            };
            vm.ShaperBase.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (bool)jo["query"]["filters"]["misc_filters"]["filters"]["shaper_item"]["option"];

            Assert.AreEqual(true, param);
        }

        [TestMethod]
        public void ElderItemIsAddedIfChecked()
        {
            var vm = new ItemViewModel()
            {
                ElderBase = new ItemStat<bool>("elder", true)
            };
            vm.ElderBase.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("elder_item", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void ElderItemOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ElderBase = new ItemStat<bool>("elder", true)
            };
            vm.ElderBase.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (bool)jo["query"]["filters"]["misc_filters"]["filters"]["elder_item"]["option"];

            Assert.AreEqual(true, param);
        }

        [TestMethod]
        public void CorruptedItemParameterIsAddedIfChecked()
        {
            var vm = new ItemViewModel()
            {
                CorruptedItem = new ItemStat<bool>("corrupted", true)
            };
            vm.CorruptedItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("corrupted", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void CorruptedItemOptionIsSet()
        {
            var vm = new ItemViewModel()
            {
                CorruptedItem = new ItemStat<bool>("corrupted", true)
            };
            vm.CorruptedItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (bool)jo["query"]["filters"]["misc_filters"]["filters"]["corrupted"]["option"];

            Assert.AreEqual(true, param);
        }

        [TestMethod]
        public void MirroredItemParameterIsAddedIfChecked()
        {
            var vm = new ItemViewModel()
            {
                MirroredItem = new ItemStat<bool>("mirrored", true)
            };
            vm.MirroredItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("mirrored", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void MirroredItemOptionIsSet()
        {
            var vm = new ItemViewModel()
            {
                MirroredItem = new ItemStat<bool>("mirrored", true)
            };
            vm.MirroredItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"]["mirrored"]["option"];

            Assert.AreEqual(true, param);
        }

        [TestMethod]
        public void SynthesisedItemParameterIsAddedIfChecked()
        {
            var vm = new ItemViewModel()
            {
                SynthesisedItem = new ItemStat<bool>("synthesised", true)
            };
            vm.SynthesisedItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("synthesised_item", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void SynthesisedItemOptionIsSet()
        {
            var vm = new ItemViewModel()
            {
                SynthesisedItem = new ItemStat<bool>("synthesised", true)
            };
            vm.SynthesisedItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (bool)jo["query"]["filters"]["misc_filters"]["filters"]["synthesised_item"]["option"];

            Assert.AreEqual(true, param);
        }

        [TestMethod]
        public void FiltersHaveTypeFilterParameter()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"].SelectToken("type_filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void TypeFiltersHasFiltersParameter()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"].SelectToken("filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void FiltersHasCategoryParameterIfItemTypeIsNotUnknown()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Currency
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"].SelectToken("category", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void CurrencyOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Currency
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("currency", param);
        }

        [TestMethod]
        public void GemOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Gem
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("gem", param);
        }

        [TestMethod]
        public void FlaskOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Flask
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("flask", param);
        }

        [TestMethod]
        public void MapOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Map
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("map", param);
        }

        [TestMethod]
        public void Sword1HOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword1H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.onesword", param);
        }

        [TestMethod]
        public void Axe1HOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Axe1H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.oneaxe", param);
        }

        [TestMethod]
        public void Mace1HOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Mace1H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.onemace", param);
        }

        [TestMethod]
        public void DaggerOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Dagger
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.dagger", param);
        }

        [TestMethod]
        public void ClawOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Claw
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.claw", param);
        }

        [TestMethod]
        public void SceptreOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Sceptre
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.sceptre", param);
        }

        [TestMethod]
        public void WandOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Wand
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.wand", param);
        }

        [TestMethod]
        public void Sword2hOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword2H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.twosword", param);
        }

        [TestMethod]
        public void Axe2hOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Axe2H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.twoaxe", param);
        }

        [TestMethod]
        public void Mace2hOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Mace2H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.twomace", param);
        }

        [TestMethod]
        public void StaffOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Staff
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.staff", param);
        }

        [TestMethod]
        public void BowOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Bow
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.bow", param);
        }

        [TestMethod]
        public void FishingRodOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.FishingRod
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("weapon.rod", param);
        }

        [TestMethod]
        public void AmuletOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Amulet
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("accessory.amulet", param);
        }

        [TestMethod]
        public void RingOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Ring
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("accessory.ring", param);
        }

        [TestMethod]
        public void BeltOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Belt
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("accessory.belt", param);
        }

        [TestMethod]
        public void QuiverOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Quiver
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("armour.quiver", param);
        }

        [TestMethod]
        public void JewelOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Jewel
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("jewel", param);
        }

        [TestMethod]
        public void HelmetOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Helmet
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("armour.helmet", param);
        }

        [TestMethod]
        public void GlovesOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Gloves
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("armour.gloves", param);
        }

        [TestMethod]
        public void BootsOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Boots
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("armour.boots", param);
        }

        [TestMethod]
        public void BodyArmorOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.BodyArmor
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("armour.chest", param);
        }

        [TestMethod]
        public void ShieldOptionValueIsSet()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Shield
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["type_filters"]["filters"]["category"].SelectToken("option", false);

            Assert.AreEqual("armour.shield", param);
        }

        [TestMethod]
        public void ItemEnchantMinValueIsEqualToOriginalAvgValue()
        {
            var vm = new ItemViewModel()
            {
                ItemEnchant = new ItemModContainer(_modAddedLightningDmg)
            };
            vm.ItemEnchant.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = (int)jo["query"]["stats"][0]["filters"][0]["value"]["min"];

            Assert.AreEqual(50, param);
        }

        [TestMethod]
        public void ItemExplicitModMinAndMaxValueAreCalculatedFromTheLowerAndUpperBoundPercentages()
        {
            var vm = new ItemViewModel()
            {
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(_modAddedLightningDmg)
                }
            };
            vm.ItemExplicits[0].Checked = true;
            var converter = new PoEJsonConverter(vm, 90, 110);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var min = (int)jo["query"]["stats"][0]["filters"][0]["value"]["min"];
            var max = (int)jo["query"]["stats"][0]["filters"][0]["value"]["max"];

            Assert.AreEqual(45, min);
            Assert.AreEqual(55, max);
        }

        [TestMethod]
        public void QueryHasNameParameterIfItemIsUnique()
        {
            var vm = new ItemViewModel()
            {
                ItemName = "foo",
                ItemRarity = Rarity.Unique
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("name", false);

            Assert.AreEqual("foo", param);
        }

        [TestMethod]
        public void QueryHasTypeParameterIfItemIsUnique()
        {
            var vm = new ItemViewModel()
            {
                ItemBase = "bar",
                ItemRarity = Rarity.Unique
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("type", false);

            Assert.AreEqual("bar", param);
        }

        [TestMethod]
        public void StatGetsLocalModVersionIdIfOnArmor()
        {
            var mod = Mod.Parse("100% increased Evasion Rating");
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Helmet,
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(mod)
                    {
                        Checked = true
                    }
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.AreEqual("explicit.stat_124859000", param);
        }

        [TestMethod]
        public void StatGetsLocalModVersionIdIfOnWeapons()
        {
            var mod = Mod.Parse("Adds 10 to 20 Physical Damage");
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword2H,
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(mod)
                    {
                        Checked = true
                    }
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["stats"][0]["filters"][0]["id"].ToString();

            Assert.AreEqual("explicit.stat_1940865751", param);
        }

        [TestMethod]
        public void WeaponStatOnlyGetsLocalStatsThatCanBeOnWeapons()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword2H,
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(Mod.Parse("Adds 10 to 20 Physical Damage"))
                    {
                        Checked = true
                    },
                    new ItemModContainer(Mod.Parse("100% increased Evasion Rating"))
                    {
                        Checked = true
                    }
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param1 = jo["query"]["stats"][0]["filters"][0]["id"].ToString();
            var param2 = jo["query"]["stats"][0]["filters"][1]["id"].ToString();

            Assert.AreEqual("explicit.stat_1940865751", param1);
            Assert.AreEqual("explicit.stat_2106365538", param2);
        }

        [TestMethod]
        public void ArmorStatOnlyGetsLocalStatsThatCanBeOnArmor()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Helmet,
                ItemExplicits = new List<ItemModContainer>()
                {
                    new ItemModContainer(Mod.Parse("Adds 10 to 20 Physical Damage"))
                    {
                        Checked = true
                    },
                    new ItemModContainer(Mod.Parse("100% increased Evasion Rating"))
                    {
                        Checked = true
                    }
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param1 = jo["query"]["stats"][0]["filters"][0]["id"].ToString();
            var param2 = jo["query"]["stats"][0]["filters"][1]["id"].ToString();

            Assert.AreEqual("explicit.stat_960081730", param1);
            Assert.AreEqual("explicit.stat_124859000", param2);
        }

        [TestMethod]
        public void FracturedItemIsSetIfChecked()
        {
            var vm = new ItemViewModel()
            {
                FracturedItem = new ItemStat<bool>("fractured", true)
            };
            vm.FracturedItem.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("fractured_item", false);

            Assert.IsNotNull(param);
            Assert.AreEqual(true, param["option"]);
        }

        [TestMethod]
        public void ItemLevelIsSetIfChecked()
        {
            var vm = new ItemViewModel()
            {
                ItemLevel = new ItemStat<int>("level", 1)
                {
                    Checked = true
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("gem_level", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void ItemLevelHasMinAndMaxValues()
        {
            var vm = new ItemViewModel()
            {
                ItemLevel = new ItemStat<int>("level", 10)
                {
                    Checked = true
                }
            };
            var converter = new PoEJsonConverter(vm, 100, 100);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("gem_level", false);

            Assert.AreEqual(10, (int)param["min"]);
            Assert.AreEqual(10, (int)param["max"]);
        }

        [TestMethod]
        public void ItemQualityIsSetIfChecked()
        {
            var vm = new ItemViewModel()
            {
                ItemQuality = new ItemStat<int>("quality", 0)
                {
                    Checked = true
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("quality", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void ItemQualityMinAndMaxAreSet()
        {
            var vm = new ItemViewModel()
            {
                ItemQuality = new ItemStat<int>("quality", 10)
                {
                    Checked = true
                }
            };
            var converter = new PoEJsonConverter(vm, 100, 100);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("quality", false);

            Assert.AreEqual(10, (int)param["min"]);
            Assert.AreEqual(10, (int)param["max"]);
        }

        [TestMethod]
        public void QueryTypeParameterIsSetIfItemIsAGem()
        {
            var vm = new ItemViewModel()
            {
                ItemBase = "bar",
                ItemType = PoEItemType.Gem
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("type", false);

            Assert.AreEqual("bar", param);
        }

        [TestMethod]
        public void FiltersHasTradeFiltersCategory()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"].SelectToken("trade_filters", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void TradeFiltersHasSaleTypePropertyWithAValueOfPriced()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["trade_filters"]["filters"]["sale_type"]["option"];

            Assert.AreEqual("priced", param);
        }

        [TestMethod]
        public void TradeFiltersHasIndexedPropertyWithValueOf1Week()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["trade_filters"]["filters"]["indexed"]["option"];

            Assert.AreEqual("1week", param);
        }

        [TestMethod]
        public void SetSortByPriceAscending()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["sort"]["price"];

            Assert.AreEqual("asc", param);
        }

        [TestMethod]
        public void MinValuesAreRounded()
        {
            var vm = new ItemViewModel()
            {
                WeaponDPS = new ItemStat<double>("dps", 123.45)
                {
                    Checked = true
                },
                ItemType = PoEItemType.Axe1H
            };
            var converter = new PoEJsonConverter(vm, lowerBound:33);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["min"];

            Assert.AreEqual(40.74, param);
        }

        [TestMethod]
        public void MaxValuesAreRounded()
        {
            var vm = new ItemViewModel()
            {
                WeaponDPS = new ItemStat<double>("dps", 123.45)
                {
                    Checked = true
                },
                ItemType = PoEItemType.Axe1H
            };
            var converter = new PoEJsonConverter(vm, upperBound: 33);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["max"];

            Assert.AreEqual(40.74, param);
        }

        [TestMethod]
        public void WeaponFiltersHasCritChancePropertyIfChecked()
        {
            var vm = new ItemViewModel()
            {
                WeaponCritChance = new ItemStat<double>("crit", 1)
                {
                    Checked = true
                },
                ItemType = PoEItemType.Axe1H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["weapon_filters"]["filters"].SelectToken("crit", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void CritChancePropertyHasMinAndMaxValues()
        {
            var vm = new ItemViewModel()
            {
                WeaponCritChance = new ItemStat<double>("crit", 1)
                {
                    Checked = true
                },
                ItemType = PoEItemType.Axe1H
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["weapon_filters"]["filters"].SelectToken("crit", false);

            Assert.AreEqual(.9, param["min"]);
            Assert.AreEqual(1.1, param["max"]);
        }

        [TestMethod]
        public void TradeFiltersHasPricePropertyWithMinValue()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["trade_filters"]["filters"]["price"]["min"];

            Assert.AreEqual(.00001, param);
        }
        
        [TestMethod]
        public void QueryHasTypeParameterWithItemBaseIfFragment()
        {
            var vm = new ItemViewModel()
            {
                ItemBase = "foo",
                ItemType = PoEItemType.Fragment
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("type", false);

            Assert.AreEqual("foo", param);
        }

        [TestMethod]
        public void QueryHasTypeParameterOfProphecyIfItemIsAProphecy()
        {
            var vm = new ItemViewModel()
            {
                ItemType = PoEItemType.Prophecy
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("type", false);

            Assert.AreEqual("Prophecy", param);
        }

        [TestMethod]
        public void QueryNameParameterIsTheItemBaseIfItemIsAProphecy()
        {
            var vm = new ItemViewModel()
            {
                ItemBase = "foo",
                ItemType = PoEItemType.Prophecy
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("name", false);

            Assert.AreEqual("foo", param);
        }

        [TestMethod]
        public void QueryTypeParameterIsTheItemBaseIfItemIsAMap()
        {
            var vm = new ItemViewModel()
            {
                ItemBase = "foo",
                ItemType = PoEItemType.Map
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("type", false);

            Assert.AreEqual("foo", param);
        }

        [TestMethod]
        public void MiscFiltersHasIdentifiedPropertyIfUnidItemIsChecked()
        {
            var vm = new ItemViewModel()
            {
                UnidItem = new ItemStat<bool>("unid", true)
                {
                    Checked = true
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"].SelectToken("identified", false);

            Assert.IsNotNull(param);
        }

        [TestMethod]
        public void IdentifiedPropertyValueIsTheInvertedValueOfUnidItem()
        {
            var vm = new ItemViewModel()
            {
                UnidItem = new ItemStat<bool>("unid", true)
                {
                    Checked = true
                }
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["misc_filters"]["filters"]["identified"]["option"];

            Assert.AreEqual(false, param);
        }

        [TestMethod]
        public void JsonDoesNotHaveNamePropertyIfUniqueItemsNameIsNull()
        {
            var vm = new ItemViewModel()
            {
                ItemRarity = Rarity.Unique
            };
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"].SelectToken("name", false);

            Assert.IsNull(param);
        }

        [TestMethod]
        public void TradeFiltersHasNoIndexedPropertyIfListedTimeIsAny()
        {
            var vm = new ItemViewModel();
            var converter = new PoEJsonConverter(vm, time:ListTime.Any);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var param = jo["query"]["filters"]["trade_filters"]["filters"].SelectToken("indexed", false);

            Assert.IsNull(param);
        }
    }
}
