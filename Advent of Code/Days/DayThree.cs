﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    internal class DayThree : DayBase
    {
        private Dictionary<Location, House> map;
        private readonly bool santaAlone;

        public DayThree(bool isSantaAlone = true)
        {
            this.map = new Dictionary<Location, House>();
            this.santaAlone = isSantaAlone;
        }

        public override void Run()
        {
            var startLocation = new Location(0, 0);
            var santaPosition = startLocation;
            var roboSantaPosition = startLocation;

            for (int index = 0; index < this.InputFile.Length; index++)
            {
                santaPosition = this.PresentDelivery(santaPosition, this.InputFile[index]);

                if(this.santaAlone) continue;

                roboSantaPosition = this.PresentDelivery(roboSantaPosition, this.InputFile[++index]);
            }

            Console.WriteLine(this.map.Values.Count(h => h.VisitCount > 0));
        }

        private Location PresentDelivery(Location position, char move)
        {
            var santaMove = move;
            if (this.map.ContainsKey(position))
            {
                this.map[position].VisitCount++;
            }
            else
            {
                this.map.Add(position, new House());
            }

            return this.GoSantaGo(position, santaMove);
        }

        private Location GoSantaGo(Location current, char move)
        {
            switch (move)
            {
                case '^': return new Location(current.X, current.Y + 1);
                case 'v': return new Location(current.X, current.Y - 1);
                case '>': return new Location(current.X + 1, current.Y);
                case '<': return new Location(current.X - 1, current.Y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private class House
        {
            public int VisitCount { get; set; }

            public House()
            {
                this.VisitCount = 1;
            }
        }

        private struct Location : IComparable<Location>
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Location(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            #region Implementation of IComparable<in Location>

            public int CompareTo(Location other)
            {
                if (this.X == other.X && this.Y == other.Y)
                    return 0;

                if (this.Y > other.Y)
                    return 1;
                return -1;
            }

            #endregion
        }
    }
}