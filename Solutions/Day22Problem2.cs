using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day22Problem2 : ISolution
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
            var quads = new List<Qudrant>();

            for (var i = 0; i < lines[0].Length; i++)
            {
                for (var j = 0; j < lines.Length; j++)
                {
                    if (lines[i][j] == '#')
                    //infected.Add(new Point()
                    //{
                    //    X = j,
                    //    Y = i,
                    //    State = State.Infected
                    //});
                    {
                        var point = new Point()
                        {
                            X = j,
                            Y = i,
                            State = State.Infected
                        };
                        infected.Add(point);

                        var targetQuad = this.GetQuad(point, quads);
                        var targetArray = targetQuad.Points;
                        Array.Resize(ref targetArray, targetArray.Length + 1);
                        targetArray[targetArray.Length - 1] = point;
                        targetQuad.Points = targetArray;
                    }
                }
            }

            foreach(var quad in quads)
            {
                Array.Sort(quad.Points);
            }

            infected.Sort();
            var infectedArray = infected.ToArray();
            

            var totalInfected = 0L;
            var currentDirection = Direction.Up;
            var totalBursts = 10000000;

            for (var i = 0; i < totalBursts; i++)
            {
                //var matchingPointIndex = Array.BinarySearch(infectedArray, currentPoint);
                //var matchingPoint = matchingPointIndex >= 0 ? infectedArray[matchingPointIndex] : null;
                var matchingQuad = this.GetQuad(currentPoint, quads);

                var matchingPointIndex = Array.BinarySearch(matchingQuad.Points, currentPoint);
                var matchingPoint = matchingPointIndex >= 0 ? matchingQuad.Points[matchingPointIndex] : null;
                if (matchingPoint == null)
                {
                    var newPoint = new Point
                    {
                        X = currentPoint.X,
                        Y = currentPoint.Y,
                        State = State.Weakened
                    };

                    var targetArray = matchingQuad.Points;
                    Array.Resize(ref targetArray, targetArray.Length + 1);
                    targetArray[targetArray.Length - 1] = newPoint;
                    Array.Sort(targetArray);
                    matchingQuad.Points = targetArray;

                    var updatedDirection = ((int)currentDirection) - 1;
                    if (updatedDirection < 0)
                        updatedDirection = 3;

                    currentDirection = (Direction)updatedDirection;
                    currentPoint = this.GetNewPosition(currentPoint, currentDirection);
                }
                else if (matchingPoint.State == State.Weakened)
                {
                    matchingPoint.State = State.Infected;
                    ++totalInfected;
                    currentPoint = this.GetNewPosition(currentPoint, currentDirection);
                }
                else if (matchingPoint.State == State.Infected)
                {
                    var updatedDirection = ((int)currentDirection) + 1;
                    if (updatedDirection > 3)
                        updatedDirection = 0;
                    currentDirection = (Direction)updatedDirection;

                    matchingPoint.State = State.Flagged;
                    currentPoint = this.GetNewPosition(currentPoint, currentDirection);
                }
                else if(matchingPoint.State == State.Flagged)
                {
                    var updatedDirection = ((int)currentDirection) + 2;
                    if (updatedDirection > 3)
                        updatedDirection -= 4;

                    currentDirection = (Direction)updatedDirection;
                    currentPoint = this.GetNewPosition(currentPoint, currentDirection);
                    var updatedArray = new Point[matchingQuad.Points.Length - 1];
                    Array.Copy(matchingQuad.Points, updatedArray, matchingPointIndex);
                    Array.Copy(matchingQuad.Points, matchingPointIndex + 1, updatedArray, matchingPointIndex, updatedArray.Length - matchingPointIndex);
                    matchingQuad.Points = updatedArray;
                }

                if(i % 100000 == 0)
                {
                    Console.WriteLine("Reached " + i);
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

        private Qudrant GetQuad(Point input, List<Qudrant> quads)
        {
            var target = new Point
            {
                X = input.X / 10,
                Y = input.Y / 10
            };

            var match = quads.FirstOrDefault(it => it.QuadId.IsMatch(target));

            if(match != null)
                return match;

            match = new Qudrant()
            {
                QuadId = target,
                Points = new List<Point>().ToArray()
            };

            quads.Add(match);

            return match;
        }

        private class Point : IComparable<Point>
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
            public State State { get; set; }

            public bool IsMatch(Point point)
            {
                return this.X == point.X && this.Y == point.Y;
            }

            public int CompareTo(Point other)
            {
                if (other.X > this.X)
                    return 1;
                if (other.X < this.X)
                    return -1;

                if (other.Y > this.Y)
                    return 1;
                if (other.Y < this.Y)
                    return -1;

                return 0;
            }
        }

        private class Qudrant
        {
            public Point QuadId { get; set; }

            public Point[] Points { get; set; }
        }

        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3
        }

        private enum State
        {
            Weakened,
            Infected,
            Flagged
        }
    }
}
