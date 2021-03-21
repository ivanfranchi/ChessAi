using ChessAI.Pieces;
using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Management
{
    public class InterfaceData
    {
        /// <summary>
        /// Get a string representation of the board
        /// </summary>
        /// <returns>8*8 string matrix of the board</returns>
        public static string[,] GetStringedBoard(List<Piece> pieces)
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

        /// <summary>
        /// Return the evaluation of alive pieces
        /// </summary>
        /// <param name="pieces">Pieces on board</param>
        /// <returns>Sum of current state (whites +, blacks -)</returns>
        public static double EvaluateBoard(List<Piece> pieces)
        {
            var score = 0.0;

            foreach (var piece in pieces)
            {
                score += piece.Score;
            }

            return score;
        }

        /// <summary>
        /// Upper case for whites and lower case for blacks
        /// </summary>
        /// <param name="piece">Piece to get the name from</param>
        /// <returns>Name of the piece distinguishing black and white</returns>
        private static string GetCasedName(Piece piece)
        {
            if (piece.IsWhite)
            {
                return piece.Name;
            }
            else
            {
                return piece.Name.ToLower() + ".";
            }
        }
    }
}
