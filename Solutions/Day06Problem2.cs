using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day06Problem2 : ISolution
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
                this.DistubuteValues(data);
            }

            // Reached loop start

            history.Clear();
            count = 0;
            history.Add(data.ToArray());

            while(count == 0 || !this.DoesHistoryContain(history, data))
            {
                ++count;
                this.DistubuteValues(data);
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

        private void DistubuteValues(int[] data)
        {
            var maxValue = data.Max();
            var MaxIndex = data.ToList().IndexOf(maxValue);
            data[MaxIndex] = 0;

            while (maxValue > 0)
            {
                if (++MaxIndex >= data.Length)
                    MaxIndex = 0;
                data[MaxIndex]++;
                maxValue--;
            }
        }
    }
}
