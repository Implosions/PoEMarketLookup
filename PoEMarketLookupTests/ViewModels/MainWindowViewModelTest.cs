﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.ViewModels;

namespace PoEMarketLookupTests.ViewModels
{
    [TestClass]
    public class MainWindowViewModelTest
    {
        private class MockViewModel : MainWindowViewModel
        {
            protected override string GetClipboard()
            {
                return "foo bar";
            }
        }

        [TestMethod]
        public void PasteClipboardCommandSetsItemTextToClipboardValue()
        {
            var vm = new MockViewModel();
            vm.PasteFromClipboardCommand.Execute(null);

            Assert.AreEqual("foo bar", vm.ItemText);
        }

        [TestMethod]
        public void SettingItemTextFiresPropertyChangedEvent()
        {
            var propertyChanged = false;
            var vm = new MockViewModel();
            vm.PropertyChanged += delegate { propertyChanged = true; };
            vm.ItemText = "foo";

            Assert.IsTrue(propertyChanged);
        }
    }
}