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
    public class EmpresasController : ControllerBase
    {
        //atributos
        private readonly IEmpresaDomainService _empresaDomainService;
        private readonly IMapper _mapper;

        //construtor para inicialização do atributo (injeção de dependência)
        public EmpresasController(IEmpresaDomainService empresaDomainService, IMapper mapper)
        {
            _empresaDomainService = empresaDomainService;
            _mapper = mapper;
        }

        /// <summary>
        /// Serviço para cadastro de empresa
        /// </summary>
        [HttpPost]
        public IActionResult Post(EmpresasPostRequest request)
        {
            try
            {
                //cadastrando a empresa
                var empresa = _mapper.Map<Empresa>(request);
                _empresaDomainService.Add(empresa);

                var response = _mapper.Map<EmpresasGetResponse>(empresa);

                //HTTP 201 -> CREATED
                return StatusCode(201, new { message = "Empresa cadastrada com sucesso.", response });
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

        /// <summary>
        /// Serviço para atualizar empresa
        /// </summary>
        [HttpPut]
        public IActionResult Put(EmpresasPutRequest request)
        {
            try
            {
                //atualizando dados da empresa
                var empresa = _mapper.Map<Empresa>(request);
                _empresaDomainService.Update(empresa);

                var response = _mapper.Map<EmpresasGetResponse>(empresa);

                //HTTP 200 -> OK
                return StatusCode(200, new { message = "Empresa atualizada com sucesso.", response });
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

        /// <summary>
        /// Serviço para excluir empresa 
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var empresa = _empresaDomainService.Get(id);
                _empresaDomainService.Delete(empresa);

                var response = _mapper.Map<EmpresasGetResponse>(empresa);

                //HTTP 200 -> OK
                return StatusCode(200, new { message = "Empresa excluída com sucesso.", response });
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

        /// <summary>
        /// Serviço para consultar todas as empresas 
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var empresas = _mapper.Map<List<EmpresasGetResponse>>(_empresaDomainService.GetAll());

                if (empresas.Count > 0)
                    //HTTP 200 -> OK
                    return StatusCode(200, empresas);
                else
                    //HTTP 200 -> NO CONTENT
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }

        /// <summary>
        /// Serviço para consultar 1 empresa baseado no ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                //consultando 1 empresa baseado no ID
                var empresa = _mapper.Map<EmpresasGetResponse>(_empresaDomainService.Get(id));

                //verificando se a empresa foi encontrada
                if (empresa != null)
                    //HTTP 200 -> OK
                    return StatusCode(200, empresa);
                else
                    //HTTP 200 -> NO CONTENT
                    return StatusCode(204);
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { message = e.Message });
            }
        }

        /// <summary>
        /// Serviço para obter um relatório de empresas em formato excel ou pdf
        /// </summary>
        [HttpGet]
        [Route("Reports/{formato}")]
        public IActionResult GetReport(string formato)
        {
            try
            {
                var lista = new List<Empresa>();

                switch (formato.ToLower())
                {
                    case "excel":
                        lista = _empresaDomainService.GetAll();
                        var excel = _empresaDomainService.GetReport(lista, ReportType.EXCEL);

                        return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                    case "pdf":
                        lista = _empresaDomainService.GetAll();
                        var pdf = _empresaDomainService.GetReport(lista, ReportType.PDF);

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