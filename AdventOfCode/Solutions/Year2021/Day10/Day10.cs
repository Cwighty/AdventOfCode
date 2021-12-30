using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2021
{
    class Day10 : ASolution
    {
        public static List<char> OpeningChars = new List<char>() { '{', '[', '<', '(' };
        public static List<char> ClosingChars = new List<char>() { '}', ']', '>', ')' };

        public Day10() : base(10, 2021, "Syntax Scoring")
        {

        }

        protected override string SolvePartOne()
        {
            var lines = Input.SplitByNewline().ToList();
            List<char> corruptedChars = GetCorruptedChars(lines);
            var score = CalculateSytaxErrorScore(corruptedChars);
            return score.ToString();
        }

        protected override string SolvePartTwo()
        {
            var lines = Input.SplitByNewline().ToList();
            var incompleteLines = GetIncompleteLines(lines);
            List<string> completedStrings = FixIncompleteLines(incompleteLines);
            var autoCompleteScores = CalculateAutoCompleteScore(completedStrings);
            autoCompleteScores.Sort();
            var mid = autoCompleteScores.Count / 2;
            return autoCompleteScores[mid].ToString();
        }

        private static List<double> CalculateAutoCompleteScore(List<string> completedStrings)
        {
            var scores = new List<double>();
            foreach (var s in completedStrings)
            {
                double score = 0;
                foreach (var c in s)
                {
                    score *= 5;
                    switch (c)
                    {
                        case ')':
                            score += 1;
                            break;
                        case ']':
                            score += 2;
                            break;
                        case '}':
                            score += 3;
                            break;
                        case '>':
                            score += 4;
                            break;
                    }
                }
                scores.Add(score);
            }
            return scores;
        }

        private static List<string> FixIncompleteLines(List<string> lines)
        {
            var stack = new Stack<char>();
            var closingStrings = new List<string>();
            foreach (var line in lines)
            {
                var terminatingString = "";
                foreach (var c in line)
                {
                    if (OpeningChars.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                while (stack.Count != 0)
                {
                    char top = stack.Pop();
                    switch (top)
                    {
                        case '(':
                            terminatingString += ")";
                            break;
                        case '[':
                            terminatingString += "]";
                            break;
                        case '{':
                            terminatingString += "}";
                            break;
                        case '<':
                            terminatingString += ">";
                            break;
                    }
                }
                closingStrings.Add(terminatingString);
            }
            return closingStrings;
        }

        private static object CalculateSytaxErrorScore(List<char> corruptedChars)
        {
            int score = 0;
            foreach (var c in corruptedChars)
            {
                switch (c)
                {
                    case ')':
                        score += 3;
                        break;
                    case ']':
                        score += 57;
                        break;
                    case '}':
                        score += 1197;
                        break;
                    case '>':
                        score += 25137;
                        break;
                }
            }
            return score;
        }

        private static List<char> GetCorruptedChars(List<string> corruptedLines)
        { // First corrupted sytax on each line
            var corrupted = new List<char>();
            var stack = new Stack<char>();
            var newCorruptedLines = new List<string>();
            foreach (var line in corruptedLines)
            {
                foreach (var c in line)
                {
                    if (OpeningChars.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        if (IsPair(stack.Peek(), c))
                        {
                            stack.Pop();
                        }
                        else
                        {
                            corrupted.Add(c);
                            newCorruptedLines.Add(line);
                            break;
                        }
                    }
                }
            }

            return corrupted;
        }


        private static bool IsPair(char opening, char closing)
        {
            if ((opening == '(' && closing == ')') | (opening == '[' && closing == ']') | (opening == '<' && closing == '>') | (opening == '{' && closing == '}'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<string> GetIncompleteLines(List<string> lines)
        { // First corrupted sytax on each line
            var stack = new Stack<char>();
            var corruptedLines = new List<string>();

            foreach (var line in lines)
            {
                foreach (var c in line)
                {
                    if (OpeningChars.Contains(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        if (IsPair(stack.Peek(), c))
                        {
                            stack.Pop();
                        }
                        else
                        {
                            corruptedLines.Add(line);
                            break;
                        }
                    }
                }
            }

            return lines.Except(corruptedLines).ToList();
        }
    }
}

