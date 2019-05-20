using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OffsetStructures;

namespace TestStructures
{
    [TestClass]
    public class ExamTableTests
    {
        [TestMethod]
        public void ExamTableTest()
        {
            ExamTable table = new ExamTable(50);
            List<Thread> threads = new List<Thread>();
            List<Offset> offsets = new List<Offset>();
            int numberOfThreads = 10;
            int numberOfAddThreads = numberOfThreads / 2;
            int numberOfRemoveThreads = numberOfThreads - numberOfAddThreads;
            int numberOfIdStudents = 50;
            int numberOfIdCourses = 20;
            int numberOfRemovedOffsets = 0;
            int removeValue = 5;

            for (int i = 0; i < numberOfIdStudents; ++i)
            {
                for (int j = 0; j < numberOfIdCourses; ++j)
                {
                    offsets.Add(new Offset(i, j));
                    if (i * j % removeValue == 0)
                    {
                        numberOfRemovedOffsets++;
                    }
                }
            }

            int addPartLenght = offsets.Count / numberOfAddThreads;

            for (int i = 0; i < numberOfAddThreads - 1; ++i)
            {
                var partOfAllOffsets = offsets.GetRange(i * addPartLenght, addPartLenght);
                threads.Add(new Thread(() => AddOffsets(partOfAllOffsets, table)));
                threads.Last().Start();
            }

            var lastPartOfAllOffsets = offsets.GetRange((numberOfAddThreads - 1) * addPartLenght, offsets.Count - (numberOfAddThreads - 1) * addPartLenght);
            threads.Add(new Thread(() => AddOffsets(lastPartOfAllOffsets, table)));
            threads.Last().Start();


            for (int i = 0; i < numberOfAddThreads; ++i)
            {
                threads[i].Join();
            }


            List<Offset> filteredList = offsets.FindAll(new Predicate<Offset>((o) => (o.studentId * o.courseId) % removeValue == 0));
            int partLength = filteredList.Count / numberOfRemoveThreads;

            for (int i = 0; i < numberOfRemoveThreads - 1; ++i)
            {
                var partOfFilteredList = filteredList.GetRange(i * partLength, partLength);
                threads.Add(new Thread(() => RemoveOffsets(partOfFilteredList, table)));
                threads.Last().Start();
            }
            var lastPartOfFilteredList = filteredList.GetRange((numberOfRemoveThreads - 1) * partLength, filteredList.Count - (numberOfRemoveThreads - 1) * partLength);
            threads.Add(new Thread(() => RemoveOffsets(filteredList, table)));
            threads.Last().Start();

            for (int i = numberOfAddThreads; i < threads.Count; ++i)
            {
                threads[i].Join();
            }

            for (int i = 0; i < numberOfIdStudents; ++i)
            {
                for (int j = 0; j < numberOfIdCourses; ++j)
                {
                    Assert.AreEqual(i * j % removeValue != 0, table.Contains(i, j));
                }
            }

            int numberOfOffsets = 0;
            foreach (var r in table.Content)
            {
                numberOfOffsets += r.Length;
            }
            
            Assert.AreEqual(numberOfIdStudents * numberOfIdCourses - numberOfRemovedOffsets, numberOfOffsets);
        }

        private void AddOffsets(List<Offset> offsets, ExamTable table)
        {
            foreach (var o in offsets)
            {
                table.Add(o.studentId, o.courseId);
            }
        }

        private void RemoveOffsets(List<Offset> offsets, ExamTable table)
        {
            foreach (var o in offsets)
            {
                table.Remove(o.studentId, o.courseId);
            }
        }
    }
}
