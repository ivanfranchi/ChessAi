using MediatR;

namespace ChessAi.Domain.Pieces.Queries
{
    public class GetFenBoard : IRequest<string>
    {
        public GetFenBoard()
        {
        }
    }
}
