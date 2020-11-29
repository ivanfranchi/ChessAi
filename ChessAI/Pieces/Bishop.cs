﻿using ChessAI.Common;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(
            bool isWhite,
            Point position)
            : base(
                isWhite,
                "B",
                position)
        { }

        public override List<Point> GetLegalMoves()
        {
            var list = new List<Point>();

            var incrementX = 1;
            var incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 1;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = +1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            return list;
        }

        /// <summary>
        /// Scan a direction for legal moves, fill in the list of legal moves by ref
        /// </summary>
        /// <param name="list">list to fill with found moves</param>
        /// <param name="incrementX">direction of search</param>
        /// <param name="incrementY">direction of search</param>
        /// <param name="currentPosition">current position from where to check the next position</param>
        private void GetMovesForDirection(ref List<Point> list, int incrementX, int incrementY, Point currentPosition)
        {
            Point nextPosition;
            ConflictType conflict;

            while (OperatorOverloading.Comparer(incrementX, currentPosition.X)
                && OperatorOverloading.Comparer(incrementY, currentPosition.Y))
            {
                nextPosition = new Point(
                    currentPosition.X + incrementX,
                    currentPosition.Y + incrementY);

                conflict = Board.CheckConflict(this, nextPosition);
                if (conflict == ConflictType.Ally)
                {
                    return;
                }
                if (conflict == ConflictType.Enemy)
                {
                    list.Add(nextPosition);
                    return;
                }
                if (conflict == ConflictType.None)
                {
                    list.Add(nextPosition);
                    currentPosition = nextPosition;
                }
            }
        }
    }
}