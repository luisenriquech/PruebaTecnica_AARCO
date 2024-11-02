using FluentValidation;
using PruebaTecnica_AARCO.DTO.RequestBody;
using PruebaTecnica_AARCO.Validaciones.Mensajes;

namespace PruebaTecnica_AARCO.Validaciones.Request
{
    public class PostRequestValidation : AbstractValidator<requestBodyDto>
    {
        public PostRequestValidation()
        {
            RuleFor(x => x.String).Cascade(CascadeMode.Stop).NotNull().WithMessage(Utils.MensajesValidacion("String", 1) + Utils.Sugerencia(4)).NotEmpty().WithMessage(Utils.MensajesValidacion("String", 2) + Utils.Sugerencia(4)).Matches(RegularExpressions.stringValidation).WithMessage((Utils.MensajesValidacion("String", 3) + Utils.Sugerencia(4)));        
        }
    }
}
