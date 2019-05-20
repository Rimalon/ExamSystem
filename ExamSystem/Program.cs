using System;
using System.Collections.Generic;
using System.Threading;
using OffsetStructures;

namespace ExamSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new ExamTable(1000);
            var list = new ExamList();
            int numberOfMoves = 5000;
            int numberOfThreads = 100;
            TimeSpan time = WorkingTime(table, numberOfMoves, numberOfThreads);
            Console.WriteLine("{0} moves on {1} threads with {2} took {3} time", numberOfMoves, numberOfThreads, table.ToString(), time);
            time = WorkingTime(list, numberOfMoves, numberOfThreads);
            Console.WriteLine("{0} moves on {1} threads with {2} took {3} time", numberOfMoves, numberOfThreads, list.ToString(), time);
            Console.ReadKey();
        }

        static TimeSpan WorkingTime(IExamSystem system, int numberOfMoves, int numberOfThreads, int numberOfStudents = 1000, int numberOfCourses = 5)
        {

            int numberOfMovesForThread = numberOfMoves / numberOfThreads;

            List<Thread> threads = new List<Thread>(numberOfThreads);

            for (int i = 0; i < numberOfThreads; ++i)
            {
                threads.Add(new Thread(() => WorkIteration(system, numberOfMovesForThread, numberOfStudents, numberOfCourses)));
            }

            DateTime startTime = DateTime.Now;

            foreach (var t in threads)
            {
                t.Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }

            return DateTime.Now - startTime;
        }

        static void WorkIteration(IExamSystem system, int numberOfMoves, int numberOfStudents, int numberOfCourses)
        {
            int numberOfCotainsMoves = Convert.ToInt32(0.9 * numberOfMoves);
            int numberOfAdditions = Convert.ToInt32(0.09 * numberOfMoves);
            int numberOfDeletions = Convert.ToInt32(0.01 * numberOfMoves);
            Random random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < numberOfAdditions; i++)
            {
                system.Add(random.Next(numberOfMoves), random.Next(numberOfCourses));
            }

            for (int i = 0; i < numberOfCotainsMoves; i++)
            {
                system.Contains(random.Next(numberOfMoves), random.Next(numberOfCourses));
            }
            
            for (int i = 0; i < numberOfDeletions; i++)
            {
                system.Remove(random.Next(numberOfMoves), random.Next(numberOfCourses));
            }
        }
    }
}
