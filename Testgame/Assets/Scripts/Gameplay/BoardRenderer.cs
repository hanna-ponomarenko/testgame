using UnityEngine;

namespace Testgame.Match3
{
    public class BoardRenderer : MonoBehaviour
    {
        public int width = 8;
        public int height = 8;
        public float tileSize = 1f;

        public GameObject tilePrefab;
        public GameObject piecePrefab;

        private Board board;

        private void Start()
        {
            board = new Board(width, height);
            board.GenerateRandomBoard();
            RenderBoard();
        }

        private void RenderBoard()
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var position = new Vector3(x * tileSize, y * tileSize, 0f);

                    var tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    var tileView = tile.GetComponent<TileView>();
                    if (tileView != null)
                    {
                        tileView.Initialize(x, y);
                    }

                    var type = board.GetPiece(x, y);
                    var piece = Instantiate(piecePrefab, position, Quaternion.identity, transform);
                    var pieceView = piece.GetComponent<PieceView>();
                    if (pieceView != null)
                    {
                        pieceView.Initialize(x, y, type);
                    }
                }
            }
        }
    }
}
