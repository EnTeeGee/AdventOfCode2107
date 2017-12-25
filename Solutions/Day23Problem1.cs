using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day23Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var currentInstruction = 0L;
            var registers = new Dictionary<string, long>
            {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 },
                { "e", 0 },
                { "f", 0 },
                { "g", 0 },
                { "h", 0 },
            };

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var totalMulCalls = 0;

            while (currentInstruction >= 0 && currentInstruction < lines.Length)
            {
                var items = lines[currentInstruction].Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    switch (items[0])
                    {
                        case "set":
                            registers[items[1]] = this.GetValue(items[2], registers);
                            break;
                        case "sub":
                            registers[items[1]] -= this.GetValue(items[2], registers);
                            break;
                        case "mul":
                            ++totalMulCalls;
                            registers[items[1]] *= this.GetValue(items[2], registers);
                            break;
                        case "jnz":
                            var xValue = this.GetValue(items[1], registers);
                            if (xValue != 0)
                            {
                                var jumpValue = this.GetValue(items[2], registers);
                                currentInstruction += jumpValue;
                                continue;
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Caught exception: " + e.Message);
                    throw e;
                }

                ++currentInstruction;
            }

            return totalMulCalls.ToString();
        }

        private long GetValue(string value, Dictionary<string, long> registers)
        {
            if (value.Length == 1 && Char.IsLetter(value[0]))
            {
                if (registers.ContainsKey(value))
                    return registers[value];

                registers[value] = 0;
                return 0;
            }

            return Convert.ToInt32(value);
        }
    }
}
