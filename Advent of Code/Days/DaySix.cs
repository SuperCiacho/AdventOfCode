using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal class DaySix : DayBase
    {
        private readonly List<List<Light>> grid;

        public DaySix()
        {
            this.grid = new List<List<Light>>(1000);

            foreach (var i in Enumerable.Range(0, 1000))
            {
                this.grid.Add(new List<Light>(1000));

                foreach (var j in Enumerable.Range(0, 1000))
                {
                    this.grid[i].Add(new Light(i, j));
                }
            }
        }

        public override void Run()
        {
            foreach (var instruction in this.InputFile.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(Instruction.Parse))
            {
                for (var i = instruction.StartLocation.X; i <= instruction.EndLocation.X; i++)
                {
                    for (var j = instruction.StartLocation.Y; j <= instruction.EndLocation.Y; j++)
                    {
                        var light = this.grid[i][j];
                        instruction.Action(light);
                    }
                }
            }

            Console.WriteLine("Number of litted lights: " + this.grid.SelectMany(x => x).Count(l => l.IsLit));
            Console.WriteLine($"Brightness: { this.grid.SelectMany(x => x).Sum(l => l.Brightness)}");
        }

        private class Instruction
        {
            public Action<Light> Action { get; private set; }
            public Location StartLocation { get; private set; }
            public Location EndLocation { get; private set; }

            public static Instruction Parse(string serialized)
            {
                var instruction = new Instruction();

                var parts = serialized.Split(' ');

                if (parts.Length == 4)
                {
                    instruction.Action = l => l.Toggle();
                    instruction.Action += l => l.Brightness = l.Brightness + 2;
                    instruction.StartLocation = Location.Parse(parts[1]);
                    instruction.EndLocation = Location.Parse(parts[3]);
                }
                else
                {
                    if (parts[1] == "on")
                    {
                        instruction.Action = l => l.IsLit = true;
                        instruction.Action += l => ++l.Brightness;
                    }
                    else
                    {
                        instruction.Action = l => l.IsLit = false;
                        instruction.Action += l => l.Brightness = l.Brightness == 0 ? 0 : --l.Brightness;
                    }

                    instruction.StartLocation = Location.Parse(parts[2]);
                    instruction.EndLocation = Location.Parse(parts[4]);
                }

                return instruction;
            }
        }

        private class Light : IComparable<Light>
        {
            public Light(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; }
            public int Y { get; }
            public bool IsLit { get; set; }

            public int Brightness { get; set; }

            #region Implementation of IComparable<in Location>

            public int CompareTo(Light other)
            {
                if (this.X == other.X && this.Y == other.Y)
                {
                    return 0;
                }

                if (this.Y > other.Y)
                {
                    return 1;
                }
                return -1;
            }

            #endregion

            public void Toggle()
            {
                this.IsLit = !this.IsLit;
            }
        }

        private struct Location : IComparable<Location>
        {
            public int X { get; }
            public int Y { get; }

            public Location(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static Location Parse(string serialized)
            {
                var parts = serialized.Split(',');
                return new Location(int.Parse(parts[0]), int.Parse(parts[1]));
            }

            #region Implementation of IComparable<in Location>

            public int CompareTo(Location other)
            {
                if (this.X == other.X && this.Y == other.Y)
                {
                    return 0;
                }

                if (this.Y > other.Y)
                {
                    return 1;
                }
                return -1;
            }

            #endregion
        }
    }
}