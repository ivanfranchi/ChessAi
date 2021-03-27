using ChessAi.Domain.Pieces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChessAi.Application.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovesController : ControllerBase
    {
        protected IActionResult Json(object value)
        {
            return new JsonResult(value);
        }

        private static MovesController _movesController;
        private readonly ILogger<MovesController> _logger;
        private readonly IMediator _mediator;

        public MovesController(
            ILogger<MovesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _movesController = this;
        }

        public static MovesController GetInstance()
        {
            if (_movesController == null)
            {
                _movesController = new MovesController(default, default);
            }
            return _movesController;
        }

        [HttpGet("fen")]
        public Task<string> GetMoves(CancellationToken cancellationToken)
        {
            var query = new GetFenBoard();
            return _mediator.Send(query, cancellationToken);
        }
    }
}
