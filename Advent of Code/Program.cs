using System;
using AdventOfCode.Days;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            // http://adventofcode.com/
            IRunner codeDay;
            // codeDay = new DayOne();
            // codeDay = new DayTwo();
            // codeDay = new DayThree();
            // codeDay = new DayFour();
             codeDay = new DayFive();
            //codeDay = new DaySix();

            codeDay.Run();
            Console.ReadLine();
        }
    }
}
