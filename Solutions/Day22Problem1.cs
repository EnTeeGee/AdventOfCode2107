using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day22Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim('\r')).ToArray();

            var currentPoint = new Point
            {
                X = lines[0].Length / 2,
                Y = lines.Length / 2
            };

            var infected = new List<Point>();

            for(var i = 0; i < lines[0].Length; i++)
            {
                for(var j = 0; j < lines.Length; j++)
                {
                    if (lines[i][j] == '#')
                        infected.Add(new Point()
                        {
                            X = j,
                            Y = i
                        });
                }
            }

            var originalInfected = infected.ToList();

            var totalInfected = 0;
            var currentDirection = Direction.Up;
            var totalBursts = 10000;

            for(var i = 0; i < totalBursts; i++)
            {
                var matchingPoint = infected.FirstOrDefault(it => it.IsMatch(currentPoint));
                if (matchingPoint == null)
                {
                    infected.Add(new Point
                    {
                        X = currentPoint.X,
                        Y = currentPoint.Y
                    });
                    var updatedDirection = ((int)currentDirection) - 1;
                    if (updatedDirection < 0)
                        updatedDirection = 3;

                    ++totalInfected;

                    currentDirection = (Direction)updatedDirection;
                    currentPoint = this.GetNewPosition(currentPoint, currentDirection);
                }
                else
                {
                    var updatedDirection = ((int)currentDirection) + 1;
                    if (updatedDirection > 3)
                        updatedDirection = 0;

                    currentDirection = (Direction)updatedDirection;
                    currentPoint = this.GetNewPosition(currentPoint, currentDirection);
                    infected.Remove(matchingPoint);
                }
            }

            return totalInfected.ToString();
        }

        private Point GetNewPosition(Point currentPoint, Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    return new Point(currentPoint.X, currentPoint.Y - 1);
                case Direction.Right:
                    return new Point(currentPoint.X + 1, currentPoint.Y);
                case Direction.Down:
                    return new Point(currentPoint.X, currentPoint.Y + 1);
                case Direction.Left:
                    return new Point(currentPoint.X - 1, currentPoint.Y);
            }

            throw new Exception("Unexpected direction");
        }

        private class Point
        {
            public Point()
            {

            }

            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }

            public bool IsMatch(Point point)
            {
                return this.X == point.X && this.Y == point.Y;
            }
        }

        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }
    }
}
