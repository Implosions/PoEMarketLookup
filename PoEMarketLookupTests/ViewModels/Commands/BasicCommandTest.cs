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
        public void CommandFiresCanExecuteChangedEventWhenCanExecuteIsChanged()
        {
            var changed = false;
            var command = new BasicCommand(null);
            command.CanExecuteChanged += delegate { changed = true; };
            command.SetCanExecute(false);
            
            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void CommandDoesNotFireCanExecuteChangedEventWhenCanExecuteIsSetToTheSameValue()
        {
            var changed = false;
            var command = new BasicCommand(null);
            command.CanExecuteChanged += delegate { changed = true; };
            command.SetCanExecute(true);

            Assert.IsFalse(changed);
        }
    }
}
