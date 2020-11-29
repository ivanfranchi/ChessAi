using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
{
    public class Queen : Piece
    {
        public Queen(
            bool isWhite,
            Point position)
            : base(
                isWhite,
                "Q",
                position)
        { }

        public override List<Point> GetLegalMoves()
        {
            return new List<Point>();
        }
    }
}
