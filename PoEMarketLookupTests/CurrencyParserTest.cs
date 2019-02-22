using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup;

namespace PoEMarketLookupTests
{
    [TestClass]
    public class CurrencyParserTest
    {
        [TestMethod]
        public void CurrencyParserReturnsCurrencyObjWithCorrectBase()
        {
            CurrencyParser cp = new CurrencyParser(PoEItemData.Currency.EXALTED_ORB);
            Currency c = cp.Parse();

            Assert.AreEqual("Exalted Orb", c.Base);
        }

        [TestMethod]
        public void CurrencyParserReturnsCurrencyObjWithCorrectStackSize()
        {
            CurrencyParser cp = new CurrencyParser(PoEItemData.Currency.EXALTED_ORB);
            Currency c = cp.Parse();

            Assert.AreEqual(9, c.StackSize);
        }
    }
}
