using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    public class Day01Problem2
    {
        public string GenerateAnswer(string input)
        {
            var midPoint = input.Length / 2;
            var numberList = input.Select(it => Convert.ToInt32(it.ToString())).ToList();
            var offsetList = numberList.Concat(numberList.Take(midPoint)).Skip(midPoint).ToList();

            return numberList.Zip(offsetList, (a, b) => new { a, b }).Where(it => it.a == it.b).Sum(it => it.a).ToString();
        }
    }
}
