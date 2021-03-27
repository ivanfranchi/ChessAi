using ChessAi.Domain.Pieces;
using ChessAI.Domain.Common;
using ChessAI.Domain.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChessAI.Domain.Management
{
    public class Board
    {
        static Board _board = null;
        public bool isWhiteTurn = true;
        public List<Piece> pieces;
        int movesCounter = 0;
        Dictionary<Piece, List<Point>> legalPiecesMoves = new Dictionary<Piece, List<Point>>();
        Dictionary<Piece, List<Point>> pastMoves; //history management

        public static Board GetBoard()
        {
            if (_board == null)
            {
                _board = new Board();
            }
            return _board;
        }

        IEnumerable<Point> enemyThreats = new List<Point>();

        public Board()
        {
            FillStartingBoard();

            pastMoves = new Dictionary<Piece, List<Point>>();
        }

        public string[,] GetStringedBoard()
        {
            return InterfaceData.GetStringedBoard(pieces);
        }

        public int MovesCounter => movesCounter;

        public double EvaluateBoard()
        {
            return InterfaceData.EvaluateBoard(pieces);
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
                throw new Exception(Messages.NoPieceAtStart);
            }

            //check if piece has legal moves
            if (!legalPiecesMoves.TryGetValue(pieceToMove, out var legalMovesForPiece) || legalMovesForPiece.Count == 0)
            {
                throw new Exception(Messages.NoLegalMoves);
            }

            //check if the move we are asking exists in the list of legal moves
            if (!legalMovesForPiece.IsLegal(toPosition))
            {
                throw new Exception(Messages.IllegalMove);
            }

            var pieceAtArrivalPosition = pieces.GetPieceByPosition(toPosition);
            bool isCastling = false;
            if (pieceAtArrivalPosition != null)
            {
                if (pieceToMove.SameColor(pieceAtArrivalPosition))
                {
                    isCastling = pieceToMove.IsCastle(pieceAtArrivalPosition);
                    if (isCastling)
                    {
                        pieceToMove.Position = new Point(6, pieceToMove.Position.Y);
                        pieceAtArrivalPosition.Position = new Point(5, pieceToMove.Position.Y);
                    }
                    else
                    {
                        isCastling = pieceToMove.IsCastle(pieceAtArrivalPosition);
                        if (isCastling)
                        {
                            pieceToMove.Position = new Point(2, pieceToMove.Position.Y);
                            pieceAtArrivalPosition.Position = new Point(3, pieceToMove.Position.Y);
                        }
                        else
                        {
                            throw new Exception(Messages.CantEatYourPiece);
                        }
                    }
                }
                else
                {
                    pieces.Remove(pieceAtArrivalPosition);
                }
            }

            //update piece current position
            if (!isCastling)
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

            UpdateStatus();
        }

        public Dictionary<Piece, List<Point>> GetPossibleMoves()
        {
            SetEnemyThreats(isWhiteTurn);
            SetLegalMoves(isWhiteTurn);

            return legalPiecesMoves;
        }

        public static ConflictType CheckConflict(Piece piece, Point arrivingPosition)
        {
            return PiecesMoves.CheckConflict(GetBoard().pieces, piece, arrivingPosition);
        }

        private void SetEnemyThreats(bool isTurnWhite)
        {
            enemyThreats = PiecesMoves.GetListOfThreats(pieces, !isTurnWhite);
        }

        private void SetLegalMoves(bool isTurnWhite)
        {
            legalPiecesMoves = PiecesMoves.GetMoves(pieces, isTurnWhite, enemyThreats, this);
        }

        public bool CanCastle(Piece king, bool isShort)
        {
            bool isWhite = king.IsWhite;
            int y = isWhite ? 0 : 7;
            int rookX = isShort ? 7 : 0;

            Piece tmpRook = pieces.GetPieceByPosition(new Point(rookX, y));
            //rook in corner and never moved
            if (tmpRook != null
                && tmpRook is Rook
                && !((Rook)tmpRook).HasMoved
                && king.IsWhite == tmpRook.IsWhite)
            {
                if (isShort)
                {
                    //empty way from bishop and knight
                    if (pieces.GetPieceByPosition(new Point(5, y)) == null
                        && pieces.GetPieceByPosition(new Point(6, y)) == null)
                    {
                        //f1/f7 and g1/g7 not under enemy check
                        if (!legalPiecesMoves.IsCoordinateUnderThreat(!isWhite, new Point(5, y))
                            && !legalPiecesMoves.IsCoordinateUnderThreat(!isWhite, new Point(6, y)))
                        {
                            return true;
                        }
                    }
                }
                else //long castle
                {
                    //empty way from queen bishop and knight
                    if (pieces.GetPieceByPosition(new Point(3, y)) == null
                        && pieces.GetPieceByPosition(new Point(2, y)) == null
                        && pieces.GetPieceByPosition(new Point(1, y)) == null)
                    {
                        //c1/c7 and d1/d7 not under enemy check
                        if (!legalPiecesMoves.IsCoordinateUnderThreat(!isWhite, new Point(3, y))
                            && !legalPiecesMoves.IsCoordinateUnderThreat(!isWhite, new Point(2, y)))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void CheckForWinner()
        {
            //count amount of moves, not pieces available
            //if (legalPieceMoves. == 0)
            //{
            //    //if king under check we have a winner,
            //    //otherwise stalemate
            //}
        }

        private void UpdateStatus()
        {
            movesCounter++;
            isWhiteTurn = !isWhiteTurn;
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
            pieces.Add(new Bishop(true, new Point(5, 0)));
            pieces.Add(new Knight(true, new Point(6, 0)));
            pieces.Add(new Rook(true, new Point(7, 0)));

            pieces.Add(new Pawn(true, new Point(0, 1)));
            pieces.Add(new Pawn(true, new Point(1, 1)));
            pieces.Add(new Pawn(true, new Point(2, 1)));
            pieces.Add(new Pawn(true, new Point(3, 1)));
            pieces.Add(new Pawn(true, new Point(4, 1)));
            pieces.Add(new Pawn(true, new Point(5, 1)));
            pieces.Add(new Pawn(true, new Point(6, 1)));
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
            //pieces.Add(new Rook(false, new Point(5, 5))); //the anti castle
            //pieces.Add(new Pawn(true, new Point(4, 4)));
        }
    }
}
