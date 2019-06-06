using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PoEMarketLookup.Web;

namespace PoEMarketLookupTests.Web
{
    [TestClass]
    public class PoEJsonConverterTest
    {
        [TestMethod]
        public void SerializeSearchParametersHasQueryObject()
        {
            string json = PoEJsonConverter.SerializeSearchParameters();
            var jo = JObject.Parse(json);

            Assert.IsTrue(jo.ContainsKey("query"));
        }

        [TestMethod]
        public void SerializeSearchParametersQueryObjectHasStatusChildPropertyWithValueAny()
        {
            string json = PoEJsonConverter.SerializeSearchParameters();
            var jo = JObject.Parse(json);
            string status = jo["query"]["status"].ToString();

            Assert.AreEqual("any", status);
        }
    }
}
