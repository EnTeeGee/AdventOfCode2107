using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day07Problem1 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var progs = lines.Select(it => new Prog(it)).Where(it => it.Nodes.Any()).ToList();

            var currentHead = progs[0];

            while (true)
            {
                var parent = progs.FirstOrDefault(it => it.Nodes.Contains(currentHead.Name));
                if(parent == null)
                    return currentHead.Name;

                currentHead = parent;
            }
        }

        private class Prog
        {
            public Prog(string input)
            {
                var items = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                this.Name = items[0];

                if(items.Length > 2)
                    this.Nodes = items.Skip(3).Select(it => it.TrimEnd(',')).ToList();
                else
                    this.Nodes = new List<string>();
            }

            public string Name { get; }

            public List<string> Nodes { get; }
        }
    }
}
