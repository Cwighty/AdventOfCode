using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day01 : ASolution
    {
        public static List<char> OpeningChars = new List<char>() { '{', '[', '<', '(' };
        public static List<char> ClosingChars = new List<char>() { '}', ']', '>', ')' };

        public Day01() : base(01, 2021, "")
        {

        }

        protected override string SolvePartOne()
        {
            
            return null;
        }

        protected override string SolvePartTwo()
        {
            var nums = Input.SplitByNewline().ToIntArray().ToList();
            var count = CountIncreases(WindowSums(nums));
            return count.ToString();
        }


        public static int CountIncreases(List<int> nums)
        {
            int curr;
            int prev;
            int count = 0;
            int i = 0;
            while (i < nums.Count)
            {
                try
                {
                    curr = nums[i];
                    prev = nums[i - 1];
                }
                catch
                {
                    curr = 0;
                    prev = 0;
                }

                if (curr > prev)
                {
                    count++;
                }
                i++;
            }
            return count;
        }

        public static List<int> WindowSums(List<int> nums)
        {
            var sums = new List<int>();
            for (int i = 0; i < nums.Count; i++)
            {
                try
                {
                    var sum = nums[i] + nums[i + 1] + nums[i + 2];
                    sums.Add(sum);
                }
                catch { }
            }
            return sums;
        }
    }
}
