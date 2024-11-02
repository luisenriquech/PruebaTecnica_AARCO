using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnica_AARCO.Configuration;
using PruebaTecnica_AARCO.DTO.General;
using PruebaTecnica_AARCO.DTO.JWT;
using PruebaTecnica_AARCO.Models;
using PruebaTecnica_AARCO.Negocio;
using PruebaTecnica_AARCO.Validaciones.Mensajes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnica_AARCO.UseCase.JWT
{
    public class PostTokenCommand : IInPutPort<usrJWTdto, SuccessResult>
    {
        public usrJWTdto RequestData { get; }

        public IOutPutPort<SuccessResult> OutPutPort { get; }

        public PostTokenCommand(usrJWTdto req, IOutPutPort<SuccessResult> outPutPort)
        {
            RequestData = req;
            OutPutPort = outPutPort;
        }

        public class PostTokenHandler : AsyncRequestHandler<PostTokenCommand>
        {
            public IConfiguration _configuration;
            private readonly IMapper _mapper;

            public PostTokenHandler(IMapper mapper, IConfiguration configuration)
            {
                _mapper = mapper;
                _configuration = configuration;
            }

            protected async override Task Handle(PostTokenCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    #region "Desencriptado de usuario y contraseña"
                    string userDecrypt = null;
                    string passDecrypt = null;

                    try
                    {
                        var hash = _configuration.GetSection("Hash").Get<HashDto>();
                        userDecrypt = JWTFunctions.DecryptStringAES(request.RequestData.usuario, hash);
                        passDecrypt = JWTFunctions.DecryptStringAES(request.RequestData.password, hash);
                    }
                    catch (Exception ex) { }
                    #endregion

                    #region "Validación de credenciales de usuario"
                    DataContext _context = new DataContext();
                    var usr = _context.Usuarios.FirstOrDefault(x => x.Usuario1 == userDecrypt & x.Password == JWTFunctions.EncryptToSHA256(passDecrypt));
                    #endregion
                    #region "Si el usuario sí existe y está activo"
                    if (usr != null)
                    {
                        #region "Creación de token JWT

                        var jwt = _configuration.GetSection("JWT").Get<JWTdto>();
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            //new Claim(JwtRegisteredClaimNames.Aud, jwt.Audience),
                            new Claim("usuario", usr.Usuario1),
                            new Claim("correo", usr.Email),
                            new Claim("id", usr.IdUsuario.ToString()),
                            new Claim("idRol", ""),
                            new Claim("idPuesto", "")
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                        var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                                jwt.Issuer,
                                jwt.Audience,
                                claims,
                                expires: DateTime.Now.AddMinutes(jwt.TimeSession),
                                signingCredentials: singIn
                        );

                        #endregion
                        #region "Llenado y envío de objeto respuesta"

                        ResponseTokenLoginDto responseLoginDto = new ResponseTokenLoginDto();
                        responseLoginDto.token = new JwtSecurityTokenHandler().WriteToken(token);
                        responseLoginDto.nombre = userDecrypt;
                        //responseLoginDto.email = usr.Email;
                        //responseLoginDto.idRol = usr.IdRol;
                        //responseLoginDto.idPuesto = usr.IdPuesto;
                        responseLoginDto.MinutosSesion = jwt.TimeSession;
                        responseLoginDto.Caducidad = DateTime.Now.AddMinutes(jwt.TimeSession);

                        request.OutPutPort.Handle(new SuccessResult(responseLoginDto));

                        #endregion

                    }
                    #endregion
                    #region "Si el usuario no existe o está desactivado"
                    else
                    {
                        request.OutPutPort.Handle(new SuccessResult(true, Utils.MensajesGenerales(3)));
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
