using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;

namespace AdventOfCode2017.Solutions
{
    class Day17Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var steps = Convert.ToInt32(input);
            var currentPosition = 1;

            var values = new List<int> { 0, 1 };

            for(var i = 2; i < 2018; i++)
            {
                var newPosition = ((steps + currentPosition) % values.Count) + 1;
                values.Insert(newPosition, i);
                currentPosition = newPosition;
            }

            return values[currentPosition + 1].ToString();
        }
    }
}
