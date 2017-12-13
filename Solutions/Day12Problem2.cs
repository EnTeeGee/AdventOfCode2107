using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day12Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var items = lines.Select(it => it.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)).ToList();
            var nodes = new List<Node>();

            foreach (var item in items)
            {
                nodes.Add(new Node(item[0], item.Skip(2).Select(it => it.Trim(',')).ToList()));
            }

            foreach (var item in nodes)
            {
                foreach (var name in item.LinkedNodeNames)
                {
                    var matchingNode = nodes.First(it => it.Name == name);
                    item.LinkedNodes.Add(matchingNode);
                }
            }

            var linkedNodes = nodes[0].GetLinkedNodes(new List<string> { "0" });
            var groups = 1;

            while(linkedNodes.Count < lines.Count())
            {
                var firstUnlinkedNode = nodes.FirstOrDefault(it => !linkedNodes.Contains(it.Name));
                linkedNodes.AddRange(firstUnlinkedNode.GetLinkedNodes(new List<string> { firstUnlinkedNode.Name }));
                ++groups;
            }

            return groups.ToString();
        }

        private class Node
        {
            public Node(string name, List<string> linkedNodeNames)
            {
                this.Name = name;
                this.LinkedNodeNames = linkedNodeNames;
                this.LinkedNodes = new List<Node>();
            }

            public string Name { get; }

            public List<string> LinkedNodeNames { get; }

            public List<Node> LinkedNodes { get; }

            public List<string> GetLinkedNodes(List<string> knownLinks)
            {
                var unknownLinks = this.LinkedNodes.Where(it => !knownLinks.Contains(it.Name));
                var combinedLinks = knownLinks.Concat(unknownLinks.Select(it => it.Name));

                foreach (var item in unknownLinks)
                    combinedLinks = item.GetLinkedNodes(combinedLinks.ToList());

                return combinedLinks.ToList();
            }
        }
    }
}
