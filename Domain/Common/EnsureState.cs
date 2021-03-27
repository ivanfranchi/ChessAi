using ChessAI.Domain.Pieces;
using System.Drawing;

namespace ChessAI.Domain.Common
{
    public static class EnsureState
    {
        internal static bool SameColor(this Piece first, Piece second)
        {
            return first.IsWhite == second.IsWhite;
        }

        internal static bool IsCastle(this Piece king, Piece rook)
        {
            if (king is King && rook is Rook
                && ((king.Position.X == 4 && rook.Position.X == 7)
                || king.Position.X == 4 && rook.Position.X == 0))
            {
                return true;
            }
            return false;
        }

        internal static bool IsPositionOnBoard(Point position)
        {
            return IsWithinLimits(position.X) && IsWithinLimits(position.Y);
        }

        private static bool IsWithinLimits(int coordinate)
        {
            return coordinate >= 0 && coordinate <= 7;
        }
    }
}
