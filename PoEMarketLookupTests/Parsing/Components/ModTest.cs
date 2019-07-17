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

        [TestMethod]
        public void ModParseCanCaptureDecimalValues()
        {
            string text = "foo 1.23 bar";
            Mod mod = Mod.Parse(text);

            Assert.AreEqual(1.23f, mod.AffixValues[0]);
        }

        [TestMethod]
        public void ModParseCanCaptureNegativeValues()
        {
            string text = "foo -1 bar";
            Mod mod = Mod.Parse(text);

            Assert.AreEqual(-1f, mod.AffixValues[0]);
        }

        [TestMethod]
        public void ModTypeIsCraftedIfModIsCrafted()
        {
            string text = "foo (crafted)";
            var mod = Mod.Parse(text);

            Assert.AreEqual(Mod.ModType.Crafted, mod.Type);
        }

        [TestMethod]
        public void ModAffixDoesNotContainCraftedSuffixIfCrafted()
        {
            string text = "foo (crafted)";
            var mod = Mod.Parse(text);

            Assert.AreEqual("foo", mod.Affix);
        }
    }
}
