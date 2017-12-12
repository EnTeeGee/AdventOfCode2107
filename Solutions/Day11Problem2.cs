using AdventOfCode2017.Common;
using System;

namespace AdventOfCode2017.Solutions
{
    class Day11Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var directions = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var position = new Point();
            var maxDistance = 0;

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

                maxDistance = Math.Max(maxDistance, position.GetCurentDistance());
            }

            return maxDistance.ToString();
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

            public int GetCurentDistance()
            {
                return Math.Max(this.X, Math.Max(this.Y, this.Z));
            }
        }
    }
}
