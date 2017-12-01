using System;
using AdventOfCode.Days;

namespace AdventOfCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // http://adventofcode.com/
            IRunner codeDay = DayRunner.GetSpecificDay(9);
            codeDay.Run();
            Console.ReadLine();
        }
    }
}