using ChessAI.Common;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
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

        public override List<Point> GetLegalMoves()
        {
            var list = new List<Point>();

            var incrementX = 1;
            var incrementY = 2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 2;
            incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 2;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 1;
            incrementY = -2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = -2;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -2;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -2;
            incrementY = +1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = 2;
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

            nextPosition = new Point(
                currentPosition.X + incrementX,
                currentPosition.Y + incrementY);

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
    }
}
