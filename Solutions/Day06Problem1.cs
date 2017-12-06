using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day06Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var data = input
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                .Select(it => Convert.ToInt32(it))
                .ToArray();

            var history = new List<int[]>();
            var count = 0;

            while (!this.DoesHistoryContain(history, data))
            {
                history.Add(data.ToArray());
                ++count;
                var maxValue = data.Max();
                var MaxIndex = data.ToList().IndexOf(maxValue);
                data[MaxIndex] = 0;

                while(maxValue > 0)
                {
                    if(++MaxIndex >= data.Length)
                        MaxIndex = 0;
                    data[MaxIndex]++;
                    maxValue--;
                }

            }

            return count.ToString();
        }

        private bool DoesHistoryContain(List<int[]> history, int[] input)
        {
            return history
                .Select(it => it
                    .Zip(input, (a, b) => a - b)
                    .All(diff => diff == 0))
                .Any(it => it == true);
        }
    }
}
