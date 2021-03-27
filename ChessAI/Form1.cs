using ChessAI.Domain.Management;
using ChessAI.Domain.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace ChessAI.Domain
{
    public partial class Form1 : Form
    {
        Board board;
        Random rnd;
        object guardClick = new object();

        public Form1()
        {
            InitializeComponent();

            rnd = new Random(DateTime.Now.Millisecond);    

            board = Board.GetBoard();
            UpdateStringedBoard();
            UpdateScore();
        }

        private void btnPopulate_Click(object sender, EventArgs e)
        {
            lock (guardClick)
            {
                for (int i = 0; i < 13; i++)
                {
                    try
                    {
                        var moves = board.GetPossibleMoves();
                        var move = PickMove(moves);
                        ExecuteFromToPoint(move[0], move[1]);
                        UpdateScore();
                        UpdateMovesCounter();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("From: ") //write down what happened
                        lblError.Text = ex.Message;
                        return;
                    }
                }
            }
        }

        private void UpdateScore()
        {
            lblScore.Text = board.EvaluateBoard().ToString();
        }

        private void UpdateMovesCounter()
        {
            lblMovesCounter.Text = board.MovesCounter.ToString();
        }

        /// <summary>
        /// Pick a move among the legal moves
        /// </summary>
        /// <param name="totalMoves">legal moves</param>
        /// <returns>A point to point movement</returns>
        private Point[] PickMove(Dictionary<Piece, List<Point>> totalMoves)
        {
            try
            {
                var moves = RemovePiecesWithoutMoves(totalMoves);

                var num = GetNum(0, moves.Keys.Count);
                var piece = moves.ElementAt(num);

                num = GetNum(0, piece.Value.Count);
                var move = piece.Value.ElementAt(num);

                return new Point[2] { piece.Key.Position, move };
            }
            catch
            {
                throw new Exception("No legal moves available");
            }
        }

        /// <summary>
        /// Removes the pieces with no legal moves
        /// </summary>
        /// <param name="moves">legal moves</param>
        /// <returns>A list with only pieces with a legal move available</returns>
        private Dictionary<Piece, List<Point>> RemovePiecesWithoutMoves(Dictionary<Piece, List<Point>> moves)
        {
            Dictionary<Piece, List<Point>> cleanDictionary = new Dictionary<Piece, List<Point>>();
            var cleaned = moves.Where(piece => piece.Value.Count > 0);

            foreach (var item in cleaned)
            {
                cleanDictionary.Add(item.Key, item.Value);
            }

            return cleanDictionary;
        }

        //Random, get a move
        private int GetNum(int min, int max)
        {
            return rnd.Next(min, max - 1);
        }

        //redraw the board from its stringed version from board
        private void UpdateStringedBoard()
        {
            var pieces = board.GetStringedBoard();
            string stringedBoard = "";
            string stringedPiece;

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    stringedPiece = pieces[j, i];
                    if (stringedPiece == null)
                    {
                        stringedBoard += "  ";
                    }
                    else
                    {
                        stringedBoard += stringedPiece;
                    }
                    stringedBoard += "  ";
                }
                stringedBoard += Environment.NewLine;
            }

            txtBoard.Text = stringedBoard;
        }

        //call the board.move
        private void ExecuteFromToPoint(Point toPosition, Point toGo)
        {
            try
            {
                Point from = toPosition;
                Point to = toGo;

                board.Move(from, to);
                lblError.Text = "";
                UpdateStringedBoard();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        /// <summary>
        /// Read from - to move from textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMove_Click(object sender, EventArgs e)
        {
            string move = txtMove.Text;

            try
            {
                string fromStr = move.Split('-')[0];
                int fromX = int.Parse(fromStr.Split('.')[0]);
                int fromY = int.Parse(fromStr.Split('.')[1]);
                Point from = new Point(fromX, fromY);

                string toStr = move.Split('-')[1];
                int toX = int.Parse(toStr.Split('.')[0]);
                int toY = int.Parse(toStr.Split('.')[1]);
                Point to = new Point(toX, toY);

                board.Move(from, to);
                lblError.Text = "";
                UpdateStringedBoard();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}
