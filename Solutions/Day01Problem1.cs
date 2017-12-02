using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    public class Day01Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var numberList = input.Select(it => Convert.ToInt32(it.ToString())).ToList();
            var offsetList = numberList.Concat(new List<int> { numberList[0] }).Skip(1).ToList();

            return numberList.Zip(offsetList, (a, b) => new { a, b }).Where(it => it.a == it.b).Sum(it => it.a).ToString();
        }
    }
}
