using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items.Components;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void FindItemTypeReturnsUnknownTypeIfItemIsUnrecognized()
        {
            var type = Utils.FindItemType(string.Empty);

            Assert.AreEqual(PoEItemType.Unknown, type);
        }
    }
}
