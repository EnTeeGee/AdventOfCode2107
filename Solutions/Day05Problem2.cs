﻿using AdventOfCode2017.Common;
using System;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day05Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var numbers = input
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                .Select(it => Convert.ToInt32(it))
                .ToArray();

            var currentPosition = 0;
            var currentSteps = 0;

            while (currentPosition < numbers.Length)
            {
                if (numbers[currentPosition] >= 3)
                    currentPosition += (numbers[currentPosition]--);
                else
                    currentPosition += (numbers[currentPosition]++);

                currentSteps++;
            }

            return currentSteps.ToString();
        }
    }
}
