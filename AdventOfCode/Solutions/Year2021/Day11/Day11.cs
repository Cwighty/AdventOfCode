using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day11 : ASolution
    {

        public Day11() : base(11, 2021, "dumbo octo")
        {

        }
        public int[,] Grid;
        public const int STEPS = 1000;
        int rows = 10;
        int cols = 10;
        public int flashCount = 0;
        protected override string SolvePartOne()
        {
            var input = Input.SplitByNewline();
            Grid = FormatInput(input);

            for (int step = 1; step <= STEPS; step++)
            {
                Stack<(int r, int c)> needsNeighborsFlashed = new();
                HashSet<(int r, int c)> flashed = new();

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        //Add 1 to every octo
                        Increment(r, c);
                    }
                }

                while (needsNeighborsFlashed.Count > 0)
                {
                    var cur = needsNeighborsFlashed.Pop();
                    for (int row = cur.r - 1; row <= cur.r + 1; row++)
                    {
                        for (int col = cur.c - 1; col <= cur.c + 1; col++)
                        {
                            if (row >= 0 && row < rows && col >= 0 && col < cols)
                            {
                                if (!flashed.Contains((row, col)))
                                    Increment(row, col);
                            }
                        }
                    }
                }
                flashCount += flashed.Count;
                // Print out every step

                // Console.WriteLine($"Step:{i}");
                // for (int r = 0; r < rows; r++)
                // {
                //     for (int c = 0; c < cols; c++)
                //     {
                //         if (Grid[r,c] == 0)
                //         {
                //             Console.ForegroundColor = ConsoleColor.Red;
                //             Console.Write(Grid[r, c]);
                //             Console.ResetColor();
                //         }
                //         else
                //         {
                //             Console.Write(Grid[r, c]);
                //         }
                //     }
                //     Console.WriteLine();
                // }
                // Console.WriteLine($"Flashes: {flashed.Count}");
                void Increment(int r, int c)
                {
                    Grid[r, c]++;
                    if (Grid[r, c] > 9)
                    {
                        Grid[r, c] = 0;
                        flashed.Add((r, c));
                        needsNeighborsFlashed.Push((r, c));
                    }
                }
                if (step == 100)
                {
                    return flashCount.ToString();
                }
            }
            return flashCount.ToString();
        }

        protected override string SolvePartTwo()
        {
            var input = Input.SplitByNewline();
            Grid = FormatInput(input);

            for (int i = 1; i <= STEPS; i++)
            {
                Stack<(int r, int c)> needsNeighborsFlashed = new();
                HashSet<(int r, int c)> flashed = new();

                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        //Add 1 to every octo
                        Increment(r, c);
                    }
                }

                while (needsNeighborsFlashed.Count > 0)
                {
                    var cur = needsNeighborsFlashed.Pop();
                    for (int row = cur.r - 1; row <= cur.r + 1; row++)
                    {
                        for (int col = cur.c - 1; col <= cur.c + 1; col++)
                        {
                            if (row >= 0 && row < rows && col >= 0 && col < cols)
                            {
                                if (!flashed.Contains((row, col)))
                                    Increment(row, col);
                            }
                        }
                    }
                }
                flashCount += flashed.Count;


                if (flashed.Count == rows * cols)
                {
                    //If all octos flash
                    return $"Step: {i}";
                }
                void Increment(int r, int c)
                {
                    Grid[r, c]++;
                    if (Grid[r, c] > 9)
                    {
                        Grid[r, c] = 0;
                        flashed.Add((r, c));
                        needsNeighborsFlashed.Push((r, c));
                    }
                }
            }
            return null;
        }

        public static int[,] FormatInput(string[] lines)
        {
            int rows = lines.Length;
            int cols = lines[0].Length;
            int[,] grid = new int[rows, cols];
            int row = 0;
            foreach (var line in lines)
            {
                int col = 0;
                foreach (var num in line.ToCharArray())
                {
                    grid[row, col] = Convert.ToInt32(num) - 48;
                    col++;
                }
                row++;
            }
            return grid;
        }

    }
}
