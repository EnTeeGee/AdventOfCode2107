using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day14Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var totalCount = 0;

            for(var i = 0; i < 128; i++)
            {
                var hash = KnotHash.Generate($"{input}-{i}");
                var usedCount = hash
                    .Select(it => Convert.ToString(Convert.ToByte(it.ToString(), 16), 2))
                    .SelectMany(it => it)
                    .Count(it => it == '1');
                totalCount += usedCount;
            }

            return totalCount.ToString();
        }
    }
}
