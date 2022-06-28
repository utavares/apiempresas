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
    public class EmpresasTest
    {
        //atributos
        private readonly HttpClient _httpClient;
        private readonly Faker _faker;

        //construtor
        public EmpresasTest()
        {
            _httpClient = new HttpClient();
            _faker = new Faker("pt_BR");

            var loginTest = new LoginTest();
            var _accessToken = loginTest.Post_Login_Returns_Ok().Result;

            _httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        [Fact]
        public async Task<EmpresaResponse> Post_Empresas_Returns_Created()
        {
            #region Criando os dados da requisição

            var request = new EmpresasPostRequest
            {
                NomeFantasia = _faker.Company.CompanyName(),
                RazaoSocial = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
            };

            #endregion

            #region Executando o serviço da API e capturando a resposta

            var result = await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Empresas",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            #endregion

            return ApiTestHelper.CreateResponse<EmpresaResponse>(result);
        }

        [Fact]
        public async Task Put_Empresas_Returns_Ok()
        {
            #region Consultar as empresas e capturar a primeira empresa obtida

            var empresa = await Post_Empresas_Returns_Created();

            #endregion

            #region Criando os dados da requisição

            var request = new EmpresasPutRequest
            {
                IdEmpresa = empresa.response.IdEmpresa,
                NomeFantasia = _faker.Company.CompanyName(),
                RazaoSocial = _faker.Company.CompanyName(),
                Cnpj = _faker.Company.Cnpj(),
            };

            #endregion

            #region Executando o serviço da API e capturando a resposta

            var result = await _httpClient.PutAsync($"{ApiTestHelper.Endpoint}/Empresas",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async Task Delete_Empresas_Returns_Ok()
        {
            #region Cadastrando uma empresa

            var empresa = await Post_Empresas_Returns_Created();

            #endregion

            #region Executando o serviço da API e capturando a resposta

            var result = await _httpClient.DeleteAsync($"{ApiTestHelper.Endpoint}/Empresas/{empresa.response.IdEmpresa}");

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            #endregion
        }

        [Fact]
        public async Task<List<EmpresasGetResponse>> GetAll_Empresas_Returns_Ok()
        {
            #region Executando o serviço de consulta e ler o resultado

            var result = await _httpClient.GetAsync($"{ApiTestHelper.Endpoint}/Empresas");

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = ApiTestHelper.CreateResponse<List<EmpresasGetResponse>>(result);

            response.Should().NotBeNullOrEmpty();

            #endregion

            return response;
        }

        [Fact]
        public async Task GetById_Empresas_Returns_Ok()
        {
            #region Cadastrando uma empresa

            var empresa = await Post_Empresas_Returns_Created();

            #endregion

            #region Executando o serviço de consulta e ler o resultado

            var result = await _httpClient.GetAsync($"{ApiTestHelper.Endpoint}/Empresas/{empresa.response.IdEmpresa}");

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = ApiTestHelper.CreateResponse<EmpresasGetResponse>(result);

            response.Should().NotBeNull();

            #endregion
        }
    }

    public class EmpresaResponse
    {
        public string message { get; set; }
        public EmpresasGetResponse response { get; set; }
    }
}