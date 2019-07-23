using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.ViewModels.Commands;

namespace PoEMarketLookupTests.ViewModels.Commands
{
    [TestClass]
    public class AsyncCommandTest
    {
        [TestMethod]
        public void CommandExecutesAction()
        {
            var value = 0;
            Task action() { value = 1; return Task.CompletedTask; };
            var command = new AsyncCommand(action);

            command.Execute(null);
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void CanExecuteReturnsValueFromSetFunctionIfNotNull()
        {
            var command = new AsyncCommand(null, () => false);

            Assert.IsFalse(command.CanExecute(null));
        }

        [TestMethod]
        public void InvokeCanExecuteChangedRaisesCanExecuteChangedEvent()
        {
            var changed = false;
            var command = new AsyncCommand(null);
            command.CanExecuteChanged += delegate { changed = true; };
            command.InvokeCanExecuteChanged();

            Assert.IsTrue(changed);
        }

        [TestMethod]
        public void IsExecutingIsTrueWhileCommandIsExecuting()
        {
            bool IsExecutingValue = false;
            AsyncCommand command = null;
            Task action() { IsExecutingValue = command.IsExecuting; return Task.CompletedTask; };
            command = new AsyncCommand(action);
            command.Execute(null);

            Assert.IsTrue(IsExecutingValue);
        }

        [TestMethod]
        public async Task IsExecutingIsFalseAfterExecutingACommand()
        {
            Task action() { return Task.CompletedTask; };
            var command = new AsyncCommand(action);
            await command.ExecuteAsync();

            Assert.IsFalse(command.IsExecuting);
        }

        [TestMethod]
        public async Task IsExecutingIsFalseAfterExecutingACommandAndCommandThrowsAnException()
        {
            Task action() { throw new Exception(); };
            var command = new AsyncCommand(action);

            try
            {
                await command.ExecuteAsync();
            }
            catch
            {
                Assert.IsFalse(command.IsExecuting);
            }
        }

        [TestMethod]
        public void CanExecuteIsFalseIfCommandIsExecuting()
        {
            bool CanExecute = true;
            AsyncCommand command = null;
            Task action() { CanExecute = command.CanExecute(null); return Task.CompletedTask; };
            command = new AsyncCommand(action);
            command.Execute(null);

            Assert.IsFalse(CanExecute);
        }

        [TestMethod]
        public async Task SettingIsExecutingFiresCanExecuteChangedEvent()
        {
            int changedCount = 0;
            var command = new AsyncCommand(() => { return Task.CompletedTask; });
            command.CanExecuteChanged += delegate { changedCount += 1; };
            await command.ExecuteAsync();

            Assert.AreEqual(2, changedCount);
        }
    }
}
