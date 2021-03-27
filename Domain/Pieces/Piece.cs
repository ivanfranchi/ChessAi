using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Domain.Pieces
{
    public abstract class Piece
    {
        public Piece(
            bool isWhite,
            string name,
            Point position)
        {
            IsWhite = isWhite;
            Name = isWhite ? name : name.ToLower();
            Position = position;

            GiveScore(name);
        }

        public bool IsWhite { get; set; }
        /// <summary>
        /// K: king
        /// Q: Queen
        /// R: Rook
        /// B: Bishop
        /// N: Knight
        /// P: Pawn
        /// </summary>
        public string Name { get; set; }
        public Point Position { get; set; }
        public double Score { get; private set; }

        /// <summary>
        /// Moves allowed for a given piece
        /// </summary>
        /// <param name="board">Reference to the knowledge</param>
        /// <returns>List of moves allowed for the piece</returns>
        public abstract List<Point> GetMoves();

        /// <summary>
        /// Covered squares contains all the legal moves for a piece + the ally pieces covered by this piece
        /// </summary>
        /// <returns>List of covered squares</returns>
        public abstract List<Point> GetCoveredSquares();
        
        private void GiveScore(string name)
        {
            switch (name)
            {
                case "K":
                    Score = 1000;
                    break;
                case "Q":
                    Score = 9;
                    break;
                case "R":
                    Score = 5;
                    break;
                case "B":
                    Score = 3;
                    break;
                case "N":
                    Score = 3;
                    break;
                case "P":
                    Score = 1;
                    break;
            }

            if (!IsWhite)
            {
                Score *= -1;
            }
        }
    }
}
