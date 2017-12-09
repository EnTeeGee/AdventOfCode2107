using AdventOfCode2017.Common;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017.Solutions
{
    class Day09Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var withoutContainingGroup = new string(input.Skip(1).Take(input.Length - 2).ToArray());

            var assembler = new Assembler();

            foreach (var item in withoutContainingGroup)
            {
                assembler.ReadNext(item);
            }

            return assembler.TotalGarbage.ToString();
        }

        private class Assembler
        {
            private bool ignoringNext;

            private bool isInGarbage;

            private Group currentGroup;

            public int TotalGarbage { get; private set; }

            public Assembler()
            {
                currentGroup = new Group(1, null);
            }

            public void ReadNext(char input)
            {
                if (ignoringNext)
                {
                    this.ignoringNext = false;
                    return;
                }

                if (input == '!')
                {
                    this.ignoringNext = true;
                    return;
                }

                if (isInGarbage)
                {
                    if (input == '>')
                        this.isInGarbage = false;
                    else
                        ++this.TotalGarbage;
                    return;
                }

                if (input == '<')
                {
                    this.isInGarbage = true;
                    return;
                }

                if (input == '{')
                {
                    var parentGroup = currentGroup;
                    currentGroup = new Group(parentGroup.Score + 1, parentGroup);
                    parentGroup.SubGroups.Add(currentGroup);
                }
                else if (input == '}' && currentGroup.Parent != null)
                {
                    currentGroup = currentGroup.Parent;
                }
            }
        }

        private class Group
        {
            public Group(int score, Group parent)
            {
                this.Score = score;
                this.Parent = parent;
                this.SubGroups = new List<Group>();
            }

            public int Score { get; }

            public Group Parent { get; }

            public List<Group> SubGroups { get; }
        }
    }
}
