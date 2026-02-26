using System;

namespace Testgame.Match3
{
    public static class LevelParser
    {
        public static int[,] Parse(string levelText)
        {
            if (string.IsNullOrWhiteSpace(levelText))
            {
                throw new ArgumentException("Level text is empty.", nameof(levelText));
            }

            var lines = levelText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
            {
                throw new FormatException("Level requires a header and at least one row.");
            }

            var header = lines[0].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (header.Length != 2)
            {
                throw new FormatException("Header must be '<width> <height>'.");
            }

            var width = int.Parse(header[0]);
            var height = int.Parse(header[1]);
            if (width <= 0 || height <= 0)
            {
                throw new FormatException("Width and height must be positive.");
            }

            if (lines.Length - 1 != height)
            {
                throw new FormatException($"Expected {height} rows but found {lines.Length - 1}.");
            }

            var board = new int[width, height];
            for (var y = 0; y < height; y++)
            {
                var row = lines[y + 1].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (row.Length != width)
                {
                    throw new FormatException($"Row {y} expected {width} values but found {row.Length}.");
                }

                for (var x = 0; x < width; x++)
                {
                    var value = int.Parse(row[x]);
                    if (value < 1 || value > 10)
                    {
                        throw new FormatException($"Cell ({x},{y}) value {value} is outside 1..10.");
                    }

                    board[x, y] = value;
                }
            }

            return board;
        }
    }
}
