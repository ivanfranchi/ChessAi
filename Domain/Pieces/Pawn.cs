using ChessAI.Domain.Common;
using ChessAI.Domain.Management;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Domain.Pieces
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

        public override List<Point> GetMoves()
        {
            return GetMoves(true);
        }

        public override List<Point> GetCoveredSquares()
        {
            return GetMoves(false);
        }

        private List<Point> GetMoves(bool onlyLegals)
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
            EatLeftAndRight(ref list, Position, IsWhite, isEatingLeft: true, onlyLegals);
            //right
            EatLeftAndRight(ref list, Position, IsWhite, isEatingLeft: false, onlyLegals);

            //enpassant TODO
            EnPassant(ref list, Position, IsWhite);

            return list;
        }

        //To perform double step check first if step is legal then to step again
        private void OneOrDoubleStep(
            ref List<Point> list,
            Point currentPosition,
            bool isWhite, 
            bool isSingleStep)
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
                    if (IsPromoting(nextPosition.Y))
                    {

                    }
                    else
                    {
                        list.Add(nextPosition);
                    }
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

        private void EatLeftAndRight(
            ref List<Point> list,
            Point currentPosition,
            bool isWhite,
            bool isEatingLeft,
            bool onlyLegals)
        {
            int stepX = isEatingLeft ? -1 : +1;
            int stepY = isWhite ? 1 : -1;

            Point nextPosition = new Point(
                currentPosition.X + stepX,
                currentPosition.Y + stepY);

            ConflictType conflict = Board.CheckConflict(this, nextPosition);
            if (conflict == ConflictType.Enemy)
            {
                if (IsPromoting(nextPosition.Y))
                {

                }
                else
                {
                    list.Add(nextPosition);
                }
            }
            if (conflict == ConflictType.Ally && !onlyLegals)
            {
                list.Add(nextPosition);
            }
        }

        private void EnPassant(ref List<Point> list, Point currentPosition, bool isWhite)
        {
            //check if previous move was double step and pawn has arrived next to us...then check for conflicts etc
        }

        private bool IsPromoting(int nextPositionY)
        {
            return nextPositionY == 7 || nextPositionY == 0;
        }
    }
}
