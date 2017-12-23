using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day18Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lastPlayedSound = 0L;
            var currentInstruction = 0L;
            var registers = new Dictionary<string, long>();

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            while(currentInstruction >= 0 && currentInstruction < lines.Length)
            {
                var items = lines[currentInstruction].Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    switch (items[0])
                    {
                        case "snd":
                            lastPlayedSound = this.GetValue(items[1], registers);
                            break;
                        case "set":
                            registers[items[1]] = this.GetValue(items[2], registers);
                            break;
                        case "add":
                            this.CreateIfNotExisting(items[1], registers);
                            registers[items[1]] += this.GetValue(items[2], registers);
                            break;
                        case "mul":
                            this.CreateIfNotExisting(items[1], registers);
                            registers[items[1]] *= this.GetValue(items[2], registers);
                            break;
                        case "mod":
                            this.CreateIfNotExisting(items[1], registers);
                            registers[items[1]] %= this.GetValue(items[2], registers);
                            break;
                        case "rcv":
                            if (items[1] != "0")
                                return lastPlayedSound.ToString();
                            break;
                        case "jgz":
                            var xValue = this.GetValue(items[1], registers);
                            if (xValue > 0)
                            {
                                var jumpValue = this.GetValue(items[2], registers);
                                currentInstruction += jumpValue;
                                continue;
                            }
                            break;
                    }
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Caught exception: " + e.Message);
                    throw e;
                }

                

                ++currentInstruction;
            }

            throw new Exception("Reached end of program");
        }

        private long GetValue(string value, Dictionary<string, long> registers)
        {
            if(value.Length == 1 && Char.IsLetter(value[0]))
            {
                if (registers.ContainsKey(value))
                    return registers[value];

                registers[value] = 0;
                return 0;
            }

            return Convert.ToInt32(value);
        }

        private void CreateIfNotExisting(string register, Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(register))
                registers[register] = 0;
        }
    }
}
