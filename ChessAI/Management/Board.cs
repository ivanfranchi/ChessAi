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
            if (!moves.TryGetValue(pieceToMove, out var legalMovesForPiece) || legalMovesForPiece.Count == 0)
            {
                throw new Exception("No legal moves for piece.");
            }

            //check if the move we are asking exists in the list of legal moves
            if (!legalMovesForPiece.IsLegal(toPosition))
            {
                throw new Exception("Move is not legal, if you think this is an error ...");
            }

            var pieceAtArrivalPosition = pieces.GetPieceByPosition(toPosition);
            bool isCastle = false;
            if (pieceAtArrivalPosition != null)
            {
                if (pieceToMove.IsWhite == pieceAtArrivalPosition.IsWhite)
                {
                    isCastle = IsShortCastle(pieceToMove, pieceAtArrivalPosition);
                    if (isCastle)
                    {
                        pieceToMove.Position = new Point(6, pieceToMove.Position.Y);
                        pieceAtArrivalPosition.Position = new Point(5, pieceToMove.Position.Y);
                    }
                    else
                    {
                        //isCastle = IsLongCastle(pieceToMove, pieceToRemove);
                        //if (isCastle)
                        //{
                        //    pieceToMove.Position = new Point(6, pieceToMove.Position.Y);
                        //    pieceToRemove.Position = new Point(5, pieceToMove.Position.Y);
                        //}
                        //else
                        //{
                        throw new Exception("You can't eat your own pieces.");
                        //}
                    }
                }
                else
                {
                    pieces.Remove(pieceAtArrivalPosition);
                }
            }

            //update piece current position
            if (!isCastle)
            {
                pieceToMove.Position = toPosition;
            }

            if (pieceToMove is King)
            {
                ((King)pieceToMove).HasMoved = true;
            }
            if (pieceToMove is Rook)
            {
                ((Rook)pieceToMove).HasMoved = true;
            }
            movesCounter++;
        }

        public bool CanCastleShort(Piece king)
        {
            bool isWhite = king.IsWhite;
            int y = isWhite ? 0 : 7;

            Piece tmpRook = pieces.GetPieceByPosition(new Point(7, y));
            //rook in corner and never moved
            if (tmpRook != null
                && tmpRook is Rook
                && !((Rook)tmpRook).HasMoved
                && king.IsWhite == tmpRook.IsWhite)
            {
                //empty way from bishop and knight
                if (pieces.GetPieceByPosition(new Point(5, y)) == null
                    && pieces.GetPieceByPosition(new Point(6, y)) == null)
                {
                    //f1/f7 or g2/g7 not under enemy check
                    if (!moves.IsCoordinateUnderThreat(!isWhite, new Point(5, y))
                        && !moves.IsCoordinateUnderThreat(!isWhite, new Point(6, y)))
                    {
                        return true;
                    }
                }
            }

            return false;
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

        public int MovesCounter => movesCounter;

        public Dictionary<Piece, List<Point>> GetMoves(bool isWhite)
        {
            moves = new Dictionary<Piece, List<Point>>();
            List<Piece> kings = new List<Piece>();

            foreach (var piece in pieces)
            {
                //check king moves as last thing (check for checks)
                if (piece is King)
                {
                    kings.Add(piece);
                    continue;
                }
                moves.Add(piece, piece.GetLegalMoves(this));
            }
            foreach (var piece in kings)
            {
                moves.Add(piece, piece.GetLegalMoves(this));
            }

            Dictionary<Piece, List<Point>> retArray = new Dictionary<Piece, List<Point>>();
            foreach (var move in moves)
            {
                if (move.Key.IsWhite == isWhite)
                {
                    retArray.Add(move.Key, move.Value);
                }
            }

            return retArray;
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
            pieces.Add(new Rook(true, new Point(0, 0)));
            pieces.Add(new Knight(true, new Point(1, 0)));
            pieces.Add(new Bishop(true, new Point(2, 0)));
            pieces.Add(new Queen(true, new Point(3, 0)));
            pieces.Add(new King(true, new Point(4, 0)));
            //pieces.Add(new Bishop(true, new Point(5, 0)));
            //pieces.Add(new Knight(true, new Point(6, 0)));
            pieces.Add(new Rook(true, new Point(7, 0)));

            pieces.Add(new Pawn(true, new Point(0, 1)));
            pieces.Add(new Pawn(true, new Point(1, 1)));
            pieces.Add(new Pawn(true, new Point(2, 1)));
            pieces.Add(new Pawn(true, new Point(3, 1)));
            pieces.Add(new Pawn(true, new Point(4, 1)));
            //pieces.Add(new Pawn(true, new Point(5, 1)));
            //pieces.Add(new Pawn(true, new Point(6, 1)));
            pieces.Add(new Pawn(true, new Point(7, 1)));

            //blacks
            pieces.Add(new Rook(false, new Point(0, 7)));
            pieces.Add(new Knight(false, new Point(1, 7)));
            pieces.Add(new Bishop(false, new Point(2, 7)));
            pieces.Add(new Queen(false, new Point(3, 7)));
            pieces.Add(new King(false, new Point(4, 7)));
            pieces.Add(new Bishop(false, new Point(5, 7)));
            pieces.Add(new Knight(false, new Point(6, 7)));
            pieces.Add(new Rook(false, new Point(7, 7)));

            pieces.Add(new Pawn(false, new Point(0, 6)));
            pieces.Add(new Pawn(false, new Point(1, 6)));
            pieces.Add(new Pawn(false, new Point(2, 6)));
            pieces.Add(new Pawn(false, new Point(3, 6)));
            pieces.Add(new Pawn(false, new Point(4, 6)));
            pieces.Add(new Pawn(false, new Point(5, 6)));
            pieces.Add(new Pawn(false, new Point(6, 6)));
            pieces.Add(new Pawn(false, new Point(7, 6)));

            //testsss
            pieces.Add(new Rook(false, new Point(5, 5))); //the anti castle
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

        private bool IsShortCastle(Piece king, Piece rook)
        {
            if (king is King
                && rook is Rook
                && king.Position.X == 4
                && rook.Position.X == 7)
            {
                return true;
            }
            return false;
        }
    }
}
