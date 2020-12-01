using ChessAI.Common;
using ChessAI.Management;
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

            //enpassant TODO
            EnPassant(ref list, Position, IsWhite);

            return list;
        }

        //To perform double step check first if step is legal then to step again
        private void OneOrDoubleStep(ref List<Point> list, Point currentPosition, bool isWhite, bool isSingleStep)
        {
            var step = 1;
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
                if (isSingleStep)
                {
                    if (Promoting(isWhite, nextPosition.Y))
                    {
                        return; //stopping atm
                    }

                    list.Add(nextPosition);
                }
                else
                {
                    nextPosition = new Point(
                        currentPosition.X + 0,
                        currentPosition.Y + step + step);

                    conflict = Board.CheckConflict(this, nextPosition);
                    if (conflict == ConflictType.None)
                    {
                        list.Add(nextPosition);
                    }
                }
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
                //check for promotion...
                if (Promoting(isWhite, nextPosition.Y))
                {
                    return; //stopping atm
                }
                list.Add(nextPosition);
            }
        }

        private void EnPassant(ref List<Point> list, Point currentPosition, bool isWhite)
        {
            //check if previous move was double step and pawn has arrived next to us...then check for conflicts etc
        }

        private bool Promoting(bool isWhite, int nextPositionY)
        {
            return isWhite && nextPositionY == 7 || !isWhite && nextPositionY == 0;
        }
    }
}
