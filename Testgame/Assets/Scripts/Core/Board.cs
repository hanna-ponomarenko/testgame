namespace Testgame.Match3
{
    public class Board
    {
        public int Width;
        public int Height;

        private readonly PieceType[,] grid;

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            grid = new PieceType[width, height];
        }

        public bool IsInside(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public PieceType GetPiece(int x, int y)
        {
            return grid[x, y];
        }

        public void SetPiece(int x, int y, PieceType type)
        {
            grid[x, y] = type;
        }
    }
}
