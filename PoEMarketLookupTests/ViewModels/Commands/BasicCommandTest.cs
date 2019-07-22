using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.ViewModels.Commands;

namespace PoEMarketLookupTests.ViewModels.Commands
{
    [TestClass]
    public class BasicCommandTest
    {
        [TestMethod]
        public void CommandExecutesAction()
        {
            var value = 0;
            Action action = delegate
            {
                value = 1;
            };
            var command = new BasicCommand(action);

            command.Execute(null);
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void CanExecuteReturnsValueFromSetFunctionIfNotNull()
        {
            var command = new BasicCommand(null, () => false);

            Assert.IsFalse(command.CanExecute(null));
        }
    }
}
