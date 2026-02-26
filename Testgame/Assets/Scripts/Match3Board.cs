using System;
using System.Collections.Generic;

namespace Testgame.Match3
{
    public interface IRandomProvider
    {
        int NextInclusive(int min, int max);
    }

    public sealed class SystemRandomProvider : IRandomProvider
    {
        private readonly Random random = new Random();

        public int NextInclusive(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }

    public sealed class Match3Board
    {
        public const int Empty = 0;
        private readonly int[,] cells;

        public Match3Board(int[,] initial)
        {
            var width = initial.GetLength(0);
            var height = initial.GetLength(1);
            cells = new int[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    cells[x, y] = initial[x, y];
                }
            }
        }

        public int Width => cells.GetLength(0);
        public int Height => cells.GetLength(1);

        public int Get(int x, int y) => cells[x, y];

        public HashSet<(int x, int y)> FindMatches()
        {
            var matched = new HashSet<(int x, int y)>();

            for (var y = 0; y < Height; y++)
            {
                var x = 0;
                while (x < Width)
                {
                    var value = cells[x, y];
                    if (value == Empty)
                    {
                        x++;
                        continue;
                    }

                    var start = x;
                    while (x < Width && cells[x, y] == value)
                    {
                        x++;
                    }

                    var runLength = x - start;
                    if (runLength >= 3)
                    {
                        for (var i = start; i < x; i++)
                        {
                            matched.Add((i, y));
                        }
                    }
                }
            }

            for (var x = 0; x < Width; x++)
            {
                var y = 0;
                while (y < Height)
                {
                    var value = cells[x, y];
                    if (value == Empty)
                    {
                        y++;
                        continue;
                    }

                    var start = y;
                    while (y < Height && cells[x, y] == value)
                    {
                        y++;
                    }

                    var runLength = y - start;
                    if (runLength >= 3)
                    {
                        for (var i = start; i < y; i++)
                        {
                            matched.Add((x, i));
                        }
                    }
                }
            }

            return matched;
        }

        public int RemoveMatches(HashSet<(int x, int y)> matches)
        {
            var removed = 0;
            foreach (var (x, y) in matches)
            {
                if (cells[x, y] != Empty)
                {
                    cells[x, y] = Empty;
                    removed++;
                }
            }

            return removed;
        }

        public void ApplyGravity()
        {
            for (var x = 0; x < Width; x++)
            {
                var writeY = Height - 1;
                for (var y = Height - 1; y >= 0; y--)
                {
                    var value = cells[x, y];
                    if (value == Empty)
                    {
                        continue;
                    }

                    if (writeY != y)
                    {
                        cells[x, writeY] = value;
                        cells[x, y] = Empty;
                    }

                    writeY--;
                }
            }
        }

        public void Refill(IRandomProvider randomProvider)
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (cells[x, y] == Empty)
                    {
                        cells[x, y] = randomProvider.NextInclusive(1, 10);
                    }
                }
            }
        }

        public int ResolveCascades(IRandomProvider randomProvider)
        {
            var totalRemoved = 0;
            while (true)
            {
                var matches = FindMatches();
                if (matches.Count == 0)
                {
                    break;
                }

                totalRemoved += RemoveMatches(matches);
                ApplyGravity();
                Refill(randomProvider);
            }

            return totalRemoved;
        }
    }
}
