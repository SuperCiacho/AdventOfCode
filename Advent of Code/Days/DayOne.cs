using System;

namespace AdventOfCode.Days
{
    public class DayOne : DayBase
    {
        public override void Run()
        {
            var output = this.WhichFloor(this.InputFile[0]);
            Console.WriteLine("Santa reached basement at {0} instruction.\nbut finally end up on {1} floor", output.Item1, output.Item2);
        }

        private Tuple<int, int> WhichFloor(string input)
        {
            var floor = 0;
            var position = 0;
            var basementReached = false;

            foreach (var @char in input)
            {
                if (@char == '(') { floor++; }
                else
                { floor--; }

                if (!basementReached)
                {
                    position++;
                    basementReached = floor == -1;
                }
            }

            return new Tuple<int, int>(position, floor);
        }
    }
}