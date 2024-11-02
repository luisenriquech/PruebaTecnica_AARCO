using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnica_AARCO.Configuration;
using PruebaTecnica_AARCO.DTO.General;
using PruebaTecnica_AARCO.DTO.RequestBody;
using PruebaTecnica_AARCO.Models;
using PruebaTecnica_AARCO.Negocio;
using PruebaTecnica_AARCO.Validaciones.Mensajes;
using PruebaTecnica_AARCO.Validaciones.Request;

namespace PruebaTecnica_AARCO.UseCase
{
    public class PostRequestCommand : IInPutPort<Tuple<requestBodyDto, int>, SuccessResult>
    {
        public Tuple<requestBodyDto, int> RequestData { get; }

        public IOutPutPort<SuccessResult> OutPutPort { get; }

        public PostRequestCommand(Tuple<requestBodyDto, int> req, IOutPutPort<SuccessResult> outPutPort)
        {
            RequestData = req;
            OutPutPort = outPutPort;
        }

        public class PostRequestHandler : AsyncRequestHandler<PostRequestCommand>
        {
            public IConfiguration _configuration;
            private readonly IMapper _mapper;

            public PostRequestHandler(IMapper mapper, IConfiguration configuration)
            {
                _mapper = mapper;
                _configuration = configuration;
            }

            protected async override Task Handle(PostRequestCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    #region "Validación de estructura de datos"
                    var validator = new PostRequestValidation();
                    ValidationResult result = validator.Validate(request.RequestData.Item1);
                    #endregion
                    #region "Si la validación fue correcta" 
                    if (result.IsValid)
                    {
                        #region "Llamado al SP creado en la BD"
                        DataContext _context = new DataContext();
                        var Filtro = _context.uspRequest.FromSqlInterpolated($"exec [dbo].[uspRequest] @vOpcion = {request.RequestData.Item1.filterType}, @vString = {request.RequestData.Item1.String},@vPagina = {(request.RequestData.Item2 > 0 ? request.RequestData.Item2 : 1) }").ToList();
                        #endregion
                        #region "Si el SP con los datos proporcionados regresa 1 o más resultados"
                        if (Filtro != null && Filtro.Count > 0)
                        {
                            request.OutPutPort.Handle(new SuccessResult((request.RequestData.Item2 > 0 ? request.RequestData.Item2 : 1), Filtro));
                        }
                        #endregion
                        #region "Si no regresa resultados"
                        else
                        {
                            request.OutPutPort.Handle(new SuccessResult(true, Utils.MensajesGenerales(9)));
                        }
                        #endregion
                    }
                    #endregion
                    #region "Si la validación no fue correcta"
                    else
                    {
                        request.OutPutPort.Handle(new SuccessResult(true, result.Errors, Utils.MensajesGenerales(2)));
                    }
                    #endregion
                }

                catch (Exception ex)
                {
                    request.OutPutPort.Handle(new SuccessResult(true, Utils.MensajesGenerales(1) + ex.Message));
                }
            }
        }
    }
}
