using ChessAI.Common;
using ChessAI.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAI.Management
{
    /// <summary>
    /// Evaluate moves
    /// </summary>
    public class PiecesMoves
    {
        /// <summary>
        /// Get the list of moves for the opponent (included his ally covered pieces).
        /// This list prevent the king in doing illegal moves
        /// </summary>
        /// <param name="pieces">All pieces</param>
        /// <param name="isEnemy">Enemy colour</param>
        /// <returns>Moves for the enemy</returns>
        public static List<Point> GetListOfThreats(IEnumerable<Piece> pieces, bool isEnemy)
        {
            var listOfThreats = new List<Point>();
            foreach (var piece in pieces)
            {
                if (piece.IsWhite == isEnemy)
                {
                    listOfThreats.AddRange(piece.GetCoveredSquares());
                }
            }
            return listOfThreats;
        }

        public static Dictionary<Piece, List<Point>> GetMoves(
            List<Piece> pieces,
            bool isWhite,
            IEnumerable<Point> enemyThreats, 
            Board board)
        {
            var legalPieceMoves = new Dictionary<Piece, List<Point>>();
            foreach (var piece in pieces)
            {
                if (piece.IsWhite == isWhite)
                {
                    if (piece is King)
                    {
                        var moves = ((King)piece).GetKingMoves(board);
                        var legalMoves = new List<Point>();

                        foreach (var move in moves)
                        {
                            if (!enemyThreats.Contains(move))
                            {
                                legalMoves.Add(move);
                            }
                        }
                        legalPieceMoves.Add(piece, legalMoves);
                    }
                    else
                    {
                        legalPieceMoves.Add(piece, piece.GetMoves());
                    }
                }
            }
            return legalPieceMoves;
        }

        /// <summary>
        /// Check what is in place at the arriving position
        /// </summary>
        /// <param name="piece">piece getting moved</param>
        /// <param name="arrivingPosition">arriving position to test</param>
        /// <returns>Conflict type. What we find at arrival position</returns>
        public static ConflictType CheckConflict(List<Piece> pieces, Piece piece, Point arrivingPosition)
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
    }
}
