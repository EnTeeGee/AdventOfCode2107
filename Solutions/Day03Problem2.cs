using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day03Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var value = Convert.ToInt32(input);

            var currentLevel = SpiralLayer.Initial;

            while(currentLevel.Values.Last() <= value)
            {
                currentLevel = new SpiralLayer(currentLevel);
            }

            return currentLevel.Values.Where(it => it > value).Min().ToString();
        }

        private struct SpiralPoint
        {
            public int Value { get; }

            public int X { get; }

            public int Y { get; }

            public SpiralPoint(int value, int x, int y)
            {
                this.Value = value;
                this.X = x;
                this.Y = y;
            }

            public bool IsNextTo(int x, int y)
            {
                return Math.Abs(this.X - x) <= 1 && Math.Abs(this.Y - y) <= 1;
            }

            public override string ToString()
            {
                return $"{this.Value} ({this.X}, {this.Y})";
            }
        }

        private class SpiralLayer
        {
            private List<SpiralPoint> points;

            public static SpiralLayer Initial
            {
                get
                {
                    return new SpiralLayer(0, new List<SpiralPoint> { new SpiralPoint(1, 0, 0) });
                }
            }

            public int Level { get; }

            public IEnumerable<int> Values { get { return this.points.Select(it => it.Value); } }

            public SpiralLayer(SpiralLayer priorLayer)
            {
                this.Level = priorLayer.Level + 1;
                this.points = new List<SpiralPoint>();

                var currentX = this.Level;
                var currentY = -this.Level + 1;
                var previousValue = 0;

                while(currentY <= this.Level)
                {
                    var pointValue = priorLayer.GetSumOfPointsNextTo(currentX, currentY) + previousValue;
                    previousValue = pointValue;
                    this.points.Add(new SpiralPoint(pointValue, currentX, currentY));
                    ++currentY;
                }

                previousValue += this.points[this.points.Count - 2].Value;
                --currentY;
                --currentX;

                while(currentX >= -this.Level)
                {
                    var pointValue = priorLayer.GetSumOfPointsNextTo(currentX, currentY) + previousValue;
                    previousValue = pointValue;
                    this.points.Add(new SpiralPoint(pointValue, currentX, currentY));
                    --currentX;
                }

                previousValue += this.points[this.points.Count - 2].Value;
                ++currentX;
                --currentY;

                while (currentY >= -this.Level)
                {
                    var pointValue = priorLayer.GetSumOfPointsNextTo(currentX, currentY) + previousValue;
                    previousValue = pointValue;
                    this.points.Add(new SpiralPoint(pointValue, currentX, currentY));
                    --currentY;
                }

                previousValue += this.points[this.points.Count - 2].Value;
                ++currentY;
                ++currentX;

                while (currentX <= this.Level)
                {
                    var pointValue = priorLayer.GetSumOfPointsNextTo(currentX, currentY) + previousValue;
                    if(this.points[0].IsNextTo(currentX, currentY))
                    {
                        pointValue += this.points[0].Value;
                    }

                    previousValue = pointValue;
                    this.points.Add(new SpiralPoint(pointValue, currentX, currentY));
                    ++currentX;
                }
            }

            private SpiralLayer(int level, List<SpiralPoint> points)
            {
                this.Level = level;
                this.points = points;
            }

            public int GetSumOfPointsNextTo(int x, int y)
            {
                return this.points.Where(it => it.IsNextTo(x, y)).Select(it => it.Value).Sum();
            }
        }
    }
}
