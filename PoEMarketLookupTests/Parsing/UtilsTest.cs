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
    }
}
