using ChessAI.Domain.Management;

namespace ChessAi.Domain.Common
{
    public static class FenManager
    {
        public static string DumpCurrentFen()
        {
            var rows = InterfaceData.GetPiecedBoard(Board.GetBoard().pieces);
            var output = "";
            
            var rowIndex = 8;

            for (int i = 7; i >= 0; i--)
            {
                rowIndex--;
                var blankSpaces = 0;
                var pieceInRow = false;

                for (int j = 0; j < 8; j++)
                {
                    var piece = rows[j, i];
                    if (piece == null)
                    {
                        blankSpaces++;
                    }
                    else
                    {
                        pieceInRow = true;
                        if (blankSpaces == 0)
                        {
                            output += piece.Name;
                        }
                        else
                        {
                            output += string.Format("{0}{1}", blankSpaces, piece.Name);
                            blankSpaces = 0;
                        }
                    }
                }

                if (!pieceInRow)
                {
                    output += blankSpaces;
                }

                if (rowIndex != 0)
                {
                    output += "/";
                }
            }

            return output;
        }
    }
}
