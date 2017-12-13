using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day13Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var walls = lines.Select(it => new Wall(it)).ToList();

            var hasHits = walls.Any(it => it.IsHit());
            var currentDelay = 0;

            while(hasHits)
            {
                foreach (var item in walls)
                    item.Depth++;
                ++currentDelay;

                hasHits = walls.Any(it => it.IsHit());
            }

            return currentDelay.ToString();
        }

        private class Wall
        {
            public Wall(string input)
            {
                var items = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                Depth = Convert.ToInt32(items[0].Trim(':'));
                Range = Convert.ToInt32(items[1]);
            }

            public int Depth { get; set; }

            public int Range { get; }

            public bool IsHit()
            {
                var cycle = (this.Range - 1) * 2;

                return this.Depth % cycle == 0;
            }
        }
    }
}
