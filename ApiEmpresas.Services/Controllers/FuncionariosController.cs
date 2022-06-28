using ApiEmpresas.Domain.Contracts.Reports;
using ApiEmpresas.Domain.Contracts.Services;
using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Services.Requests;
using ApiEmpresas.Services.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEmpresas.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        //atributos
        private readonly IFuncionarioDomainService _funcionarioDomainService;
        private readonly IEmpresaDomainService _empresaDomainService;
        private readonly IMapper _mapper;

        //construtor com entrada de argumentos
        public FuncionariosController(IFuncionarioDomainService funcionarioDomainService, IEmpresaDomainService empresaDomainService, IMapper mapper)
        {
            _funcionarioDomainService = funcionarioDomainService;
            _empresaDomainService = empresaDomainService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(FuncionariosPostRequest request)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(request);
                _funcionarioDomainService.Add(funcionario);

                var response = _mapper.Map<FuncionariosGetResponse>(funcionario);
                response.Empresa = _mapper.Map<EmpresasGetResponse>(_empresaDomainService.Get(request.IdEmpresa));

                //HTTP 201 -> CREATED
                return StatusCode(201, new { message = "Funcionário cadastrado com sucesso.", response });
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

        [HttpPut]
        public IActionResult Put(FuncionariosPutRequest request)
        {
            try
            {
                var funcionario = _mapper.Map<Funcionario>(request);
                _funcionarioDomainService.Update(funcionario);

                var response = _mapper.Map<FuncionariosGetResponse>(funcionario);
                response.Empresa = _mapper.Map<EmpresasGetResponse>(_empresaDomainService.Get(request.IdEmpresa));

                //HTTP 200 -> OK
                return StatusCode(200, new { message = "Funcionário atualizado com sucesso.", response });
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

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var funcionario = _funcionarioDomainService.Get(id);
                _funcionarioDomainService.Delete(funcionario);

                var response = _mapper.Map<FuncionariosGetResponse>(funcionario);
                response.Empresa = _mapper.Map<EmpresasGetResponse>(_empresaDomainService.Get(funcionario.IdEmpresa));

                //HTTP 200 -> OK
                return StatusCode(200, new { message = "Funcionário excluído com sucesso.", response });
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

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var funcionarios = _mapper.Map<List<FuncionariosGetResponse>>(_funcionarioDomainService.GetAll());

                if (funcionarios.Count > 0)
                    //HTTP 200 -> OK
                    return StatusCode(200, funcionarios);
                else
                    //HTTP 204 -> NO CONTENT
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var funcionario = _mapper.Map<FuncionariosGetResponse>(_funcionarioDomainService.Get(id));

                if (funcionario != null)
                    //HTTP 200 -> OK
                    return StatusCode(200, funcionario);
                else
                    //HTTP 204 -> NO CONTENT
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }

        /// <summary>
        /// Serviço para obter um relatório de funcionários em formato excel ou pdf
        /// </summary>
        [HttpGet]
        [Route("Reports/{formato}")]
        public IActionResult GetReport(string formato)
        {
            try
            {
                var lista = new List<Funcionario>();

                switch (formato.ToLower())
                {
                    case "excel":
                        lista = _funcionarioDomainService.GetAll();
                        var excel = _funcionarioDomainService.GetReport(lista, ReportType.EXCEL);

                        return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    case "pdf":
                        lista = _funcionarioDomainService.GetAll();
                        var pdf = _funcionarioDomainService.GetReport(lista, ReportType.PDF);

                        return File(pdf, "application/pdf");

                    default:
                        return StatusCode(400, new { message = "formato inválido, informe apenas 'excel' ou 'pdf'." });
                }
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}