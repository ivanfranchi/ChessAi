using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessAI
{
    public partial class Form1 : Form
    {
        Board board;
        public Form1()
        {
            InitializeComponent();

            board = new Board();
            ShowBoard();
        }

        private void btnPopulate_Click(object sender, EventArgs e)
        {
            //ShowBoard();

            board.GetMoves();
        }

        private void ShowBoard()
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
                        stringedBoard += " ";
                    }
                    else
                    {
                        stringedBoard += stringedPiece;
                    }
                    stringedBoard += " ";
                }
                stringedBoard += Environment.NewLine;
            }

            txtBoard.Text = stringedBoard;
        }

        /// <summary>
        /// Read from - to move
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
                ShowBoard();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}
