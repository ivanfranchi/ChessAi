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

        private readonly ILogger<MovesController> _logger;
        private readonly IMediator _mediator;

        public MovesController(
            ILogger<MovesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("fen")]
        public Task<string> GetMoves(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Called GetMoves");
            var query = new GetFenBoard();
            return _mediator.Send(query, cancellationToken);
        }
    }
}
