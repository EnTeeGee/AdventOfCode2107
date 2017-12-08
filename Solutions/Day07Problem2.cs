using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day07Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var progs = lines.Select(it => new Prog(it)).ToList();

            foreach(var item in progs)
                item.AssignNodes(progs);

            var headNode = this.GetHead(progs.Where(it => it.Nodes.Any()).ToList());

            return headNode.GetRequiredWeight().ToString();
        }

        private Prog GetHead(List<Prog> progs)
        {
            var currentHead = progs[0];

            while (true)
            {
                var parent = progs.FirstOrDefault(it => it.NodeNames.Contains(currentHead.Name));
                if (parent == null)
                    return currentHead;

                currentHead = parent;
            }
        }

        private class Prog
        {
            private int? totalWeight;

            public Prog(string input)
            {
                var items = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                this.Name = items[0];
                this.Weight = Convert.ToInt32(items[1].Trim('(', ')'));
                this.Nodes = new List<Prog>();

                if (items.Length > 2)
                    this.NodeNames = items.Skip(3).Select(it => it.TrimEnd(',')).ToList();
                else
                {
                    this.NodeNames = new List<string>();
                    this.totalWeight = this.Weight;
                }
            }

            public void AssignNodes(List<Prog> nodeList)
            {
                this.Nodes.AddRange(this.NodeNames.Join(nodeList, it => it, it => it.Name, (a, b) => b));
            }

            public int GetTotalWeight()
            {
                if (this.totalWeight == null)
                    this.totalWeight = this.Nodes.Sum(it => it.GetTotalWeight()) + this.Weight;

                return this.totalWeight.Value;
            }

            public int GetRequiredWeight()
            {
                var unbalancedNode = this.Nodes.FirstOrDefault(it => this.Nodes.Count(node => node.GetTotalWeight() == it.GetTotalWeight()) == 1);
                var balancedNode = this.Nodes.First(it => it != unbalancedNode);


                return unbalancedNode.GetRequiredWeight(balancedNode.GetTotalWeight());
            }

            public int GetRequiredWeight(int expectedWeightOfStack)
            {
                var unbalancedNode = this.Nodes.FirstOrDefault(it => this.Nodes.Count(node => node.GetTotalWeight() == it.GetTotalWeight()) == 1);

                if(unbalancedNode == null)
                {
                    var childWeight = this.Nodes[0].GetTotalWeight();
                    return expectedWeightOfStack - (childWeight * this.Nodes.Count);
                }

                var balancedNode = this.Nodes.First(it => it != unbalancedNode);

                return unbalancedNode.GetRequiredWeight(balancedNode.GetTotalWeight());
            }

            public string Name { get; }

            public int Weight { get; }

            public List<string> NodeNames { get; }

            public List<Prog> Nodes { get; }
        }
    }
}
