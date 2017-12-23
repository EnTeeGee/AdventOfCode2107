using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day20Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            //Hacky solution: just run for a a lot of steps and see what's left
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim('\r')).ToArray();

            var items = new List<Particle>();

            for (var i = 0; i < lines.Length; i++)
            {
                var points = lines[i].Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim(',')).ToArray();
                var item = new Particle();
                item.Id = i;
                item.InitialPosition = this.StringToPoint(points[0]);
                item.CurrentPosition = item.InitialPosition;
                item.InitialVelocity = this.StringToPoint(points[1]);
                item.CurrentVelocity = item.InitialVelocity;
                item.Acceleration = this.StringToPoint(points[2]);
                items.Add(item);
            }

            var steps = 100000;

            for(var i = 0; i < steps; i++)
            {
                foreach (var item in items)
                {
                    item.PerformStep();
                }

                for(var j = 0; j < items.Count; j++)
                {
                    var toRemove = new List<Particle>();

                    for(var k = j + 1; k < items.Count; k++)
                    {
                        if (items[j].IsSame(items[k]))
                            toRemove.Add(items[k]);
                    }

                    if (toRemove.Any())
                    {
                        items.RemoveAll(it => toRemove.Contains(it));
                        items.Remove(items[j]);
                        --j;
                    }
                }
            }

            return items.Count.ToString();
        }

        private Point StringToPoint(string input)
        {
            var items = input.Split(new char[] { ',' }).Select(it => it.Trim('a', 'v', 'p', '<', '>', '=')).ToArray();

            var output = new Point();
            output.X = Convert.ToInt32(items[0]);
            output.Y = Convert.ToInt32(items[1]);
            output.Z = Convert.ToInt32(items[2]);

            return output;
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public bool IsSame(Point point)
            {
                return this.X == point.X && this.Y == point.Y && this.Z == point.Z;
            }

            public void Add(Point point)
            {
                this.X += point.X;
                this.Y += point.Y;
                this.Z += point.Z;
            }
        }

        private class Particle
        {
            public int Id { get; set; }

            public Point InitialPosition { get; set; }

            public Point CurrentPosition { get; set; }

            public Point InitialVelocity { get; set; }

            public Point CurrentVelocity { get; set; }

            public Point Acceleration { get; set; }

            public void PerformStep()
            {
                this.CurrentVelocity.Add(this.Acceleration);
                this.CurrentPosition.Add(this.CurrentVelocity);
            }

            public bool IsSame(Particle particle)
            {
                return CurrentPosition.IsSame(particle.CurrentPosition);
            }
        }
    }
}
