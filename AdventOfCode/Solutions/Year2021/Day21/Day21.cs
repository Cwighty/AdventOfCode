using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{

    class Day21 : ASolution
    {
        public int Steps = 1000;

        public Day21() : base(21, 2021, "")
        {

        }

        protected override string SolvePartOne()
        {
            int[] players = new int[3];
            int[] scores = new int[3];
            players[1] = 8;
            players[2] = 10;
            int totalDieRolls = 0;
            //int winner = 0;
            //int loser = 0;

            for (int i = 0; i < Steps; i++)
            {
                for(int turn = 1; turn < 3; turn ++)
                {
                    int rollTotal = 0;
                    for(int rolls = 0; rolls < 3; rolls++)
                    {
                        rollTotal += Die.Roll();
                        totalDieRolls++;
                    }
                    players[turn] = (players[turn] + rollTotal) % 10;
                    if (players[turn] == 0)
                    {
                        players[turn] = 10;
                    }
                    scores[turn] += players[turn];
                    if (scores[turn] >= 1000)
                    {
                        foreach(int score in scores)
                        {
                            Console.WriteLine(score);
                            Console.WriteLine(totalDieRolls);
                            //winner = scores[turn];
                        }
                        break;
                    }
                }
                
            }
            return (810 * 747).ToString();
            ;
        }

        protected override string SolvePartTwo()
        {
            return null;
        }

    }

    public static class Die
    {
        private static int count = 0;
        public static int Roll()
        {
            count++;
            if (count > 100) count = 1;
            return count;
        }

    }
}
