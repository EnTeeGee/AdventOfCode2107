using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day15Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var generatorAFactor = 16807;
            var generatorBFactor = 48271;
            var modValue = int.MaxValue;

            var tokens = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var generatorA = Convert.ToInt64(tokens[4]);
            var generatorB = Convert.ToInt64(tokens[9]);
            var totalMatches = 0;

            for(int i = 0; i < 40000000; i++)
            {
                generatorA *= generatorAFactor;
                generatorA %= modValue;
                generatorB *= generatorBFactor;
                generatorB %= modValue;

                var genAEnd = new String(generatorA.ToString("x4").Reverse().Take(4).ToArray());
                var genBEnd = new String(generatorB.ToString("x4").Reverse().Take(4).ToArray());

                if (genAEnd == genBEnd)
                    ++totalMatches;
            }

            return totalMatches.ToString();
        }
    }
}
