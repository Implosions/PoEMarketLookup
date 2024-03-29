﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoEMarketLookup.PoE.Items.Components;

namespace PoEMarketLookupTests.Parsing.Components
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

        [TestMethod]
        public void SocketGroupReturnsCorrectSocketCount()
        {
            var sg = SocketGroup.Parse("R R R");
            Assert.AreEqual(3, sg.Sockets);
        }

        [TestMethod]
        public void SocketGroupReturnsLinkArray()
        {
            var sg = SocketGroup.Parse("R-R-R R-R");
            CollectionAssert.AreEqual(new int[] { 3, 2 }, sg.Links);
        }

        [TestMethod]
        public void SocketGroupReturnsLargestLink()
        {
            var sg = SocketGroup.Parse("R R-R R-R-R");
            Assert.AreEqual(3, sg.LargestLink);
        }

        [TestMethod]
        public void SocketGroupCanParseAbyssalSockets()
        {
            var sg = SocketGroup.Parse("B-B-B A A");
            Assert.AreEqual(2, sg.AbyssalSockets);
            Assert.AreEqual(5, sg.Sockets);
            Assert.AreEqual(3, sg.LargestLink);
        }
    }
}
