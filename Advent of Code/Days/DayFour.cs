// Training Ground
// Createad by Bartosz Nowak on 18/12/2015 13:02

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class DayFour : DayBase
    {
        private const string SecretKey = "iwrupvqb";
        private const string Condition = "000000";

        public DayFour() : base(false) { }

        public override void Run()
        {
            var sw = Stopwatch.StartNew();
           // var number = this.MultipleThreadSearch();
             var number = this.SingleThreadSearch(SecretKey);

            sw.Stop();

            Console.WriteLine("Your number is: " + number);
            Console.WriteLine("Finding it took " + sw.Elapsed);
        }

        private int MultipleThreadSearch()
        {
            var numbers = new List<int>();

            int chunkSize = 100000;
            int range = 1000000;
            int numberOfTasks = range / chunkSize;
            var tasks = new Task[numberOfTasks];

            for (int i = 0; i < numberOfTasks; i++)
            {
                var start = chunkSize * i;
                var end = (chunkSize * (i + 1)) - 1;
                tasks[i] = this.CreateTask(start, end, numbers);
            }

            Task.WaitAll(tasks);

            if (numbers.Count == 0)
            {
                numbers.Add(-1);
            }

            return numbers.Min();
        }

        private Task CreateTask(int start, int end, List<int> nums)
        {
            return Task.Factory.StartNew(
                () =>
                {
                    var taskId = Task.CurrentId;
                    Console.WriteLine("Task {0} started", taskId);
                    var md5Factory = MD5.Create();
                    for (int x = start; x < end; x++)
                    {
                        Console.WriteLine("Task {0} is trying with: {1}", taskId, x);
                        var key = string.Concat(SecretKey, x);
                        var bytes = Encoding.ASCII.GetBytes(key); 
                        var md5 = md5Factory.ComputeHash(bytes);
                        var first5 = string.Join("", md5.Take(3).Select(b => b.ToString("X2")));

                        if (first5.StartsWith(Condition))
                        {
                            nums.Add(x);
                            break;
                        }
                        else if (nums.Count > 0 && nums[0] < x)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("Task {0} end up.", taskId);
                });
        }

        private int SingleThreadSearch(string input)
        {
            using (var md5Factory = MD5.Create())
            {
                for (int i = 0; i < 20000000; i++)
                {
                    var key = string.Concat(input, i);
                    var bytes = Encoding.ASCII.GetBytes(key);
                    var md5 = md5Factory.ComputeHash(bytes);
                    var first5 = string.Join("", md5.Take(3).Select(b => b.ToString("X2")));

                    if (first5.StartsWith(Condition))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
