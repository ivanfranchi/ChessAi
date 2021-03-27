using ChessAI.Domain.Common;
using ChessAI.Domain.Management;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Domain.Pieces
{
    public class King : Piece
    {
        public King(
            bool isWhite,
            Point position)
            : base(
                isWhite,
                "K",
                position)
        {
            HasMoved = false;
        }

        public bool HasMoved { get; set; }

        public List<Point> GetKingMoves(Board board)
        {
            return GetMoves(board, true);
        }

        public override List<Point> GetCoveredSquares()
        {
            return GetMoves(null, false);
        }

        public override List<Point> GetMoves()
        {
            throw new System.NotImplementedException();
        }

        private List<Point> GetMoves(Board board, bool onlyLegals)
        {
            var list = new List<Point>();

            var incrementX = 0;
            var incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 1;
            incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 1;
            incrementY = 0;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 1;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = 0;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -1;
            incrementY = -1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -1;
            incrementY = 0;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            incrementX = -1;
            incrementY = 1;
            GetMovesForDirection(ref list, incrementX, incrementY, Position, onlyLegals);

            if (!HasMoved && onlyLegals)
            {
                Castle(ref list, board);
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

        private void Castle(ref List<Point> list, Board board)
        {
            if (board.CanCastle(this, true))
            {
                int y = IsWhite ? 0 : 7;
                list.Add(new Point(7, y));
            }
            if (board.CanCastle(this, false))
            {
                int y = IsWhite ? 0 : 7;
                list.Add(new Point(0, y));
            }
        }
    }
}
