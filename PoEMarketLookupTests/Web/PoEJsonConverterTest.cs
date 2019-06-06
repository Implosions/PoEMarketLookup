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
    }
}
