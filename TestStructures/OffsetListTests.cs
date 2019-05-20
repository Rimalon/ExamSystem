using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffsetStructures;

namespace TestStructures
{
    [TestClass]
    public class OffsetListTests
    {
        [TestMethod]
        public void TestOffsetList()
        {
            OffsetList listOffset = new OffsetList();
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    listOffset.Add(new Offset(i, j));
                }
            }


            for (int i = 0; i < 10; i += 2)
            {
                for (int j = 0; j < 5; j += 2)
                {
                    listOffset.Remove(new Offset(i, j));
                    listOffset.Remove(new Offset(10 + random.Next(100), random.Next(300)));
                }
            }

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        Assert.IsFalse(listOffset.Contains(new Offset(i, j)));
                    }
                    else
                    {
                        Assert.IsTrue(listOffset.Contains(new Offset(i, j)));
                    }
                }
            }

            Assert.AreEqual(listOffset.Length, 35);

        }

        [TestMethod]
        public void RemovesTest()
        {
            int numberOfElems = 10;
            OffsetList list = new OffsetList();
            for (int i = 0; i < numberOfElems; ++i)
            {
                list.Add(new Offset(i, i));
            }

            Assert.IsFalse(list.Remove(null));
            Assert.IsFalse(list.Remove(new Offset(numberOfElems, numberOfElems)));
            Assert.AreEqual(numberOfElems, list.Length);
            int numberOfDeletions = 0;
            for (int i = 0; i < numberOfElems; i += 2)
            {
                Assert.IsTrue(list.Remove(new Offset(i, i)));
                numberOfDeletions++;
            }
            Assert.AreEqual(numberOfElems - numberOfDeletions, list.Length);
            for (int i = 0; i < numberOfElems; i += 2)
            {
                Assert.IsFalse(list.Contains(new Offset(i, i)));
            }
        }
    }
}
