using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
{
    public class King : Piece
    {
        public King(
            bool isWhite,
            Point position,
            bool hasMoved)
            : base(
                isWhite,
                "K",
                position)
        {
            HasMoved = hasMoved;
        }

        public bool HasMoved { get; }

        public override List<Point> GetLegalMoves()
        {
            //throw new System.NotImplementedException();
            return new List<Point>();
        }
    }
}
