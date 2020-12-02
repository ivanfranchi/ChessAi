using ChessAI.Common;
using ChessAI.Management;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
{
    public class Rook: Piece
    {
        public Rook(
            bool isWhite,
            Point position)
            : base(
                isWhite,
                "R",
                position)
        {
            HasMoved = false;
        }

        public bool HasMoved { get; set; }

        public override List<Point> GetLegalMoves(Board board)
        {
            var list = new List<Point>();

            var incrementX = 0;
            var incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 1;
            incrementY = 0;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = 0;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position);

            incrementX = -1;
            incrementY = 0;
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

            while (true)
            {
                nextPosition = new Point(
                    currentPosition.X + incrementX,
                    currentPosition.Y + incrementY);

                if (!OperatorOverloading.IsPositionOnBoard(nextPosition))
                {
                    return;
                }

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
