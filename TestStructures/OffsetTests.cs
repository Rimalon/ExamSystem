using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffsetStructures;

namespace TestStructures
{
    [TestClass]
    public class OffsetTests
    {
        [TestMethod]
        public void TestOffsets()
        {
            Offset nullOffset = null;
            var offset1 = new Offset(1, 1);
            Assert.AreEqual(offset1, new Offset(1, 1));
            Assert.AreEqual(null, nullOffset);
            Assert.AreEqual(nullOffset, null);
            Assert.IsTrue(offset1 == new Offset(1, 1));
            Assert.IsTrue(null == nullOffset);
            Assert.IsTrue(nullOffset == null);
            Assert.IsTrue(offset1 != null);
            Assert.IsTrue(null != offset1);
        }
    }
}
