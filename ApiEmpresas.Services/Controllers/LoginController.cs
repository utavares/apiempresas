using ApiEmpresas.Domain.Contracts.Services;
using ApiEmpresas.Services.Authentication;
using ApiEmpresas.Services.Requests;
using ApiEmpresas.Services.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresas.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //atributos
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public LoginController(IUsuarioDomainService usuarioDomainService, IMapper mapper, TokenService tokenService)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Post(LoginPostRequest request)
        {
            try
            {
                var usuario = _usuarioDomainService.Get(request.Login, request.Senha);

                if (usuario == null)
                    //HTTP 401 -> UNAUTHORIZED
                    return StatusCode(401, new { message = "Acesso negado, login e senha inválidos." });

                var response = _mapper.Map<LoginGetResponse>(usuario);

                response.DataHoraAcesso = DateTime.Now;
                response.AccessToken = _tokenService.CreateToken(usuario.Login);
                response.DataHoraExpiracao = _tokenService.ExpirationDate;

                //HTTP 200 -> OK
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}