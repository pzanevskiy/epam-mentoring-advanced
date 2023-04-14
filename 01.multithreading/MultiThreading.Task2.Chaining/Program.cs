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

            var firstTask = Task.Factory.StartNew(() =>
            {
                var random = new Random();
                var array = new int[10];

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = random.Next(0, 100);
                }

                Console.WriteLine("First task: {0}", string.Join(", ", array));
                return array;
            });

            var secondTask = firstTask.ContinueWith(prevTask =>
            {
                var random = new Random();
                var multiplier = random.Next(0, 10);
                var array = prevTask.Result;

                for (int i = 0; i < array.Length; i++)
                {
                    array[i] *= multiplier;
                }

                Console.WriteLine("Second task: {0}", string.Join(", ", array));
                return array;
            });

            var thirdTask = secondTask.ContinueWith(prevTask =>
            {
                var array = prevTask.Result;
                Array.Sort(array);

                Console.WriteLine("Third task: {0}", string.Join(", ", array));
                return array;
            });

            var fourthTask = thirdTask.ContinueWith(prevTask =>
            {
                var array = prevTask.Result;
                var average = array.Average();

                Console.WriteLine("Fourth task: {0}", average);
            });

            fourthTask.Wait();

            Console.ReadKey();
        }
    }
}
