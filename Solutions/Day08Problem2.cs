using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day08Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var items = lines.Select(it => it.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)).ToList();

            var registers = items.GroupBy(it => it[0]).ToDictionary(it => it.Key, it => new Register(it.Key));

            foreach (var item in items)
            {
                var targetRegister = item[0];
                var isInc = item[1] == "inc";
                var change = Convert.ToInt32(item[2]) * (isInc ? 1 : -1);
                var checkRegister = item[4];
                var checkFunc = this.GetComparer(item[5]);
                var checkValue = Convert.ToInt32(item[6]);

                if (checkFunc(registers[checkRegister].CurrentValue, checkValue))
                    registers[targetRegister].Update(change);
            }

            return registers.Max(it => it.Value.MaxValue).ToString();
        }

        private Func<int, int, bool> GetComparer(string value)
        {
            switch (value)
            {
                case ">":
                    return (a, b) => a > b;
                case "<":
                    return (a, b) => a < b;
                case ">=":
                    return (a, b) => a >= b;
                case "<=":
                    return (a, b) => a <= b;
                case "==":
                    return (a, b) => a == b;
                default:
                    return (a, b) => a != b;
            }
        }

        private class Register
        {
            public Register(string name)
            {
                this.Name = name;
            }

            public void Update(int value)
            {
                this.CurrentValue += value;
                this.MaxValue = Math.Max(this.CurrentValue, this.MaxValue);
            }

            string Name { get; }

            public int CurrentValue { get; private set; }

            public int MaxValue { get; private set; }
        }
    }
}
