using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class DayEight : DayBase
    {
        public override void Run()
        {
            var partOneData = new List<Tuple<int, int>>();
            var partTwoData = new List<Tuple<int, int>>();

            foreach (var line in this.InputFile)
            //foreach (var line in TestData())
            {
                var el = line.Substring(1, line.Length - 2);
                partOneData.Add(new Tuple<int, int>(line.Length, Regex.Unescape(el).Length));
                partTwoData.Add(new Tuple<int, int>(line.Length, el.ToLiteral().Length));
            }

            // PartOneCheck(partOneData);
            // PartTwoCheck(partTwoData);

            Console.WriteLine(partOneData.Sum(x => x.Item1) - partOneData.Sum(x => x.Item2));
            Console.WriteLine(partTwoData.Sum(x => x.Item1) - partTwoData.Sum(x => x.Item2));
            Console.WriteLine(partTwoData.Sum(x => x.Item2) - partTwoData.Sum(x => x.Item1));

        }

        private static void PartOneCheck(List<Tuple<int, int>> data)
        {
            Console.WriteLine(new string('#', Console.BufferWidth - 1));
            Console.WriteLine("Part One Test");
            Console.WriteLine(new string('#', Console.BufferWidth - 1));

            List<Tuple<int, int>> correctData = new List<Tuple<int, int>>
                                                {
                                                    new Tuple<int, int>(2, 0),
                                                    new Tuple<int, int>(5, 3),
                                                    new Tuple<int, int>(10, 7),
                                                    new Tuple<int, int>(6, 1)
                                                };

            for (int i = 0; i < correctData.Count; i++)
            {
                if (correctData[i].Item1 != data[i].Item1)
                {
                    Console.Error.WriteLine($"Line {i+1} is incorrect at first length");
                }

                if (correctData[i].Item2 != data[i].Item2)
                {
                    Console.Error.WriteLine($"Line {i + 1} is incorrect at second length");
                }
            }
        }

        private static void PartTwoCheck(List<Tuple<int, int>> data)
        {
            Console.WriteLine(new string('#', Console.BufferWidth - 1));
            Console.WriteLine("Part Two Test");
            Console.WriteLine(new string('#', Console.BufferWidth - 1));
            List<Tuple<int, int>> correctData = new List<Tuple<int, int>>
                                                {
                                                    new Tuple<int, int>(2, 6),
                                                    new Tuple<int, int>(5, 9),
                                                    new Tuple<int, int>(10, 16),
                                                    new Tuple<int, int>(6, 11)
                                                };

            for (int i = 0; i < correctData.Count; i++)
            {
                if (correctData[i].Item1 != data[i].Item1)
                {
                    Console.Error.WriteLine($"Line {i + 1} is incorrect at first length");
                }

                if (correctData[i].Item2 != data[i].Item2)
                {
                    Console.Error.WriteLine($"Line {i + 1} is incorrect at second length");
                }
            }
        }


        private static IEnumerable<string> TestData()
        {
            return File.ReadLines("E:\\test.txt");
            // return new [] {"\"\"", "\"abc\"", "\"aaa\\\"aaa\"", "\"\\x27\"" };
        }

       
    }

    internal static class Extenstions
    {
        public static string ToLiteral(this string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, new CodeGeneratorOptions { IndentString = "\t" });
                    var literal = writer.ToString();
                    literal = literal.Replace($"\" +{Environment.NewLine}\t\"", "");
                    return literal;
                }
            }
        }
    }
}
