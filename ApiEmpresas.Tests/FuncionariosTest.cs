using ApiEmpresas.Services.Requests;
using ApiEmpresas.Services.Responses;
using ApiEmpresas.Tests.Helpers;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiEmpresas.Tests
{
    public class FuncionariosTest
    {
        //atributos
        private readonly HttpClient _httpClient;
        private readonly Faker _faker;

        //construtor
        public FuncionariosTest()
        {
            _httpClient = new HttpClient();
            _faker = new Faker("pt_BR");

            var loginTest = new LoginTest();
            var _accessToken = loginTest.Post_Login_Returns_Ok().Result;

            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        [Fact]
        public async Task<FuncionarioResponse> Post_Funcionarios_Returns_Created()
        {
            #region Criando os dados da requisição

            var empresasTest = new EmpresasTest();
            var empresa = await empresasTest.Post_Empresas_Returns_Created();

            var request = new FuncionariosPostRequest
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                DataAdmissao = DateTime.Now,
                Matricula = _faker.Random.Number(999999).ToString(),
                IdEmpresa = empresa.response.IdEmpresa,
            };

            #endregion

            #region Executando o serviço da API e capturando o resultado

            var result = await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Funcionarios",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            #endregion

            return ApiTestHelper.CreateResponse<FuncionarioResponse>(result);
        }

        [Fact]
        public async Task Put_Funcionarios_Returns_Ok()
        {
            #region Cadastrando um funcionario

            var funcionario = await Post_Funcionarios_Returns_Created();

            #endregion

            #region Criando os dados da requisição

            var request = new FuncionariosPutRequest
            {
                IdFuncionario = funcionario.response.IdFuncionario,
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                DataAdmissao = DateTime.Now,
                Matricula = _faker.Random.Number(999999).ToString(),
                IdEmpresa = funcionario.response.Empresa.IdEmpresa
            };

            #endregion

            #region Executando o serviço da API e capturando o resultado

            var result = await _httpClient.PutAsync($"{ApiTestHelper.Endpoint}/Funcionarios",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async Task Delete_Funcionarios_Returns_Ok()
        {
            #region Cadastrando um funcionario

            var funcionario = await Post_Funcionarios_Returns_Created();

            #endregion

            #region Executando o serviço da API para exclusão do funcionário

            var result = await _httpClient.DeleteAsync($"{ApiTestHelper.Endpoint}/Funcionarios/{funcionario.response.IdFuncionario}");

            #endregion

            #region Validar o resultado do teste

            var content = result.Content.ReadAsStringAsync().Result;
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async Task GetAll_Funcionarios_Returns_Ok()
        {
            #region Executando o serviço de consulta e ler o resultado

            var result = await _httpClient.GetAsync($"{ApiTestHelper.Endpoint}/Funcionarios");

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = ApiTestHelper.CreateResponse<List<FuncionariosGetResponse>>(result);

            response.Should().NotBeNullOrEmpty();

            #endregion
        }

        [Fact]
        public async Task GetById_Funcionarios_Returns_Ok()
        {
            #region Cadastrando uma funcionário

            var funcionario = await Post_Funcionarios_Returns_Created();

            #endregion

            #region Executando o serviço de consulta e ler o resultado

            var result = await _httpClient.GetAsync($"{ApiTestHelper.Endpoint}/Funcionarios/{funcionario.response.IdFuncionario}");

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = ApiTestHelper.CreateResponse<FuncionariosGetResponse>(result);

            response.Should().NotBeNull();

            #endregion
        }
    }

    public class FuncionarioResponse
    {
        public string message { get; set; }
        public FuncionariosGetResponse response { get; set; }
    }
}