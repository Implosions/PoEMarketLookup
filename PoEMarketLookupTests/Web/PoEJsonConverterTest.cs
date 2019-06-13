using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.ViewModels;
using PoEMarketLookup.Web;

namespace PoEMarketLookupTests.Web
{
    [TestClass]
    public class PoEJsonConverterTest
    {
        private Weapon _weapon;
        private ItemViewModel _emptyVM = ItemViewModel.CreateViewModel(new Currency());

        public PoEJsonConverterTest()
        {
            _weapon = new Weapon()
            {
                AttacksPerSecond = 1,
                PhysicalDamage = new DamageRange()
                {
                    TopEnd = 100
                },
                Category = PoEItemType.Sword1H,
                Quality = 20
            };
        }

        [TestMethod]
        public void SerializeSearchParametersHasQueryObject()
        {
            var converter = new PoEJsonConverter(_emptyVM);
            string json = converter.SerializeSearchParameters();
            var jo = JObject.Parse(json);

            Assert.IsTrue(jo.ContainsKey("query"));
        }

        [TestMethod]
        public void SerializeSearchParametersQueryObjectHasStatusChildPropertyWithValueAny()
        {
            var converter = new PoEJsonConverter(_emptyVM);
            string json = converter.SerializeSearchParameters();
            var jo = JObject.Parse(json);
            string status = jo["query"]["status"].ToString();

            Assert.AreEqual("any", status);
        }

        [TestMethod]
        public void SerializeSearchParametersQueryObjectHasFiltersChildProperty()
        {
            var converter = new PoEJsonConverter(_emptyVM);
            string json = converter.SerializeSearchParameters();
            var jo = JObject.Parse(json);
            var query = jo["query"].ToObject<JObject>();

            Assert.IsTrue(query.ContainsKey("filters"));
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersDPSMinValueIsEqual90PercentOfDPSValue()
        {
            var vm = ItemViewModel.CreateViewModel(_weapon);
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JObject.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["min"];
            double expectedDps = _weapon.GetTotalDPS() * .9;

            Assert.AreEqual(expectedDps, dps);
        }

        [TestMethod]
        public void SerializeSearchParametersWeaponFiltersDPSMxValueIsEqual110PercentOfDPSValue()
        {
            var vm = ItemViewModel.CreateViewModel(_weapon);
            var converter = new PoEJsonConverter(vm);
            string json = converter.SerializeSearchParameters();
            var jo = JObject.Parse(json);
            double dps = (double)jo["query"]["filters"]["weapon_filters"]["filters"]["dps"]["max"];
            double expectedDps = _weapon.GetTotalDPS() * 1.1;

            Assert.AreEqual(expectedDps, dps);
        }
    }
}
