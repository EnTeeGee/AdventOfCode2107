using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Common
{
    static class KnotHash
    {
        public static string Generate(string input)
        {
            var values = Enumerable.Range(0, 256).Select(it => (byte)it).ToArray();
            var currentPosition = 0;
            var skipSize = 0;

            var lengths = input
                .Trim()
                .Select(it => Convert.ToByte(it))
                .Concat(new List<byte> { 17, 31, 73, 47, 23 })
                .ToList();

            for (var i = 0; i < 64; i++)
            {
                foreach (var length in lengths)
                {
                    var subset = values.Concat(values).Skip(currentPosition).Take(length).ToList();
                    subset.Reverse();
                    ApplyValuesToArray(values, subset, currentPosition);
                    currentPosition = (currentPosition + length + skipSize) % values.Length;
                    ++skipSize;
                }
            }

            var blocks = Enumerable.Range(0, 16).Select(it => values.Skip(it * 16).Take(16).ToList()).ToList();
            var bytes = blocks.Select(it => XorCombineList(it)).Select(it => it.ToString("x2"));
            return string.Join(string.Empty, bytes);

        }

        private static void ApplyValuesToArray(byte[] values, List<byte> updatedValues, int position)
        {
            for (var i = 0; i < updatedValues.Count; i++)
            {
                var index = (i + position) % values.Length;
                values[index] = updatedValues[i];
            }
        }

        private static byte XorCombineList(List<byte> input)
        {
            return input.Aggregate((a, b) => (byte)(a ^ b));
        }
    }
}
