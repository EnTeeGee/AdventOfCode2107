using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    public class Day03Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var value = Convert.ToInt32(input);

            var currentLayer = SpiralLayer.Inital;

            while (!currentLayer.ContainsValue(value))
            {
                currentLayer = currentLayer.GetNextLayer();
            }

            return currentLayer.GetDistanceForValue(value).ToString();
        }

        private struct SpiralLayer
        {
            private int valueBefore;
            private int topRight;
            private int topLeft;
            private int bottomLeft;
            private int finalValue;
            private int level;

            public static SpiralLayer Inital
            {
                get
                {
                    return new SpiralLayer(1, 1);
                }
            }

            public SpiralLayer(int level, int valueBefore)
            {
                this.level = level;
                this.valueBefore = valueBefore;
                this.topRight = valueBefore + (level * 2);
                this.topLeft = this.topRight + (level * 2);
                this.bottomLeft = this.topLeft + (level * 2);
                this.finalValue = this.bottomLeft + (level * 2);
            }

            public SpiralLayer GetNextLayer()
            {
                return new SpiralLayer(this.level + 1, this.finalValue);
            }

            public bool ContainsValue(int value)
            {
                return (this.valueBefore < value && this.finalValue >= value);
            }

            public int GetDistanceForValue(int value)
            {
                var midPoints = new List<int>
                {
                    this.valueBefore + level,
                    this.topRight + level,
                    this.topLeft + level,
                    this.bottomLeft + level
                };

                return midPoints.Select(it => Math.Abs(it - value)).Min() + this.level;
            }
        }
    }
}
