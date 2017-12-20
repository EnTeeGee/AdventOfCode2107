using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day16Problem2 : ISolution
    {
        const string StartingCharacters = "abcdefghijklmnop";

        public string GenerateAnswer(string input)
        {
            var characters = StartingCharacters.Select(it => it.ToString()).ToArray();

            var actionStrings = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var actions = new List<Func<String[], String[]>>();

            foreach (var item in actionStrings)
            {
                if (item[0] == 's')
                {
                    var toCycle = Convert.ToInt32(item.Substring(1));
                    actions.Add(new Func<string[], string[]>(it =>
                    {
                        var toMove = it.Skip(16 - toCycle).ToList();
                        return toMove.Concat(it.Take(16 - toCycle)).ToArray();
                    }));
                }
                else if (item[0] == 'x')
                {
                    var indexes = item.Substring(1).Split('/').Select(it => Convert.ToInt32(it)).ToArray();

                    actions.Add(new Func<string[], string[]>(it =>
                    {
                        var indexPlaceholder = it[indexes[0]];
                        it[indexes[0]] = it[indexes[1]];
                        it[indexes[1]] = indexPlaceholder;
                        return it;
                    }));
                    
                }
                else if (item[0] == 'p')
                {
                    var targets = item.Substring(1).Split('/');
                    actions.Add(new Func<string[], string[]>(it2 =>
                    {
                        var toList = it2.ToList();
                        var indexes = targets.Select(it3 => toList.IndexOf(it3)).ToList();
                        var indexPlaceholder = it2[indexes[0]];
                        it2[indexes[0]] = it2[indexes[1]];
                        it2[indexes[1]] = indexPlaceholder;

                        return it2;
                    }));

                    
                }
            }

            long steps = 1000000000;
            //long steps = 100;

            // standard

            //for (long i = 0; i < steps; i++)
            //{
            //    foreach (var action in actions)
            //    {
            //        characters = action(characters);
            //    }
            //}

            //Console.WriteLine("Standard method returned " + string.Join(string.Empty, characters));

            //characters = StartingCharacters.Select(it => it.ToString()).ToArray();

            for (long i = 0; i < steps; i++)
            {
                foreach(var action in actions)
                {
                    characters = action(characters);
                }

                if(string.Join(string.Empty, characters) == StartingCharacters)
                {
                    var skipTo = (steps / (i + 1)) * (i + 1);

                    i = skipTo - 1;
                }
            }

            //Console.WriteLine("New method returned " + string.Join(string.Empty, characters));

            return string.Join(string.Empty, characters);
        }
    }
}
