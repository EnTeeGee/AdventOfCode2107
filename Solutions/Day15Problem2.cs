using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day15Problem2 : ISolution
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

            for (int i = 0; i < 5000000; i++)
            {
                do
                {
                    generatorA *= generatorAFactor;
                    generatorA %= modValue;
                } while (generatorA % 4 != 0);

                do
                {
                    generatorB *= generatorBFactor;
                    generatorB %= modValue;
                } while (generatorB % 8 != 0);

                var genAEnd = new String(generatorA.ToString("x4").Reverse().Take(4).ToArray());
                var genBEnd = new String(generatorB.ToString("x4").Reverse().Take(4).ToArray());

                if (genAEnd == genBEnd)
                    ++totalMatches;
            }

            return totalMatches.ToString();
        }
    }
}
