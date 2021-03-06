﻿using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day08Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var items = lines.Select(it => it.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)).ToList();

            var registers = items.GroupBy(it => it[0]).ToDictionary(it => it.Key, it => 0);

            foreach(var item in items)
            {
                var targetRegister = item[0];
                var isInc = item[1] == "inc";
                var change = Convert.ToInt32(item[2]) * (isInc ? 1 : -1);
                var checkRegister = item[4];
                var checkFunc = this.GetComparer(item[5]);
                var checkValue = Convert.ToInt32(item[6]);

                if(checkFunc(registers[checkRegister], checkValue))
                    registers[targetRegister] += change;
            }

            return registers.Max(it => it.Value).ToString();
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
    }
}
