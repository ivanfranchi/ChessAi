using ChessAI.Pieces;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChessAI.Common
{
    public static class GettersLinq
    {
        public static Piece GetPieceByPosition(this List<Piece> pieces, Point position)
        {
            return pieces.SingleOrDefault(atPosition => atPosition.Position == position);
        }

        public static Point GetMoveByCoordinates(this List<Point> moves, Point coordinate)
        {
            return moves.SingleOrDefault(move => move.X == coordinate.X && move.Y == coordinate.Y);
        }
    }
}
