/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private const int StateNumber = 10;
        private static readonly Semaphore _semaphore = new(3, 3);

        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and _semaphore for waiting threads.");

            Console.WriteLine();

            ProcThread(StateNumber);
            ProcThreadPool(StateNumber);

            //Console.ReadKey();
        }

        private static void ProcThread(object obj)
        {
            var number = (int)obj;
            Console.WriteLine("Threads - " + number);
            if (number > 1)
            {
                var thread = new Thread(ProcThread);
                thread.Start(--number);
                thread.Join();
            }
        }

        private static void ProcThreadPool(object obj)
        {
            var number = (int)obj;
            _semaphore.WaitOne();
            Console.WriteLine("Thread pool - " + number);
            if (number > 1)
            {
                ThreadPool.QueueUserWorkItem(ProcThreadPool, --number);
                _semaphore.Release();
            }
        }

    }
}
