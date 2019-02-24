﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookupTests
{
    [TestClass]
    public class SocketGroupTest
    {
        [TestMethod]
        public void SocketGroupParseReturnsSocketGroupObjWithCorrectSockets()
        {
            var sg = SocketGroup.Parse("R R B G W W W");
            Assert.AreEqual(2, sg.RedSockets);
            Assert.AreEqual(1, sg.GreenSockets);
            Assert.AreEqual(1, sg.BlueSockets);
            Assert.AreEqual(3, sg.WhiteSockets);
        }
    }
}
