using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookupTests.Parsing.Components
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

        [TestMethod]
        public void ModParseReturnsModObjectWithCorrectAffixNumericValues()
        {
            string text = "Adds 1 to 2 damage";
            Mod mod = Mod.Parse(text);

            CollectionAssert.AreEqual(new float[] { 1, 2 }, mod.AffixValues);
        }

        [TestMethod]
        public void ModToStringReturnsOriginalAffixString()
        {
            string text = "Adds 1 to 2 damage";
            Mod mod = Mod.Parse(text);

            Assert.AreEqual(text, mod.ToString());
        }
    }
}
