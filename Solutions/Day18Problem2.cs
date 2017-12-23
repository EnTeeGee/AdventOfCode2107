using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day18Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var programs = new List<Program>();
            programs.Add(new Program(input, 0));
            programs.Add(new Program(input, 1));

            while (true)
            {
                if (!programs[0].CanRun() && !programs[1].CanRun())
                    return programs[1].TotalSends.ToString();

                if (programs[0].CanRun())
                {
                    var result = programs[0].RunStep();
                    if (result != null)
                        programs[1].ReceivedValues.Add(result.Value);
                }
                else
                {
                    var result = programs[1].RunStep();
                    if (result != null)
                        programs[0].ReceivedValues.Add(result.Value);
                }
            }
        }

        private enum ProgramState
        {
            Running,
            Blocked,
            Terminated
        }

        private class Program
        {
            private long currentInstruction = 0;

            private Dictionary<string, long> registers;

            private string[] instructions;

            public List<long> ReceivedValues { get; }

            public ProgramState State { get; private set; }

            public int TotalSends { get; private set; }

            public int ProgramId { get; }

            public Program(string input, int pValue)
            {
                this.ProgramId = pValue;
                this.registers = new Dictionary<string, long>();
                this.registers.Add("p", pValue);
                this.instructions = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                this.ReceivedValues = new List<long>();
                this.State = ProgramState.Running;
            }

            public bool CanRun()
            {
                if (this.State == ProgramState.Terminated)
                    return false;

                if(currentInstruction < 0 || currentInstruction > instructions.Length)
                {
                    this.State = ProgramState.Terminated;
                    return false;
                }

                if (this.State == ProgramState.Blocked && !this.ReceivedValues.Any())
                    return false;

                this.State = ProgramState.Running;
                return true;
            }

            public long? RunStep()
            {
                var items = instructions[currentInstruction].Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    switch (items[0])
                    {
                        case "snd":
                            ++currentInstruction;
                            ++TotalSends;
                            return this.GetValue(items[1], registers);
                        case "set":
                            registers[items[1]] = this.GetValue(items[2], registers);
                            ++currentInstruction;
                            return null;
                        case "add":
                            this.CreateIfNotExisting(items[1], registers);
                            registers[items[1]] += this.GetValue(items[2], registers);
                            ++currentInstruction;
                            return null;
                        case "mul":
                            this.CreateIfNotExisting(items[1], registers);
                            registers[items[1]] *= this.GetValue(items[2], registers);
                            ++currentInstruction;
                            return null;
                        case "mod":
                            this.CreateIfNotExisting(items[1], registers);
                            registers[items[1]] %= this.GetValue(items[2], registers);
                            ++currentInstruction;
                            return null;
                        case "rcv":
                            if (!this.ReceivedValues.Any())
                            {
                                this.State = ProgramState.Blocked;
                                return null;
                            }

                            var latestReceived = this.ReceivedValues.First();
                            this.ReceivedValues.RemoveAt(0);
                            registers[items[1]] = latestReceived;
                            ++currentInstruction;
                            return null;
                        case "jgz":
                            var xValue = this.GetValue(items[1], registers);
                            if (xValue > 0)
                            {
                                var jumpValue = this.GetValue(items[2], registers);
                                currentInstruction += jumpValue;
                            }
                            else
                                ++currentInstruction;
                            return null;
                    }

                    throw new Exception("Unkown instruction");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Caught exception: " + e.Message);
                    throw e;
                }
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

            private void CreateIfNotExisting(string register, Dictionary<string, long> registers)
            {
                if (!registers.ContainsKey(register))
                    registers[register] = 0;
            }
        }
    }
}
