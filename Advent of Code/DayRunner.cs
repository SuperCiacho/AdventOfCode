using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Days;

namespace AdventOfCode
{
    public static class DayRunner
    {
        private static readonly Dictionary<string, Type> TypeMap;

        static DayRunner()
        {
            TypeMap = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.Name.StartsWith("Day"))
                .ToDictionary(x => x.Name);
        }

        public static DayBase GetSpecificDay(int day)
        {
            var className = $"Day{day.NumberToWords().ToTitleCase().Replace("-", string.Empty)}";
            var type = TypeMap[className];
            return Activator.CreateInstance(type) as DayBase;
        }
    }

    public static class ExtentionMethods
    {
        /// <summary>
        /// Converts number to it's word equivalent.
        /// Thanks to LukeH.
        /// http://stackoverflow.com/a/2730393/1592183
        /// </summary>
        /// <param name="number"></param>
        /// <returns>String</returns>
        public static string NumberToWords(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }


    }
}
