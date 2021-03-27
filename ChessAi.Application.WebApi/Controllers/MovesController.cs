using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessAi.Domain.Pieces.Queries;
using MediatR;
using System.Threading;

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
            var query = new GetFenBoard();
            return _mediator.Send(query, cancellationToken);
        }
    }
}
