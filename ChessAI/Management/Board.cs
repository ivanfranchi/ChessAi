using ChessAI.Common;
using ChessAI.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChessAI.Management
{
    public class Board
    {
        public bool isWhiteTurn = true;
        static List<Piece> pieces;
        int movesCounter = 0;
        Dictionary<Piece, List<Point>> moves;
        Dictionary<Piece, List<Point>> pastMoves; //history management

        public Board()
        {
            FillStartingBoard();

            pastMoves = new Dictionary<Piece, List<Point>>();
        }

        /// <summary>
        /// Move a piece to a new position, remove existing piece if any
        /// </summary>
        /// <param name="piece">The piece to move</param>
        /// <param name="toPosition">The arrival position</param>
        public void Move(Point fromPosition, Point toPosition)
        {
            //check if piece to move exists
            var pieceToMove = pieces.SingleOrDefault(atPosition => atPosition.Position == fromPosition);
            if (pieceToMove == null)
            {
                throw new Exception("No piece at starting position.");
            }

            //check if piece has legal moves
            if(!moves.TryGetValue(pieceToMove, out var legalMoves) || legalMoves.Count == 0)
            {
                throw new Exception("No legal moves for piece.");
            }

            //check if the move we are asking exists in the list of legal moves
            var move = legalMoves.GetMoveByCoordinates(toPosition);

            //remove if something at arrival position
            var pieceToRemove = pieces.GetPieceByPosition(toPosition);
            if (pieceToRemove != null)
            {
                pieces.Remove(pieceToRemove);
            }

            //update piece current position
            pieceToMove.Position = toPosition;

            movesCounter++;
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
                board[position.X, position.Y] = GetCasedName(piece);
            }

            return board;
        }

        public int GetMovesCounter()
        {
            return movesCounter;
        }

        public Dictionary<Piece, List<Point>> GetMoves(bool color)
        {
            moves = new Dictionary<Piece, List<Point>>();

            foreach (var piece in pieces)
            {
                if(piece.IsWhite == color)
                {
                    moves.Add(piece, piece.GetLegalMoves());
                }
            }

            return moves;
        }

        public double EvaluateBoard()
        {
            var score = 0.0;

            foreach (var piece in pieces)
            {
                score += piece.Score;
            }

            return score;
        }

        /// <summary>
        /// Check what is in place at the arriving position
        /// </summary>
        /// <param name="piece">piece getting moved</param>
        /// <param name="arrivingPosition">arriving position to test</param>
        /// <returns>Conflict type. What we find at arrival position</returns>
        public static ConflictType CheckConflict(Piece piece, Point arrivingPosition)
        {
            var pieceInNextPosition = pieces.GetPieceByPosition(arrivingPosition);
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

        /// <summary>
        /// Upper case for whites and lower case for blacks
        /// </summary>
        /// <param name="piece">Piece to get the name from</param>
        /// <returns>Name of the piece distinguishing black and white</returns>
        private string GetCasedName(Piece piece)
        {
            if (piece.IsWhite)
            {
                return piece.Name;
            }
            else
            {
                return piece.Name.ToLower();
            }
        }
    }
}
