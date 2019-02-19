using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup;

namespace PoEMarketLookupTests
{
    [TestClass]
    public class ModTest
    {
        [TestMethod]
        public void ModParseReturnsModObjectWithItsParsedAffix()
        {
            string text = "foo";
            Mod mod = Mod.Parse(text);

            Assert.AreEqual(text, mod.Affix);
        }

        [TestMethod]
        public void ModParseReturnsModObjectWithAffixNumericValuesReplacedWithPlaceholder()
        {
            string text = "foo 2 bar";
            Mod mod = Mod.Parse(text);

            Assert.AreEqual("foo # bar", mod.Affix);
        }
    }
}
