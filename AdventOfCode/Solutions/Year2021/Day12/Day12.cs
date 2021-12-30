using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    partial class Day12 : ASolution
    {
        public Dictionary<string, List<(string, bool)>> graph;
        public List<string> visited = new List<string>();


        public Day12() : base(12, 2021, "", true)
        {
            var input = Input.SplitByNewline();
            graph = new();
            foreach (var line in input)
            {
                var nodes = line.Split('-');
                graph.TryAdd(nodes[0], new List<(string, bool)>());
                graph.TryAdd(nodes[1], new List<(string, bool)>());
            }
            foreach (var line in input)
            {
                var nodes = line.Split('-');
                var left = nodes[0];
                var right = nodes[1];
                if (left == "start" || right == "end")
                {
                    graph[left].Add((right, IsSmallCave(right)));
                }
                else
                {
                    graph[left].Add((right, IsSmallCave(right)));
                    graph[right].Add((left, IsSmallCave(left)));
                }

            }

        }

        private bool IsSmallCave(string right)
        {
            return right == right.ToLower();
        }

        protected override string SolvePartOne()
        {
            var path = new List<string>();
            var smallVisited = new List<string>();
            var q = new Queue<List<string>>();
            var smallQ = new Queue<List<string>>();
            var nodes = graph.Keys.ToList();
            path.Add(nodes[0]);
            q.Enqueue(path);

            //visited.Add(nodes[0]);
            while (q.Count > 0)
            {
                path = q.Dequeue();
                //smallVisited = smallQ.Dequeue();
                if (path.Last() == "end")
                {
                    foreach (var n in path)
                    {
                        Console.Write(n + " -> ");
                    }
                    Console.WriteLine();
                }
                var adjacent = graph[path.Last()];
                foreach (var n in adjacent)
                {
                    if (!smallVisited.Contains(n.Item1))
                    {
                        //var newSmall = new List<string>(smallVisited);
                        var newPath = new List<string>(path);
                        if (IsSmallCave(n.Item1))
                        {
                            smallVisited.Add(n.Item1);
                        }
                        newPath.Add(n.Item1);
                        q.Enqueue(newPath);
                        //smallQ.Enqueue(newSmall);
                    }
                }
            }

            return null;
        }

        protected override string SolvePartTwo()
        {
            return null;
        }
    }
}
