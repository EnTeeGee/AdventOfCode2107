using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    public class Day02Problem2
    {
        public string GenerateAnswer(string input)
        {
            var rows = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return rows.Select(it => this.RowToDiv(it)).Sum().ToString();
        }

        private int RowToDiv(string row)
        {
            var numbers = row.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Select(it => Convert.ToInt32(it)).OrderByDescending(it => it).ToList();

            for(var i = 0; i < numbers.Count; i++)
            {
                for(var j = i + 1; j < numbers.Count; j++)
                {
                    if(numbers[i] % numbers[j] == 0)
                    {
                        return numbers[i] / numbers[j];
                    }
                }
            }

            throw new Exception("Failed to find divisible pair");
        }
    }
}
