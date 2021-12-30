using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day13 : ASolution
    {
        const int PAPER_SIZE = 1400;
        bool[,] paper = new bool[PAPER_SIZE, PAPER_SIZE];
        List<(string, int)> foldPoints = new();

        public Day13() : base(13, 2021, "")
        {
            var lines = Input.SplitByNewline();
            foreach (var line in lines)
            {
                if (line[0] != 'f')
                {
                    var points = line.Split(',').ToIntArray();
                    var x = points[0];
                    var y = points[1];
                    paper[y, x] = true;
                }
                else
                {
                    var fold = line.Split();
                    var fpoint = fold[2].Split('=');
                    foldPoints.Add((fpoint[0], Convert.ToInt32(fpoint[1])));
                }
            }

            //PrintPaper(paper);
        }

        protected override string SolvePartOne()
        {

            int foldPoint = foldPoints[0].Item2;
            string foldAxis = foldPoints[0].Item1;
            for (int y = 0; y < PAPER_SIZE; y++)
            {
                for (int x = 0; x < PAPER_SIZE; x++)
                {
                    if (foldAxis == "y")
                    {
                        if (paper[y, x] && y > foldPoint)
                        {
                            paper[foldPoint - (y - foldPoint), x] = true;
                            paper[y, x] = false;
                        }
                    }
                    else
                    {
                        if (paper[y, x] && x > foldPoint)
                        {
                            paper[y, foldPoint - (x - foldPoint)] = true;
                            paper[y, x] = false;
                        }
                    }
                }
                //Console.WriteLine();
            }

            //PrintPaper(paper);

            return CountPoints(paper).ToString();
        }

        private int CountPoints(bool[,] newPaper)
        {
            int count = 0;
            for (int y = 0; y < PAPER_SIZE; y++)
            {
                for (int x = 0; x < PAPER_SIZE; x++)
                {
                    if (paper[y, x])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        protected override string SolvePartTwo()
        {
            foreach (var fold in foldPoints)
            {
                int foldPoint = fold.Item2;
                string foldAxis = fold.Item1;
                for (int y = 0; y < PAPER_SIZE; y++)
                {
                    for (int x = 0; x < PAPER_SIZE; x++)
                    {
                        if (foldAxis == "y")
                        {
                            if (paper[y, x] && y > foldPoint)
                            {
                                paper[foldPoint - (y - foldPoint), x] = true;
                                paper[y, x] = false;
                            }
                        }
                        else
                        {
                            if (paper[y, x] && x > foldPoint)
                            {
                                paper[y, foldPoint - (x - foldPoint)] = true;
                                paper[y, x] = false;
                            }
                        }
                    }
                    //Console.WriteLine();
                }
            }
            PrintPaper(paper, 20);
            return "REUPUPKR";
        }

        public void PrintPaper(bool[,] paper, int size)
        {
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size * 2; x++)
                {
                    if (paper[y, x])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
