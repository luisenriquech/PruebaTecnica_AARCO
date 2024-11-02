using MediatR;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica_AARCO.Configuration;
using PruebaTecnica_AARCO.DTO.JWT;
using PruebaTecnica_AARCO.UseCase.JWT;

namespace PruebaTecnica_AARCO.Controllers
{
    [ApiController]
    [Route("JWT")]
    public class JWTController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JWTController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("PostToken")]
        public async Task<IActionResult> PostToken(usrJWTdto usuario)
        {
            GenericPresenter presenter = new();
            await _mediator.Send(new PostTokenCommand(usuario, presenter));
            if (presenter.Content.Error)
            {
                return StatusCode(401, presenter.Content);
            }
            return StatusCode(200, presenter.Content);
        }
    }
}
