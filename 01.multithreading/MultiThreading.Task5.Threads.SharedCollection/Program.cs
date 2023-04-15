/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static readonly ConcurrentQueue<int> _sharedCollection = new ConcurrentQueue<int>();
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var addingTask = AddingTask();
            var printingTask = PrintingTask();

            Task.WaitAll(addingTask, printingTask);

            Console.ReadKey();
        }

        private static Task PrintingTask()
        {
            return Task.Factory.StartNew(() =>
            {
                while (_sharedCollection.Count < 10)
                {
                    _semaphore.Wait();
                    Console.WriteLine("Printing task prints - {0}.", string.Join(", ", _sharedCollection));
                    _semaphore.Release();
                    Task.Delay(100).Wait();
                }
            });
        }

        private static Task AddingTask()
        {
            return Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    _semaphore.Wait();
                    _sharedCollection.Enqueue(i);
                    Console.WriteLine("Adding tasks adds - {0}.", i);
                    _semaphore.Release();
                    Task.Delay(100).Wait();
                }
            });
        }
    }
}
