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
            var cp = new CurrencyParser(PoEItemData.Currency.EXALTED_ORB);
            Currency c = cp.Parse();

            Assert.AreEqual("Exalted Orb", c.Base);
        }

        [TestMethod]
        public void CurrencyParserReturnsCurrencyObjWithCorrectStackSize()
        {
            var cp = new CurrencyParser(PoEItemData.Currency.EXALTED_ORB);
            Currency c = cp.Parse();

            Assert.AreEqual(9, c.StackSize);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CurrencyParserThrowsFormatExceptionIfInputTextHasLessThanTwoSections()
        {
            string input = "foo";

            new CurrencyParser(input);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CurrencyParserParseThrowsFormatExceptionIfInfoSectionSizeIsLessThanTwo()
        {
            var cp = new CurrencyParser(PoEItemData.Currency.ORB_MISSING_INFO_FIELD);
            cp.Parse();
        }
    }
}
