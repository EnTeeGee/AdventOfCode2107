using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day19Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim('\r')).ToArray();

            var columns = lines[0].Length;

            var currentDirection = Direction.Down;
            var currentPoint = new Point(lines[0].IndexOf('|'), 0);

            var foundCharacters = new List<char>();
            var totalSteps = 0;

            while (true)
            {
                ++totalSteps;

                switch (currentDirection)
                {
                    case Direction.Up:
                        ++currentPoint.Y;
                        break;
                    case Direction.Right:
                        ++currentPoint.X;
                        break;
                    case Direction.Down:
                        --currentPoint.Y;
                        break;
                    case Direction.Left:
                        --currentPoint.X;
                        break;
                }

                var symbol = lines[-currentPoint.Y][currentPoint.X];

                if (char.IsLetter(symbol))
                {
                    foundCharacters.Add(symbol);
                    continue;
                }
                else if (symbol == ' ')
                {
                    return totalSteps.ToString();
                }
                else if (symbol == '+')
                {
                    var matchingDirections = new List<Direction>();

                    if (lines[-currentPoint.Y - 1][currentPoint.X] != ' ')
                        matchingDirections.Add(Direction.Up);
                    if (lines[-currentPoint.Y][currentPoint.X + 1] != ' ')
                        matchingDirections.Add(Direction.Right);
                    if (lines[-currentPoint.Y + 1][currentPoint.X] != ' ')
                        matchingDirections.Add(Direction.Down);
                    if (lines[-currentPoint.Y][currentPoint.X - 1] != ' ')
                        matchingDirections.Add(Direction.Left);

                    if (matchingDirections.Count == 1)
                        return totalSteps.ToString();
                    else
                    {
                        var inverseDirection = (Direction)(((int)currentDirection + 2) % 4);

                        currentDirection = matchingDirections.Single(it => it != inverseDirection);
                    }
                }
            }

            throw new Exception("Unexpected end of program");
        }

        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        private struct Point
        {
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; set; }

            public int Y { get; set; }
        }
    }
}
