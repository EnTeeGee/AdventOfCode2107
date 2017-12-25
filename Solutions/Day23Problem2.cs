using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    /*

        Defeated here. Looking at the threads it's something to do with finding non-primes,
        but i feel I've cheated now that I know that.

    */

    class Day23Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var currentInstruction = 0L;
            var registers = new Dictionary<string, long>
            {
                { "a", 1 },
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
            var priorEvents = new List<JumpEvent>();

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
                                var shouldSkip = this.ShouldSkipJump(priorEvents, registers, currentInstruction, items[1]);

                                if (shouldSkip)
                                {
                                    ++currentInstruction;
                                }
                                else
                                {
                                    var jumpValue = this.GetValue(items[2], registers);
                                    currentInstruction += jumpValue;
                                }
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

            return registers["h"].ToString();
        }

        private bool ShouldSkipJump(List<JumpEvent> priorEvents, Dictionary<string, long> currentRegisters, long currentLine, string targetedRegister)
        {
            var matchingEvent = priorEvents.FirstOrDefault(it => it.JumpLine == currentLine);

            if(matchingEvent == null)
            {
                var newEvent = new JumpEvent
                {
                    JumpLine = currentLine,
                    RegisterState = new Dictionary<string, long>(currentRegisters)
                };
                priorEvents.Add(newEvent);

                return false;
            }

            var stepSize = Math.Abs(matchingEvent.RegisterState[targetedRegister] - currentRegisters[targetedRegister]);
            var totalSteps = currentRegisters[targetedRegister] / stepSize;
            currentRegisters[targetedRegister] = 0;

            var registersToUpdate = currentRegisters.Keys.Where(it => it != targetedRegister).ToList();
            foreach(var item in registersToUpdate)
            {
                var change = currentRegisters[item] - matchingEvent.RegisterState[item];
                currentRegisters[item] += (change * totalSteps);
            }

            priorEvents.Clear();

            return true;
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

        private class JumpEvent
        {
            public Dictionary<string, long> RegisterState { get; set; }

            public long JumpLine { get; set; }
        }
    }
}
