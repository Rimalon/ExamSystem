using System;
using System.Threading;

namespace OffsetStructures
{
    public class ExamTable : IExamSystem
    {
        private readonly int NumberOfBuckets;
        public readonly OffsetList[] Content;

        public ExamTable(int numberOfBuckets)
        {
            NumberOfBuckets = numberOfBuckets;
            Content = new OffsetList[NumberOfBuckets];
            for (int i = 0; i < NumberOfBuckets; i++)
            {
                Content[i] = new OffsetList();
            }
        }

        public void Add(long studentId, long courseId)
        {
            Offset offset = new Offset(studentId, courseId);
            int numberOfBucket = HashFunction(offset);
            Monitor.Enter(Content[numberOfBucket]);
            if (!Content[numberOfBucket].Contains(offset))
            {
                Content[numberOfBucket].Add(offset);
            }
            Monitor.Exit(Content[numberOfBucket]);
        }

        public void Remove(long studentId, long courseId)
        {
            Offset offset = new Offset(studentId, courseId);
            int numberOfBucket = HashFunction(offset);
            Monitor.Enter(Content[numberOfBucket]);
            Content[numberOfBucket].Remove(offset);
            Monitor.Exit(Content[numberOfBucket]);
        }

        public bool Contains(long studentId, long courseId)
        {
            Offset offset = new Offset(studentId, courseId);
            int numberOfBucket = HashFunction(offset);
            bool result = false;
            Monitor.Enter(Content[numberOfBucket]);
            result = Content[numberOfBucket].Contains(offset);
            Monitor.Exit(Content[numberOfBucket]);
            return result;
        }

        private int HashFunction(Offset offset)
        {
            return Math.Abs(offset.GetHashCode() % NumberOfBuckets);
        }
    }
}
