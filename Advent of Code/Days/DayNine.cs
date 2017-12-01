using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    class DayNine : DayBase
    {
        private Dictionary<string, Dictionary<string, int>> routes;

        public override void Run()
        {
            this.InitiateData();

            var point = this.routes.Select(x => new Step(x.Key, x.Value.Values.Count)).Single(x => x.RoutesCount == 1).Key;
            var bestRoute = new List<string>(this.routes.Count)
                            {
                                this.routes[point].Keys.First()
                            };


            var n = this.routes.Count;
            for (int i = 1; i < n; i++)
            {
                bestRoute.Add(point);
                this.Clean(bestRoute.GetRange(0, bestRoute.Count - 1));

                int ldp = int.MaxValue;

                foreach (var p in this.routes)
                {
                    var dests = p.Value;

                    if (ldp > dests.Values.Count)
                    {
                        point = p.Key;
                        ldp = dests.Values.Count;
                    }
                }
            }

            this.InitiateData(); // reinitialized 

            int distance = 0;
            for (int i = bestRoute.Count - 1; i > 0; i--)
            {
                distance += this.routes[bestRoute[i]][bestRoute[i - 1]];
            }

            Console.WriteLine($"Shortest distance: {distance}");
        }

        private void Clean(List<string> selected)
        {
            var keysToRemove = new List<string>();
            foreach (var start in this.routes)
            {
                var dests = start.Value;

                foreach (var s in selected) dests.Remove(s);

                if(dests.Count == 0) keysToRemove.Add(start.Key);
            }

            foreach (var key in keysToRemove)
            {
                this.routes.Remove(key);
            }
        }

        private void InitiateData()
        {
            this.routes = new Dictionary<string, Dictionary<string, int>>();

            foreach (var route in this.InputFile)
            {
                var parts = route.Split(new[] { " to ", " = " }, StringSplitOptions.RemoveEmptyEntries);
                var key = parts[0];

                if (!this.routes.ContainsKey(key))
                {
                    this.routes.Add(key, new Dictionary<string, int>());
                }

                this.routes[key].Add(parts[1], int.Parse(parts[2]));
            }
        }
    }

    public struct Step : IComparable<Step>
    {
        public string Key { get; set; }

        public int RoutesCount { get; set; }

        public Step(string key, int routesCount)
        {
            this.Key = key;
            this.RoutesCount = routesCount;
        }

        #region Implementation of IComparable<in Step>

        public int CompareTo(Step other)
        {
            if (this.RoutesCount > other.RoutesCount) return 1;
            if (this.RoutesCount < other.RoutesCount) return -1;
            return 0;
        }

        #endregion
    }
}
