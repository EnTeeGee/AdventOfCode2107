using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day04Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return lines.Select(it => this.IsPassPhraseValid(it)).Count(it => it).ToString();
        }

        private bool IsPassPhraseValid(string passPhrase)
        {
            var words = passPhrase.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            for(var i = 0; i < words.Length; i++)
            {
                for (var j = i + 1; j < words.Length; j++)
                {
                    if(words[i] == words[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
