using System;

namespace AdventOfCode.Days
{
    public class DayOne : DayBase
    {

        public override void Run()
        {
            var output = this.WhichFloor(this.InputFile);
            Console.WriteLine("Santa reached basement at {0} instruction.\nbut finally end up on {1} floor", output.Item1, output.Item2);
        }

        private Tuple<int, int> WhichFloor(string input)
        {
            int floor = 0;
            int position = 0;
            bool basementReached = false;

            foreach (var @char in input)
            {
                if (@char == '(') floor++;
                else floor--;

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
