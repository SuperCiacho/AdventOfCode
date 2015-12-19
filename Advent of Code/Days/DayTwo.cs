using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class DayTwo : DayBase
    {
        public override void Run()
        {
            var presents = this.InputFile.Select(dim => new Present(dim)).ToList();

            var allSurface = presents.Sum(present => present.GetPresentSurface());
            var allRibbon = presents.Sum(present => present.GetRibbonLength());

            Console.WriteLine("Paper surface: " + allSurface);
            Console.WriteLine("Ribbon length: " + allRibbon);
        }

        private struct Present
        {
            public int Width { get; }
            public int Length { get; }
            public int Height { get; }

            public Present(string serializedPresent)
            {
                var dims = serializedPresent.Split('x');

                this.Length = int.Parse(dims[0]);
                this.Width = int.Parse(dims[1]);
                this.Height = int.Parse(dims[2]);
            }

            public int GetPresentSurface()
            {
                return 2 * this.Length * this.Width +
                       2 * this.Width * this.Height +
                       2 * this.Height * this.Length +
                       this.GetAreaOfSmallestSize();
            }

            private int GetAreaOfSmallestSize()
            {
                var dims = this.GetTwoSmallestDimensions();
                return dims.Item1 * dims.Item2;
            }

            private Tuple<int, int> GetTwoSmallestDimensions()
            {
                var dims = new List<int>(3) {this.Length, this.Width, this.Height};
                dims.Sort();

                return new Tuple<int, int>(dims[0], dims[1]);
            }

            public int GetRibbonLength()
            {
                var dims = this.GetTwoSmallestDimensions();
                return 2 * dims.Item1 +
                       2 * dims.Item2 +
                       this.Length * this.Width * this.Height;
            }
        }
    }
}