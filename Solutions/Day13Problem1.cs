using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day13Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var walls = lines.Select(it => new Wall(it)).ToList();

            var hits = walls.Where(it => it.IsHit()).Select(it => it.Depth * it.Range).Sum();

            return hits.ToString();
        }

        private class Wall
        {
            public Wall(string input)
            {
                var items = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                Depth = Convert.ToInt32(items[0].Trim(':'));
                Range = Convert.ToInt32(items[1]);
            }

            public int Depth { get; }

            public int Range { get; }

            public bool IsHit()
            {
                var cycle = (this.Range - 1) * 2;

                return this.Depth % cycle == 0;
            }
        }
    }
}
