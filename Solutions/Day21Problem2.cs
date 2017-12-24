using AdventOfCode2017.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017.Solutions
{
    class Day21Problem2 : ISolution
    {
        public string GenerateAnswer(string input)
        {
            var initialGrid = new List<bool>
            {
                false, true, false,
                false, false, true,
                true, true, true
            };

            var grids = new List<List<bool>> { initialGrid };

            var lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(it => it.Trim('\r')).ToArray();

            var mappings = lines.Select(it => new Mapping(it)).ToList();

            for (var i = 0; i < 18; i++)
            {
                //grids = grids.Select(it => mappings.First(mp => mp.IsMatch(it))).SelectMany(it => it.Outputs).ToList();
                var temp1 = grids.Select(it => mappings.First(mp => mp.IsMatch(it))).ToList();
                grids = temp1.SelectMany(it => it.Outputs).ToList();
                var temp = this.ConvertListsToMaxtix(grids);
                grids = this.ConvertMatrixToLists(temp);
            }

            return grids.Sum(it => it.Where(sg => sg == true).Count()).ToString();
        }

        private bool[,] ConvertListsToMaxtix(List<List<bool>> input)
        {
            var listsPerSide = (int)Math.Sqrt(input.Count);
            var listSize = input[0].Count == 9 ? 3 : 4;
            var itemsPerSide = listsPerSide * listSize;

            var output = new bool[itemsPerSide, itemsPerSide];

            for (var i = 0; i < input.Count; i++)
            {
                //var x = (i % listsPerSide) * listSize;
                //var y = (i / listsPerSide) * listSize;
                var x = (i / listsPerSide) * listSize;
                var y = (i % listsPerSide) * listSize;
                var list = input[i];

                if (listSize == 3)
                {
                    output[x, y] = list[0];
                    output[x + 1, y] = list[1];
                    output[x + 2, y] = list[2];
                    output[x, y + 1] = list[3];
                    output[x + 1, y + 1] = list[4];
                    output[x + 2, y + 1] = list[5];
                    output[x, y + 2] = list[6];
                    output[x + 1, y + 2] = list[7];
                    output[x + 2, y + 2] = list[8];
                }
                else
                {
                    output[x + 0, y + 0] = list[0];
                    output[x + 1, y + 0] = list[1];
                    output[x + 2, y + 0] = list[2];
                    output[x + 3, y + 0] = list[3];
                    output[x + 0, y + 1] = list[4];
                    output[x + 1, y + 1] = list[5];
                    output[x + 2, y + 1] = list[6];
                    output[x + 3, y + 1] = list[7];
                    output[x + 0, y + 2] = list[8];
                    output[x + 1, y + 2] = list[9];
                    output[x + 2, y + 2] = list[10];
                    output[x + 3, y + 2] = list[11];
                    output[x + 0, y + 3] = list[12];
                    output[x + 1, y + 3] = list[13];
                    output[x + 2, y + 3] = list[14];
                    output[x + 3, y + 3] = list[15];
                }
            }

            return output;
        }

        private List<List<bool>> ConvertMatrixToLists(bool[,] input)
        {
            var sideLength = input.GetLength(0);
            var multiple = 3;
            if (sideLength % 2 == 0)
                multiple = 2;
            var blocksPerSide = sideLength / multiple;

            var output = new List<List<bool>>();

            for (var i = 0; i < blocksPerSide; i++)
            {
                for (var j = 0; j < blocksPerSide; j++)
                {
                    var indexX = i * multiple;
                    var indexY = j * multiple;

                    if (multiple == 2)
                    {
                        output.Add(new List<bool>
                        {
                            input[indexX, indexY], input[indexX + 1, indexY],
                            input[indexX, indexY + 1], input[indexX + 1, indexY + 1]
                        });
                    }
                    else
                    {
                        output.Add(new List<bool>
                        {
                            input[indexX, indexY], input[indexX + 1, indexY], input[indexX + 2, indexY],
                            input[indexX, indexY + 1], input[indexX + 1, indexY + 1], input[indexX + 2, indexY + 1],
                            input[indexX, indexY + 2], input[indexX + 1, indexY + 2], input[indexX + 2, indexY + 2]
                        });
                    }
                }
            }

            return output;
        }

        private class Mapping
        {
            public Mapping(string input)
            {
                var items = input
                    .Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(it => it.Where(ch => ch == '.' || ch == '#').Select(ch => ch == '#').ToList())
                    .ToList();

                source = items[0];

                if (items[1].Count == 9)
                {
                    Outputs = new List<List<bool>> { items[1] };
                    return;
                }

                // [00][01][02][03]
                // [04][05][06][07]
                // [08][09][10][11]
                // [12][13][14][15]

                //var temp = items[1];
                //Outputs = new List<List<bool>>
                //{
                //    new List<bool> { temp[0], temp[1], temp[4], temp[5] },
                //    new List<bool> { temp[2], temp[3], temp[6], temp[7] },
                //    new List<bool> { temp[8], temp[9], temp[12], temp[13] },
                //    new List<bool> { temp[10], temp[11], temp[14], temp[15] }
                //};
                Outputs = new List<List<bool>> { items[1] };
            }

            public bool IsMatch(List<bool> input)
            {
                if (input.Count != source.Count)
                    return false;

                var variations = new List<List<bool>>
                {
                    source,
                    this.Rotate(source),
                    this.Rotate(this.Rotate(source)),
                    this.Rotate(this.Rotate(this.Rotate(source))),
                    this.Flip(source),
                    this.Rotate(this.Flip(source)),
                    this.Rotate(this.Rotate(this.Flip(source))),
                    this.Rotate(this.Rotate(this.Rotate(this.Flip(source))))
                };

                return variations.Any(it => this.ListsMatch(it, input));
            }

            private bool ListsMatch(List<bool> input1, List<bool> input2)
            {
                var zipped = input1.Zip(input2, (a, b) => a == b);
                return zipped.All(it => it == true);
            }

            // Lazy method of doing this, but little sense writing generic methods for so few and simple cases
            private List<bool> Rotate(List<bool> input)
            {
                // [0][1]
                // [2][3]
                if (input.Count == 4)
                {
                    return new List<bool> { input[1], input[3], input[0], input[2] };
                }

                // [0][1][2]
                // [3][4][5]
                // [6][7][8]
                return new List<bool>
                {
                    input[2], input[5], input[8],
                    input[1], input[4], input[7],
                    input[0], input[3], input[6]
                };
            }

            private List<bool> Flip(List<bool> input)
            {
                // [0][1]
                // [2][3]
                if (input.Count == 4)
                {
                    return new List<bool> { input[1], input[0], input[3], input[2] };
                }

                // [0][1][2]
                // [3][4][5]
                // [6][7][8]
                return new List<bool>
                {
                    input[2], input[1], input[0],
                    input[5], input[4], input[3],
                    input[8], input[7], input[6]
                };
            }

            private List<bool> source;

            public List<List<bool>> Outputs { get; private set; }
        }
    }
}
