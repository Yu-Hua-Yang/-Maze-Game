using Maze;
using MazeHuntKill;
using MazeRecursion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public class Benchmark
    {
        public static void Main(string[] args)
        {
            IMapProvider recursiveMaze = MazeCreationRecursive.CreateMazeRecursion(1);
            IMapProvider recursiveV2 = MazeCreationRecursive.CreateMazeRecursion(2);
            IMapProvider huntKillMaze = MazeCreationHuntKill.CreateMazeHuntKill();

            Console.WriteLine("Enter the path in which you would like to save results");
            string path = Console.ReadLine();

            Console.WriteLine("What size map would you like the benchmark to stop at");
            int maxSize = GetIntegerInput();

            Console.WriteLine("In what increment would you like to go up by");
            int increments = GetIntegerInput();

            var recursiveTask = Task.Run(() => RunGeneration(recursiveMaze, "Recursive", path, MaximumInput(maxSize), increments));
            var recursiveV2Task = Task.Run(() => RunGeneration(recursiveV2, "RecursiveV2", path, MaximumInput(maxSize), increments));
            var huntKillTask = Task.Run(() => RunGeneration(huntKillMaze, "Hunt Kill", path, MaximumInput(maxSize), increments));

            Task.WaitAll(recursiveTask, huntKillTask, recursiveV2Task);
            Console.WriteLine("Maze generation tasks completed.");
        }

        public static void RunGeneration(IMapProvider mapProvider, string providerType, string path, int biggestSize, int steps)
        {
            Tool tool = new Tool();
            StringBuilder sb = new StringBuilder("Size,Milliseconds\n");

            for (int size = 0; size < biggestSize; size ++)
            {
                size += steps;
                if (size % 2 == 0)
                {
                    size++;
                }

                TimeSpan timeSpan = tool.TimeIt(() => mapProvider.CreateMap(size, size));

                sb.AppendLine($"{size},{timeSpan.TotalMilliseconds}");
                Console.WriteLine($"{providerType} Size: {size}, Took: {timeSpan.TotalMilliseconds}ms");
            }

            tool.WriteToFile($"{path}{providerType}.csv", sb.ToString());
        }

        private static int GetIntegerInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result > 0)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                }
            }
        }

        private static int MaximumInput(int input)
        {
            return Math.Min(input, 220);
        }
    }
}