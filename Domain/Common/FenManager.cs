using ChessAI.Domain.Management;

namespace ChessAi.Domain.Common
{
    public static class FenManager
    {
        public static string DumpCurrentFen()
        {
            var rows = InterfaceData.GetPiecedBoard(Board.GetBoard().pieces);
            var output = "";
            
            var rowIndex = 0;

            for (int i = 0; i < 8; i++)
            {
                rowIndex++;
                var blankSpaces = 0;

                for (int j = 0; j < 8; j++)
                {
                    var piece = rows[i, j];
                    if (piece == null)
                    {
                        blankSpaces++;
                    }
                    else
                    {
                        output += blankSpaces == 0
                            ? piece.Name
                            : string.Format("{0}{1}", blankSpaces, piece.Name);
                        blankSpaces = 0;
                    }

                    if (blankSpaces > 0)
                    {
                        output += blankSpaces;
                    }
                }

                if (rowIndex != 8)
                {
                    output += "/";
                }
            }

            return output;
        }
    }
}
