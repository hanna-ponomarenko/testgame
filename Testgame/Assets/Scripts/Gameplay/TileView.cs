using UnityEngine;

namespace Testgame.Match3
{
    public class TileView : MonoBehaviour
    {
        public int X;
        public int Y;

        public void Initialize(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
