using ChessAI.Common;
using ChessAI.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChessAI
{
    public class Board
    {
        static List<Piece> pieces;
        Dictionary<Piece, List<Point>> moves;

        public Board()
        {
            FillStartingBoard();
        }

        /// <summary>
        /// Move a piece to a new position, remove existing piece if any
        /// </summary>
        /// <param name="piece">The piece to move</param>
        /// <param name="toPosition">The arrival position</param>
        public void Move(Point fromPosition, Point toPosition)
        {
            var pieceToMove = pieces.SingleOrDefault(atPosition => atPosition.Position == fromPosition);
            if (pieceToMove == null)
            {
                throw new Exception("No piece at starting position");
            }

            //remove if something at arrival position
            var pieceToRemove = GetPieceByPosition(toPosition);
            if (pieceToRemove != null)
            {
                pieces.Remove(pieceToRemove);
            }

            //update piece position
            pieceToMove.Position = toPosition;
        }

        /// <summary>
        /// Get a string representation of the board
        /// </summary>
        /// <returns>8*8 string matrix of the board</returns>
        public string[,] GetStringedBoard()
        {
            var board = new string[8, 8];
            Point position;
            foreach (var piece in pieces)
            {
                position = piece.Position;
                board[position.X, position.Y] = piece.Name;
            }

            return board;
        }

        public Dictionary<Piece, List<Point>> GetMoves()
        {
            moves = new Dictionary<Piece, List<Point>>();

            foreach (var piece in pieces)
            {
                moves.Add(piece, piece.GetLegalMoves());
            }

            return moves;
        }



        /// <summary>
        /// Ch
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="nextPosition"></param>
        /// <returns></returns>
        public static ConflictType CheckConflict(Piece piece, Point nextPosition)
        {
            var pieceInNextPosition = GetPieceByPosition(nextPosition);
            if (pieceInNextPosition == null)
            {
                return ConflictType.None;
            }

            if ((piece.IsWhite && pieceInNextPosition.IsWhite) || (!piece.IsWhite && !pieceInNextPosition.IsWhite))
            {
                return ConflictType.Ally;
            }

            return ConflictType.Enemy;
        }

        /// <summary>
        /// Fill board with pieces
        /// </summary>
        private void FillStartingBoard()
        {
            pieces = new List<Piece>();

            //whites
            pieces.Add(new Rook(true, new Point(0, 0), false));
            pieces.Add(new Knight(true, new Point(1, 0)));
            pieces.Add(new Bishop(true, new Point(2, 0)));
            pieces.Add(new Queen(true, new Point(3, 0)));
            pieces.Add(new King(true, new Point(4, 0), false));
            pieces.Add(new Bishop(true, new Point(5, 0)));
            pieces.Add(new Knight(true, new Point(6, 0)));
            pieces.Add(new Rook(true, new Point(7, 0), false));

            pieces.Add(new Pawn(true, new Point(0, 1)));
            pieces.Add(new Pawn(true, new Point(1, 1)));
            pieces.Add(new Pawn(true, new Point(2, 1)));
            pieces.Add(new Pawn(true, new Point(3, 1)));
            pieces.Add(new Pawn(true, new Point(4, 1)));
            pieces.Add(new Pawn(true, new Point(5, 1)));
            pieces.Add(new Pawn(true, new Point(6, 1)));
            pieces.Add(new Pawn(true, new Point(7, 1)));

            //blacks
            pieces.Add(new Rook(false, new Point(0, 7), false));
            pieces.Add(new Knight(false, new Point(1, 7)));
            pieces.Add(new Bishop(false, new Point(2, 7)));
            pieces.Add(new Queen(false, new Point(3, 7)));
            pieces.Add(new King(false, new Point(4, 7), false));
            pieces.Add(new Bishop(false, new Point(5, 7)));
            pieces.Add(new Knight(false, new Point(6, 7)));
            pieces.Add(new Rook(false, new Point(7, 7), false));

            pieces.Add(new Pawn(false, new Point(0, 6)));
            pieces.Add(new Pawn(false, new Point(1, 6)));
            pieces.Add(new Pawn(false, new Point(2, 6)));
            pieces.Add(new Pawn(false, new Point(3, 6)));
            pieces.Add(new Pawn(false, new Point(4, 6)));
            pieces.Add(new Pawn(false, new Point(5, 6)));
            pieces.Add(new Pawn(false, new Point(6, 6)));
            pieces.Add(new Pawn(false, new Point(7, 6)));

            //testsss
            //pieces.Add(new Pawn(true, new Point(4, 4)));
        }

        private static Piece GetPieceByPosition(Point position)
        {
            return pieces.SingleOrDefault(atPosition => atPosition.Position == position);
        }
    }
}
