using AdventOfCode2017.Common;
using System;

namespace AdventOfCode2017.Solutions
{
    class Day11Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var directions = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var position = new Point();

            foreach (var item in directions)
            {
                switch (item.Trim())
                {
                    case "n":
                        position.Y += 1;
                        break;
                    case "ne":
                        position.X += 1;
                        break;
                    case "se":
                        position.X += 1;
                        position.Y -= 1;
                        break;
                    case "s":
                        position.Y -= 1;
                        break;
                    case "sw":
                        position.X -= 1;
                        break;
                    case "nw":
                        position.X -= 1;
                        position.Y += 1;
                        break;
                    default:
                        throw new Exception("Unexpected character");
                }
            }

            var result = Math.Max(position.X, Math.Max(position.Y, position.Z));

            return result.ToString();
        }

        private struct Point
        {
            public int X { get; set; }

            public int Y { get; set; }

            public int Z
            {
                get
                {
                    return 0 - (this.X + this.Y);
                }
            }
        }
    }
}
