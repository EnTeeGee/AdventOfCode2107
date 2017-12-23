using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day20Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim('\r')).ToArray();

            var items = new List<Particle>();
            
            for (var i = 0; i < lines.Length; i++)
            {
                var points = lines[i].Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim(',')).ToArray();
                var item = new Particle();
                item.Id = i;
                item.Position = this.StringToPoint(points[0]);
                item.Velocity = this.StringToPoint(points[1]);
                item.Acceleration = this.StringToPoint(points[2]);
                items.Add(item);
            }

            var peakAcceleration = items.GroupBy(it => it.Acceleration.Distance()).OrderBy(it => it.Key).First();
            var peakVelocity = peakAcceleration.GroupBy(it => it.Velocity.Distance()).OrderBy(it => it.Key).First();
            var peakPosition = peakVelocity.OrderBy(it => it.Position.Distance()).First();


            return peakPosition.Id.ToString();
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

            public int Distance()
            {
                return Math.Abs(this.X) + Math.Abs(this.Y) + Math.Abs(this.Z);
            }
        }

        private class Particle
        {
            public int Id { get; set; }

            public Point Position { get; set; }

            public Point Velocity { get; set; }

            public Point Acceleration { get; set; }
        }
    }
}
