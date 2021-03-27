using ChessAi.Domain.Common;
using ChessAi.Domain.Pieces.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChessAi.Application.QueryHandlers
{
    public class GetFenBoardHandler : IRequestHandler<GetFenBoard, string>
    {
        public async Task<string> Handle(GetFenBoard request, CancellationToken cancellationToken)
        {
            return FenManager.DumpCurrentFen();
        }
    }
}
