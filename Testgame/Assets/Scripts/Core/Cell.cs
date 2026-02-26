namespace Testgame.Match3
{
    public class Cell
    {
        public int X;
        public int Y;
        public PieceType PieceType;

        public Cell(int x, int y, PieceType pieceType)
        {
            X = x;
            Y = y;
            PieceType = pieceType;
        }
    }
}
