using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AdventOfCode2017.Common
{
    public class Manager
    {
        private static readonly List<string> startupMessages = new List<string>
        {
            "Enter day followed by 1 or 2 for solution 1 or 2.",
            "Leave blank to use latest solution.",
            "Type 'q' to quit.",
            "Input for solution is copied from the clipboard."
        };

        public void Init()
        {
            var solutions = this.GetSolutions();

            foreach(var item in startupMessages)
            {
                Console.WriteLine(item);
            }

            var enteredOption = Console.ReadLine();

            while(enteredOption.ToLower() != "q")
            {
                if (string.IsNullOrWhiteSpace(enteredOption))
                {
                    var latestSolution = solutions.OrderByDescending(it => it.Name).First();
                    this.RunInstanceOfSolution(latestSolution);
                }
                else
                {
                    var inputs = enteredOption.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        var day = Convert.ToInt32(inputs[0]);
                        var solution = Convert.ToInt32(inputs[1]);
                        var expectedName = $"Day{day: D2}Problem{solution}";

                        this.RunInstanceOfSolution(solutions.Single(it => it.Name == expectedName));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to find a solution from the entered options.");
                    }
                }

                Console.ReadLine();
            }

        }

        private List<Type> GetSolutions()
        {
            var solutionType = typeof(ISolution);

            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(it => solutionType.IsAssignableFrom(it) && !it.IsInterface)
                .ToList();
        }

        private void RunInstanceOfSolution(Type solutionType)
        {
            var input = this.ReadDataFromClipboard();
            var instance = (ISolution)Activator.CreateInstance(solutionType);

            try
            {
                var result = instance.GenerateAnswer(input);
                Console.WriteLine($"Solution for {solutionType.Name}:");
                Console.WriteLine(result);
                this.WriteDataToClipboard(result);
                Console.WriteLine("Result copied to clipboard.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Generate answer: " + e.Message);
            }
        }


        public string ReadDataFromClipboard()
        {
            if (!Clipboard.ContainsText())
            {
                return string.Empty;
            }

            return Clipboard.GetText();
        }

        public void WriteDataToClipboard(string data)
        {
            Clipboard.SetText(data);
        }
    }
}
