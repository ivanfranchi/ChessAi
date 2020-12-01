﻿using ChessAI.Common;
using ChessAI.Management;
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
            var list = new List<Point>();

            var incrementX = 0;
            var incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 1;
            incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 1;
            incrementY = 0;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 1;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 0;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = 0;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            if(!HasMoved)
            {
                Castle(ref list, Position);
            }

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

            nextPosition = new Point(
                currentPosition.X + incrementX,
                currentPosition.Y + incrementY);

            if (!OperatorOverloading.IsPositionOnBoard(nextPosition))
            {
                return;
            }

            conflict = Board.CheckConflict(this, nextPosition);
            if (conflict == ConflictType.Enemy)
            {
                list.Add(nextPosition);
            }
            if (conflict == ConflictType.None)
            {
                list.Add(nextPosition);
            }
        }

        private void Castle(ref List<Point> list, Point currentPosition)
        {
            //if empty room
            //if no checks on the way

            //add castle move
        }
    }
}
