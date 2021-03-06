﻿using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public class DayFive : DayBase
    {
        private static readonly char[] Vowels = {'a', 'e', 'i', 'o', 'u'};
        private static readonly string[] Banned = {"ab", "cd", "pq", "xy"};

        public override void Run()
        {
            var niceStringsA = 0;
            var niceStringsB = 0;

            foreach (var line in this.InputFile)
            {
                if (PartOne(line)) { niceStringsA++; }
                if (PartTwo(line)) { niceStringsB++; }
            }

            Console.WriteLine("[Part A] Nice Strings: " + niceStringsA);
            Console.WriteLine("[Part B] Nice Strings: " + niceStringsB);
        }

        private static bool PartOne(string line)
        {
            var noBanned = true;
            var vowelCount = 0;
            var doubleLetter = false;

            if (Vowels.Contains(line[0])) { vowelCount++; }

            for (var i = 1; i < line.Length; i++)
            {
                var prevChar = line[i - 1];
                var thisChar = line[i];

                if (Vowels.Contains(thisChar))
                {
                    vowelCount++;
                }

                if (!doubleLetter && prevChar == thisChar)
                {
                    doubleLetter = true;
                }

                if (Banned.Contains(new string(new[] {prevChar, thisChar})))
                {
                    noBanned = false;
                    break;
                }
            }

            return doubleLetter && noBanned && vowelCount > 2;
        }

        private static bool PartTwo(string line)
        {
            var pairOfTwo = false;
            var twoSameWithOneOffset = false;

            for (var i = 0; i < line.Length - 3; i++)
            {
                var partA = line.Substring(i, 2);
                var partB = line.Substring(i + 2, line.Length - (2 + i));

                if (partB.Contains(partA))
                {
                    pairOfTwo = true;
                    break;
                }
            }

            for (var i = 2; i < line.Length; i++)
            {
                var prevChar = line[i - 2];
                var thisChar = line[i];

                if (prevChar == thisChar)
                {
                    twoSameWithOneOffset = true;
                    break;
                }
            }

            return pairOfTwo && twoSameWithOneOffset;
        }
    }
}