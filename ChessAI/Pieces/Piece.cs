using System.Collections.Generic;
using System.Drawing;

namespace ChessAI.Pieces
{
    public abstract class Piece
    {
        public Piece(
            bool isWhite,
            string name,
            Point position)
        {
            IsWhite = isWhite;
            Name = name;
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

        public abstract List<Point> GetLegalMoves();
        
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
