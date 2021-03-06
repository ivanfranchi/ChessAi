﻿using ChessAI.Domain.Common;
using ChessAI.Domain.Management;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Domain.Pieces
{
    public class Knight : Piece
    {
        public Knight(
            bool isWhite,
            Point position)
            : base(
                isWhite,
                "N",
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

        public List<Point> GetMoves(bool onlyLegals)
        {
            var list = new List<Point>();

            var incrementX = 1;
            var incrementY = 2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 2;
            incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 2;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 1;
            incrementY = -2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -1;
            incrementY = -2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -2;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -2;
            incrementY = +1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -1;
            incrementY = 2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            return list;
        }

        /// <summary>
        /// Scan a direction for legal moves, fill in the list of legal moves by ref
        /// </summary>
        /// <param name="list">list to fill with found moves</param>
        /// <param name="incrementX">direction of search</param>
        /// <param name="incrementY">direction of search</param>
        /// <param name="currentPosition">current position from where to check the next position</param>
        private void GetMovesForDirection(
            ref List<Point> list,
            int incrementX,
            int incrementY, 
            Point currentPosition,
            bool onlyLegals)
        {
            Point nextPosition;
            ConflictType conflict;

            nextPosition = new Point(
                currentPosition.X + incrementX,
                currentPosition.Y + incrementY);

            if (!EnsureState.IsPositionOnBoard(nextPosition))
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
            if (conflict == ConflictType.Ally && !onlyLegals)
            {
                list.Add(nextPosition);
            }
        }
    }
}
