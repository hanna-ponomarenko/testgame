using UnityEngine;

namespace Testgame.Match3
{
    public class PieceView : MonoBehaviour
    {
        public int X;
        public int Y;
        public PieceType PieceType;

        public void Initialize(int x, int y, PieceType type)
        {
            X = x;
            Y = y;
            PieceType = type;
        }
    }
}
