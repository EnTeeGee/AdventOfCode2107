using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day17Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var steps = Convert.ToInt32(input);
            var currentPosition = 1;
            var currentLength = 2;
            var currentSecondValue = 0;
            var target = 50000001;

            for (var i = 2; i < target; i++)
            {
                var newPosition = ((steps + currentPosition) % currentLength) + 1;
                if (newPosition == 1)
                    currentSecondValue = i;
                currentPosition = newPosition;
                ++currentLength;
            }

            return currentSecondValue.ToString();
        }
    }
}
