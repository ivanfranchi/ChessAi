using ChessAI.Common;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(
            bool isWhite,
            Point position)
            : base(
                isWhite,
                "P",
                position)
        { }

        public override List<Point> GetLegalMoves()
        {
            var list = new List<Point>();

            //one step !! promote?!
            OneOrDoubleStep(ref list, Position, IsWhite, isSingleStep: true);

            //double step if starting position for white / black
            if ((IsWhite && Position.Y == 1) || (!this.IsWhite && Position.Y == 6))
            {
                OneOrDoubleStep(ref list, Position, IsWhite, isSingleStep: false);
            }

            //left
            EatLeftAndRight(ref list, Position, IsWhite, isEatingLeft: true);
            //right
            EatLeftAndRight(ref list, Position, IsWhite, isEatingLeft: false);

            //enpassant
            EnPassant(ref list, Position, IsWhite);

            return list;
        }

        private void OneOrDoubleStep(ref List<Point> list, Point currentPosition, bool isWhite, bool isSingleStep)
        {
            int step = isSingleStep ? 1 : 2;

            if (!isWhite)
            {
                step *= -1;
            }

            Point nextPosition = new Point(
                currentPosition.X + 0,
                currentPosition.Y + step);

            ConflictType conflict = Board.CheckConflict(this, nextPosition);
            if (conflict == ConflictType.None)
            {
                list.Add(nextPosition);
            }
        }

        private void EatLeftAndRight(ref List<Point> list, Point currentPosition, bool isWhite, bool isEatingLeft)
        {
            int stepX = isEatingLeft ? -1 : +1;
            int stepY = isWhite ? 1 : -1;

            Point nextPosition = new Point(
                currentPosition.X + stepX,
                currentPosition.Y + stepY);

            ConflictType conflict = Board.CheckConflict(this, nextPosition);
            if (conflict == ConflictType.Enemy)
            {
                list.Add(nextPosition);
            }
        }

        private void EnPassant(ref List<Point> list, Point currentPosition, bool isWhite)
        {
            //check if previous move was double step and pawn has arrived next to us...then check for conflicts etc
        }
    }
}
