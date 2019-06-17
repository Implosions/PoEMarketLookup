﻿using System;
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

        [TestInitialize]
        public void SetWeaponVM()
        {
            _testWeaponVM = new ItemViewModel()
            {
                ItemType = PoEItemType.Sword1H,
                WeaponDPS = new ItemStat("dps", 100)
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
    }
}