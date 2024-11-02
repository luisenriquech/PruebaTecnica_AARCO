using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica_AARCO.Configuration;
using PruebaTecnica_AARCO.DTO.General;
using PruebaTecnica_AARCO.DTO.RequestBody;
using PruebaTecnica_AARCO.Negocio;
using PruebaTecnica_AARCO.UseCase;
using System.ComponentModel;
using System.Security.Claims;

namespace PruebaTecnica_AARCO.Controllers
{
    
    [ApiController]
    [Route("dasboard")]
    public class InformacionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InformacionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("{pagina:int}")]
        public async Task<IActionResult> request([FromBody] requestBodyDto requestBodyDto, int pagina)
        {
            #region "Verificación del token"
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = JWTFunctions.validateToken(identity);
            GenericPresenter presenter = new();
            #endregion
            #region "Si el token es correcto"
            if (rToken == rToken)
            {

                Tuple<requestBodyDto, int> req = new Tuple<requestBodyDto, int>(requestBodyDto, pagina);
                await _mediator.Send(new PostRequestCommand(req, presenter));
                if (presenter.Content.Error)
                {
                    return StatusCode(409, presenter.Content);
                }
                return StatusCode(201, presenter.Content);
            }
            #endregion
            #region "Si el token no es correcto"
            else
            {
                return StatusCode(402, new SuccessResult(true, "Credenciales incorrectas"));
            }
            #endregion
        }

        
        [HttpPost]
        public async Task<IActionResult> request([FromBody] requestBodyDto requestBodyDto)
        {
            #region "Verificación del token"
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = JWTFunctions.validateToken(identity);
            GenericPresenter presenter = new();
            #endregion
            #region "Si el token es correcto"
            if (rToken == rToken)
            {

                Tuple<requestBodyDto, int> req = new Tuple<requestBodyDto, int>(requestBodyDto, 1);
                await _mediator.Send(new PostRequestCommand(req, presenter));
                if (presenter.Content.Error)
                {
                    return StatusCode(409, presenter.Content);
                }
                return StatusCode(201, presenter.Content);
            }
            #endregion
            #region "Si el token no es correcto"
            else
            {
                return StatusCode(402, new SuccessResult(true, "Credenciales incorrectas"));
            }
            #endregion
        }
    }
}
