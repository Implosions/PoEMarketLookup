using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup;
using PoEMarketLookup.PoE.Items;
using PoEMarketLookup.PoE.Parsers;

namespace PoEMarketLookupTests.Parsing
{
    [TestClass]
    public class CurrencyParserTest
    {
        [TestMethod]
        public void CurrencyParserReturnsCurrencyObjWithCorrectStackSize()
        {
            var cp = new CurrencyParser(PoEItemData.Currency.EXALTED_ORB);
            Currency c = cp.Parse();

            Assert.AreEqual(9, c.StackSize);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CurrencyParserParseThrowsFormatExceptionIfStackSizeFieldIsNotFound()
        {
            var cp = new CurrencyParser(PoEItemData.Currency.ORB_MISSING_STACKSIZE_FIELD);
            cp.Parse();
        }
    }
}
