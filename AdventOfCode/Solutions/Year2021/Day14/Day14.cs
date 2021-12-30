using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day14 : ASolution
    {
        public string start;
        public Dictionary<string, string> replacements = new();

        public Day14() : base(14, 2021, "")
        {
            var input = Input.SplitByNewline();
            start = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                var line = input[i].Split(" -> ");
                var target = line[0];
                var insert = line[1];
                replacements.Add(target, insert.ToString());
            }
        }

        protected override string SolvePartOne()
        {
            // First part ran fine storing the string for each step and then replacing each part,
            // but for the second part it grows too fast exponentially to be solvable past like 15 steps.
            int steps = 10;
            var polymers = new List<string>();
            for (int step = 0; step < steps; step++)
            {
                string newPoly = "";
                string inserts = "";
                for (int i = 0; i < start.Length - 1; i++)
                {
                    string curr = "";
                    for (int j = i; j < i + 2; j++)
                    {
                        curr += start[j];
                    }
                    inserts += replacements[curr];
                }
                for (int i = 0; i < start.Length || i < inserts.Length; i++)
                {
                    if (i < start.Length)
                        newPoly += start[i];

                    if (i < inserts.Length)
                        newPoly += inserts[i];
                }
                start = newPoly;
            polymers.Add(newPoly);
            }

            var sums = new List<(char, double)>();
            var last = polymers.Last().ToList();
            var lastUnique = last.Distinct();
            foreach( var c in last)
            {
                sums.Add((c, last.Count(x => x == c)));
            }
            double max = sums.Max(x => x.Item2);
            double min = sums.Min(x => x.Item2);
            return (max - min).ToString();
        }

        protected override string SolvePartTwo()
        {
            // We have to do it like the other day where instead of storing every item, we store a count of each item
            // and increment based on the previous amount of those polymer items.
            // I first tried with two lists, one for polymers and one for counts, if the list didnt contain the poly, it would add it.
            // I eventually went with dictionaries after I saw someone use the GetValueOrDefault. I didn't realize before that if you
            // access a dictionary term that doesnt exsist it will add that item, that makes it a lot simpler than the !Contains() on a list
            // The dictionary is good for when you don't want to repeat any items as well.
            int steps = 40;
            var polymers = new Dictionary<string, double>();
            for (var i = 0; i < start.Length - 1; i++)
            {
                var poly = start[i..(i + 2)]; // Looking through solutions on reddit, this is a quick replacement for the nested for loop I used before to build the 2 char poly. Cool!
                polymers[poly] = polymers.GetValueOrDefault(poly, 0) + 1;
            }
            var chars = new Dictionary<char, double>();
            for (var i = 0; i < start.Length; i++)
            {
                chars[start[i]] = chars.GetValueOrDefault(start[i], 0) + 1;
            }
            for (int step = 0; step < steps; step++)
            {
                var newPolymers = new Dictionary<string, double>();
                foreach (var (poly, count) in polymers)
                {
                    var replacement = replacements[poly];

                    chars[replacement[0]] = chars.GetValueOrDefault(replacement[0], 0) + count;

                    var left = poly[0] + replacement;
                    newPolymers[left] = newPolymers.GetValueOrDefault(left, 0) + count;

                    var right = replacement + poly[1];
                    newPolymers[right] = newPolymers.GetValueOrDefault(right, 0) + count;
                }
                polymers = newPolymers;
            }
            return( chars.Max(x => x.Value) - chars.Min(x => x.Value)).ToString();
        }
    }
}
