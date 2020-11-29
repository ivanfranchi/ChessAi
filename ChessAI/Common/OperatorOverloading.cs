using System;
using System.Drawing;

namespace ChessAI.Common
{
    public static class OperatorOverloading
    {
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
