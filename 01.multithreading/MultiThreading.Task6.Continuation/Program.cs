/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var key = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (key)
            {
                case 'a':
                    {
                        var task = Task.Run(() =>
                        {
                            Console.WriteLine("Parent task execution.");
                            return 1;
                        });
                        var continuation = task.ContinueWith(t =>
                            Console.WriteLine("Continuation task execution. Status: {0}", t.Status));

                        await continuation;
                        break;
                    }
                case 'b':
                    {
                        var task = Task.Run(() =>
                        {
                            Console.WriteLine("Parent task execution.");
                            //return Task.FromException(new Exception("Custom exception."));
                            return Task.FromCanceled(new CancellationToken(true));
                        });

                        await task.ContinueWith(t =>
                                Console.WriteLine("Continuation task execution. Status: {0}", t.Status),
                            TaskContinuationOptions.NotOnRanToCompletion);

                        break;
                    }
                case 'c':
                    {
                        var task = Task.Run(() =>
                        {
                            Console.WriteLine("Parent task execution.");
                            return Task.FromException(new Exception("Custom exception."));
                        });
                        await task.ContinueWith(
                            t => Console.WriteLine("Continuation task execution. Status: {0}", t.Status),
                            TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                        break;
                    }
                case 'd':
                    {
                        var task = Task.Run(() =>
                        {
                            Console.WriteLine("Parent task execution.");
                            return Task.FromCanceled(new CancellationToken(true));
                        });
                        await task.ContinueWith(
                            t => Console.WriteLine("Continuation task execution. Status: {0}", t.Status),
                            TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Wrong key.");
                        break;
                    }
            }
        }
    }
}
