using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day10Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var values = Enumerable.Range(0, 256).ToArray();
            var currentPosition = 0;
            var skipSize = 0;

            var lengths = input.Split(',').Select(it => Convert.ToInt32(it)).ToList();

            foreach(var length in lengths)
            {
                var subset = values.Concat(values).Skip(currentPosition).Take(length).ToList();
                subset.Reverse();
                this.ApplyValuesToArray(values, subset, currentPosition);
                currentPosition = (currentPosition + length + skipSize) % values.Length;
                ++skipSize;
            }

            return (values[0] * values[1]).ToString();
        }

        private void ApplyValuesToArray(int[] values, List<int> updatedValues, int position)
        {
            for(var i = 0; i < updatedValues.Count; i++)
            {
                var index = (i + position) % values.Length;
                values[index] = updatedValues[i];
            }
        }
    }
}
