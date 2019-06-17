using System;
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

        [TestInitialize]
        public void SetWeaponVM()
        {
            _testWeaponVM = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword1H,
                WeaponDPS = new ItemStat("dps", 100),
                WeaponEDPS = new ItemStat("edps", 150),
                WeaponPDPS = new ItemStat("pdps", 200),
                WeaponAPS = new ItemStat("aps", 1.5)
            };
            _testWeaponVM.WeaponDPS.Checked = true;
            _testWeaponVM.WeaponEDPS.Checked = true;
            _testWeaponVM.WeaponPDPS.Checked = true;
            _testWeaponVM.WeaponAPS.Checked = true;

            _testArmorVM = new ItemViewModel()
            {
                ItemType = PoEItemType.BodyArmor,
                ArmorAR = new ItemStat("ar", 100)
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
            var converter = new PoEJsonConverter(_testArmorVM);
            string json = converter.SerializeSearchParameters();
            var jo = JToken.Parse(json);
            var ar = jo["query"]["filters"]["armour_filters"]["filters"]["ar"];

            Assert.AreEqual(_testArmorVM.ArmorAR.Value * .9, ar["min"]);
            Assert.AreEqual(_testArmorVM.ArmorAR.Value * 1.1, ar["max"]);
        }
    }
}
