using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Days
{
    class DaySeven : DayBase
    {
        private readonly Dictionary<string, Gate> circut;

        public DaySeven()
        {
            this.circut = new Dictionary<string, Gate>();
        }

        public override void Run()
        {
            // this.Test();

            foreach (var gate in this.InputFile.Select(Gate.Parse))
            {
                this.circut.Add(gate.Name, gate);
            }

            // PART A
            var aValue = this.circut["a"].GetValue(this.circut);
            Console.WriteLine(aValue);

            // PART B
            this.circut["b"].Value = aValue;
            foreach (var gate in this.circut.Values) { gate.Reset(); }

            Console.WriteLine(this.circut["a"].GetValue(this.circut));
        }

        private void Test()
        {
            this.InputFile = new[]
                    {
                        "123 -> x",
                        "456 -> y",
                        "x AND y -> d",
                        "x OR y -> e",
                        "x LSHIFT 2 -> f",
                        "y RSHIFT 2 -> g",
                        "NOT x -> h",
                        "NOT y -> i"
                    };

            foreach (var gate in this.InputFile.Select(Gate.Parse))
            {
                this.circut.Add(gate.Name, gate);
            }

            foreach (var gate in this.circut.Values)
            {
                Console.WriteLine($"{gate.Name}: {gate.GetValue(this.circut)}");
            }

            // Output:
            // x: 123
            // y: 456
            // d: 72
            // e: 507
            // f: 492
            // g: 114
            // h: 65412
            // i: 65079
        }

        private class Gate
        {
            private static readonly string[] ParseSplitters = { "->", " " };

            public ushort? Value { get; set; }

            private Func<Dictionary<string, Gate>, ushort> valueCalculation;

            public string Name { get; private set; }

            public ushort GetValue(Dictionary<string, Gate> circut)
            {
                if (!this.Value.HasValue)
                {
                    this.Value = this.valueCalculation(circut);
                }

                return this.Value.Value;
            }

            public static Gate Parse(string serialized)
            {
                var parts = serialized.Split(ParseSplitters, StringSplitOptions.RemoveEmptyEntries);
                var partsLength = parts.Length;

                var gate = new Gate
                {
                    Name = parts[partsLength - 1]
                };

                if (partsLength == 2)
                {
                    ushort val;
                    if (ushort.TryParse(parts[0], out val))
                    {
                        gate.Value = val;
                    }
                    else
                    {
                        gate.valueCalculation = d =>
                        {
                            Gate g;
                            if (d.TryGetValue(parts[0], out g))
                            {
                                return g.GetValue(d);
                            }

                            return 0;
                        };
                    }
                }
                else if (partsLength == 3)
                {
                    ushort val;
                    if (ushort.TryParse(parts[1], out val))
                    {
                        gate.Value = (ushort?)~val;
                    }
                    else
                    {
                        gate.valueCalculation = d =>
                        {
                            Gate g;
                            if (d.TryGetValue(parts[1], out g))
                            {
                                g.Value = (ushort)~g.GetValue(d);
                                return g.GetValue(d);
                            }
                            return 0;
                        };
                    }
                }
                else
                {
                    ushort factorA;
                    ushort factorB;
                    string keyA = parts[0];
                    string keyB = parts[2];
                    bool factorACalc;
                    bool factorBCalc;

                    switch (parts[1])
                    {
                        case "AND":
                            factorACalc = ushort.TryParse(keyA, out factorA);
                            factorBCalc = ushort.TryParse(keyB, out factorB);

                            if (factorACalc && factorBCalc)
                            {
                                gate.Value = (ushort)(factorA & factorB);
                                gate.valueCalculation = c => (ushort)(factorA & factorB);
                            }
                            else if (factorACalc)
                            {
                                gate.valueCalculation = c => (ushort)(factorA & c[keyB].GetValue(c));
                            }
                            else if (factorBCalc)
                            {
                                gate.valueCalculation = c => (ushort)(c[keyA].GetValue(c) & factorB);
                            }
                            else
                            {
                                gate.valueCalculation = c => (ushort)(c[keyA].GetValue(c) & c[keyB].GetValue(c));
                            }

                            break;
                        case "OR":
                            factorACalc = ushort.TryParse(keyA, out factorA);
                            factorBCalc = ushort.TryParse(keyB, out factorB);

                            if (factorACalc && factorBCalc)
                            {
                                gate.valueCalculation = c => (ushort)(factorA | factorB);
                            }
                            else if (factorACalc)
                            {
                                gate.valueCalculation = c => (ushort)(factorA | c[keyB].GetValue(c));
                            }
                            else if (factorBCalc)
                            {
                                gate.valueCalculation = c => (ushort)(c[keyA].GetValue(c) | factorB);
                            }
                            else
                            {
                                gate.valueCalculation = c => (ushort)(c[keyA].GetValue(c) | c[keyB].GetValue(c));
                            }

                            break;
                        case "LSHIFT":
                            factorACalc = ushort.TryParse(keyA, out factorA);
                            factorB = ushort.Parse(keyB);

                            if (factorACalc)
                            {
                                gate.valueCalculation = c => (ushort)(factorA << factorB);
                            }
                            else
                            {
                                gate.valueCalculation = c => (ushort)(c[keyA].GetValue(c) << factorB);
                            }
                            break;
                        case "RSHIFT":
                            factorACalc = ushort.TryParse(keyA, out factorA);
                            factorB = ushort.Parse(keyB);

                            if (factorACalc)
                            {
                                gate.valueCalculation = c => (ushort)(factorA << factorB);
                            }
                            else
                            {
                                gate.valueCalculation = c => (ushort)(c[keyA].GetValue(c) >> factorB);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return gate;
            }

            public void Reset()
            {
                if (this.valueCalculation != null)
                {
                    this.Value = null;
                }
            }
        }
    }
}
