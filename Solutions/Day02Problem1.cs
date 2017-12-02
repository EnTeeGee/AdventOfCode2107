using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    public class Day02Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var rows = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return rows.Select(it => this.RowToDiff(it)).Sum().ToString();
        }

        private int RowToDiff(string row)
        {
            var numbers = row.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(it => Convert.ToInt32(it));
            return numbers.Max() - numbers.Min();
        }
    }
}
