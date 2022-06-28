using ApiEmpresas.Domain.Contracts.Services;
using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Services.Requests;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresas.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //atributos
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IMapper _mapper;

        //construtor com entrada de argumentos (injeção de dependência)
        public AccountController(IUsuarioDomainService usuarioDomainService, IMapper mapper)
        {
            _usuarioDomainService = usuarioDomainService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(AccountPostRequest request)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(request);
                _usuarioDomainService.Add(usuario);

                //HTTP 201 -> CREATED
                return StatusCode(201, new { message = "Usuário cadastrado com sucesso." });
            }
            catch (ArgumentException e)
            {
                //HTTP 400 -> BAD REQUEST
                return StatusCode(400, new { message = e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}