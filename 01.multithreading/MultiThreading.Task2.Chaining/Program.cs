/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var task = Task.Factory
                .StartNew(GenerateArray)
                .ContinueWith(MultiplyArray)
                .ContinueWith(SortArray)
                .ContinueWith(GetAverageFromArray);

            task.Wait();

            Console.ReadKey();
        }

        #region Methods

        private static void GetAverageFromArray(Task<int[]> task)
        {
            var array = task.Result;
            var average = array.Average();

            Console.WriteLine("Fourth task: {0}", average);
        }

        private static int[] SortArray(Task<int[]> prevTask)
        {
            var array = prevTask.Result;
            Array.Sort(array);

            Console.WriteLine("Third task: {0}", string.Join(", ", array));
            return array;
        }

        private static int[] MultiplyArray(Task<int[]> task)
        {
            var random = new Random();
            var multiplier = random.Next(0, 10);
            var array = task.Result;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] *= multiplier;
            }

            Console.WriteLine("Second task: {0}", string.Join(", ", array));
            return array;
        }

        private static int[] GenerateArray()
        {
            var random = new Random();
            var array = new int[10];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, 100);
            }

            Console.WriteLine("First task: {0}", string.Join(", ", array));
            return array;
        }

        #endregion
    }
}
