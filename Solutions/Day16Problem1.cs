using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day16Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var characters = "abcdefghijklmnop".Select(it => it.ToString()).ToList();

            var actions = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach(var item in actions)
            {
                if(item[0] == 's')
                {
                    var toCycle = Convert.ToInt32(item.Substring(1));
                    var toMove = characters.Skip(16 - toCycle).ToList();
                    characters = toMove.Concat(characters.Take(16 - toCycle)).ToList();
                }
                else if (item[0] == 'x')
                {
                    var indexes = item.Substring(1).Split('/').Select(it => Convert.ToInt32(it)).ToArray();
                    var indexPlaceholder = characters[indexes[0]];
                    characters[indexes[0]] = characters[indexes[1]];
                    characters[indexes[1]] = indexPlaceholder;
                }
                else if(item[0] == 'p')
                {
                    var indexes = item.Substring(1).Split('/').Select(it => characters.IndexOf(it)).ToArray();
                    var indexPlaceholder = characters[indexes[0]];
                    characters[indexes[0]] = characters[indexes[1]];
                    characters[indexes[1]] = indexPlaceholder;
                }
            }

            return string.Join(string.Empty, characters);
        }
    }
}
