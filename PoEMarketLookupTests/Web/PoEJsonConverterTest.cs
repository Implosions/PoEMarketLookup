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
            string status = jo["query"]["status"].ToString();

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
        public void SerializeSearchParametersWeaponFiltersDPSMinValueIsEqual90PercentOfDPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["min"];
            double expectedDps = _testWeaponVM.WeaponDPS.Value * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersDPSMxValueIsEqual110PercentOfDPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["max"];
            double expectedDps = _testWeaponVM.WeaponDPS.Value * 1.1;

            Assert.AreEqual(expectedDps, dps);
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
        public void SerializeSearchParametersWeaponFiltersEDPSMinValueIsEqual90PercentOfDPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["edps"]["min"];
            double expectedDps = _testWeaponVM.WeaponEDPS.Value * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersEDPSMxValueIsEqual110PercentOfDPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
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
        public void SerializeSearchParametersWeaponFiltersPDPSMinValueIsEqual90PercentOfPDPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["pdps"]["min"];
            double expectedDps = _testWeaponVM.WeaponPDPS.Value * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersPDPSMxValueIsEqual110PercentOfPDPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["pdps"]["max"];
            double expectedDps = _testWeaponVM.WeaponPDPS.Value * 1.1;

            Assert.AreEqual(expectedDps, dps);
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
        public void SerializeSearchParametersWeaponFiltersAPSMinValueIsEqual90PercentOfAPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double aps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["aps"]["min"];
            double expectedAps = _testWeaponVM.WeaponAPS.Value * .9;

            Assert.AreEqual(expectedAps, aps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersAPSMaxValueIsEqual110PercentOfAPSValue()
        {
            var converter = new PoEJsonConverter(_testWeaponVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            double aps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["aps"]["max"];
            double expectedAps = _testWeaponVM.WeaponAPS.Value * 1.1;

            Assert.AreEqual(expectedAps, aps);
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
        public void SerializeSearchParametersArmorARMinAndMaxArePlusAndMinus10PercentOfValue()
        {
            _testArmorVM.ArmorAR.Checked = true;
            var converter = new PoEJsonConverter(_testArmorVM);
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
        public void SerializeSearchParametersArmorEVMinAndMaxArePlusAndMinus10PercentOfValue()
        {
            _testArmorVM.ArmorEV.Checked = true;
            var converter = new PoEJsonConverter(_testArmorVM);
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
        public void SerializeSearchParametersArmorESMinAndMaxArePlusAndMinus10PercentOfValue()
        {
            _testArmorVM.ArmorES.Checked = true;
            var converter = new PoEJsonConverter(_testArmorVM);
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
        public void StatsTotalLifeParameterHasMinValueEqualTo90PercentOfOriginalValue()
        {
            var vm = new ItemViewModel()
            {
                TotalLife = new ItemStat<int>("life", 10)
            };
            vm.TotalLife.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int minVal = (int)jo["query"]["stats"][0]["filters"][0]["value"].SelectToken("min", false);

            Assert.AreEqual(9, minVal);
        }

        [TestMethod]
        public void StatsTotalLifeParameterHasMaxValueEqualTo110PercentOfOriginalValue()
        {
            var vm = new ItemViewModel()
            {
                TotalLife = new ItemStat<int>("life", 10)
            };
            vm.TotalLife.Checked = true;
            var converter = new PoEJsonConverter(vm);
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
        public void StatsTotalResistancesParameterHasMinValueEqualTo90PercentOfOriginalValue()
        {
            var vm = new ItemViewModel()
            {
                TotalResistances = new ItemStat<int>("resists", 10)
            };
            vm.TotalResistances.Checked = true;
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            int minVal = (int)jo["query"]["stats"][0]["filters"][0]["value"].SelectToken("min", false);

            Assert.AreEqual(9, minVal);
        }

        [TestMethod]
        public void StatsTotalResistancesParameterHasMaxValueEqualTo110PercentOfOriginalValue()
        {
            var vm = new ItemViewModel()
            {
                TotalResistances = new ItemStat<int>("resists", 10)
            };
            vm.TotalResistances.Checked = true;
            var converter = new PoEJsonConverter(vm);
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
    }
}
