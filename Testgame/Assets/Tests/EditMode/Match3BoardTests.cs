using NUnit.Framework;
using Testgame.Match3;

namespace Testgame.Tests.EditMode
{
    public sealed class Match3BoardTests
    {
        private sealed class FixedRandomProvider : IRandomProvider
        {
            private readonly int value;

            public FixedRandomProvider(int value)
            {
                this.value = value;
            }

            public int NextInclusive(int min, int max)
            {
                return value;
            }
        }

        [Test]
        public void Parse_ReadsBoardSizeAndCells()
        {
            var level = "3 2\n1 2 3\n4 5 6";

            var board = LevelParser.Parse(level);

            Assert.AreEqual(3, board.GetLength(0));
            Assert.AreEqual(2, board.GetLength(1));
            Assert.AreEqual(1, board[0, 0]);
            Assert.AreEqual(6, board[2, 1]);
        }

        [Test]
        public void FindMatches_DetectsHorizontalAndVerticalRuns()
        {
            var boardData = new[,]
            {
                { 1, 1, 1 },
                { 1, 2, 3 },
                { 1, 2, 3 }
            };

            var board = new Match3Board(boardData);
            var matches = board.FindMatches();

            Assert.IsTrue(matches.Contains((0, 0)));
            Assert.IsTrue(matches.Contains((1, 0)));
            Assert.IsTrue(matches.Contains((2, 0)));
            Assert.IsTrue(matches.Contains((0, 1)));
            Assert.IsTrue(matches.Contains((0, 2)));
        }

        [Test]
        public void ResolveCascades_RemovesThenRefills()
        {
            var boardData = new[,]
            {
                { 1, 2, 3 },
                { 1, 4, 5 },
                { 1, 6, 7 }
            };

            var board = new Match3Board(boardData);
            var removed = board.ResolveCascades(new FixedRandomProvider(8));

            Assert.GreaterOrEqual(removed, 3);
            for (var x = 0; x < board.Width; x++)
            {
                for (var y = 0; y < board.Height; y++)
                {
                    Assert.AreNotEqual(Match3Board.Empty, board.Get(x, y));
                }
            }
        }
    }
}
