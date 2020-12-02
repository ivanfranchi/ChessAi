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

        public static bool IsLegal(
            this List<Point> moves,
            Point coordinate)
        {
            return moves.Contains(coordinate);
        }

        public static bool IsCoordinateUnderThreat(
            this Dictionary<Piece, List<Point>> moves,
            bool isWhiteThreatener,
            Point coordinate)
        {
            var movesForColor = moves.Where(piece => piece.Key.IsWhite == isWhiteThreatener);
            List<Point> listThreats = new List<Point>();
            foreach (var piece in movesForColor)
            {
                listThreats.AddRange(piece.Value);
            }
            return listThreats.Contains(coordinate);
        }
    }
}
