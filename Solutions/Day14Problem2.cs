using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day14Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var points = new Point[128, 128];

            for (var i = 0; i < 128; i++)
            {
                var hash = KnotHash.Generate($"{input}-{i}");
                var pointsForHash = hash
                    .Select(it => Convert.ToString(Convert.ToByte(it.ToString(), 16), 2))
                    .Select(it => it.PadLeft(4, '0'))
                    .SelectMany(it => it)
                    .Select(it => it == '1' ? new Point() : null)
                    .ToArray();

                var test = hash.Select(it => Convert.ToString(Convert.ToByte(it.ToString(), 16), 2))
                    .Select(it => it.PadLeft(4, '0'))
                    .SelectMany(it => it);

                var test2 = String.Join(string.Empty, test);
                //var test2 = "a0c2017"

                for (var j = 0; j < pointsForHash.Length; j++)
                    points[i, j] = pointsForHash[j];
            }

            var currentGroups = 0;

            for(var i = 0; i < 128 * 128; i++)
            {
                var x = i / 128;
                var y = i % 128;
                if (points[x, y] != null && points[x, y].Group == null)
                {
                    ++currentGroups;
                    AssignPointToGroup(currentGroups, points, x, y);
                }
            }

            return currentGroups.ToString();
        }

        private void AssignPointToGroup(int group, Point[,] points, int x, int y)
        {
            if (points[x, y].Group != null)
                return;

            points[x, y].Group = group;

            var surroundingPoints = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(x - 1, y),
                new Tuple<int, int>(x, y - 1),
                new Tuple<int, int>(x + 1, y),
                new Tuple<int, int>(x, y + 1)
            }.Where(it => it.Item1 >= 0 && it.Item1 < 128 && it.Item2 >= 0 && it.Item2 < 128)
            .Select(it => new { Point = points[it.Item1, it.Item2], X = it.Item1, Y = it.Item2 })
            .Where(it => it.Point != null && it.Point.Group == null);

            foreach (var point in surroundingPoints)
                AssignPointToGroup(group, points, point.X, point.Y);
        }

        private class Point
        {
            public int? Group { get; set; }
        }
    }
}
