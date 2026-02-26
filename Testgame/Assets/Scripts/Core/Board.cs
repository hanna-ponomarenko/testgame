namespace Testgame.Match3
{
    public class Board
    {
        public int Width;
        public int Height;

        private readonly PieceType[,] grid;
        private readonly System.Random random = new System.Random();

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

        public void GenerateRandomBoard()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    PieceType type;

                    do
                    {
                        type = GetRandomPiece();
                    }
                    while ((x >= 2 && grid[x - 1, y] == type && grid[x - 2, y] == type)
                        || (y >= 2 && grid[x, y - 1] == type && grid[x, y - 2] == type));

                    grid[x, y] = type;
                }
            }
        }

        private PieceType GetRandomPiece()
        {
            return (PieceType)random.Next((int)PieceType.Color1, (int)PieceType.Color10 + 1);
        }
    }
}
