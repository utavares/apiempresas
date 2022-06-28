using ApiEmpresas.Services.Requests;
using ApiEmpresas.Services.Responses;
using ApiEmpresas.Tests.Helpers;
using Bogus;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiEmpresas.Tests
{
    public class LoginTest
    {
        //atributos
        private readonly HttpClient _httpClient;

        //construtor
        public LoginTest()
        {
            _httpClient = new HttpClient();
        }

        [Fact]
        public async Task<string> Post_Login_Returns_Ok()
        {
            #region Criando os dados da requisição

            var request = new LoginPostRequest
            {
                Login = "teste2022",
                Senha = "@Teste2022"
            };

            #endregion

            #region Executando o serviço da API e capturando a resposta

            var result = await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Login",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var response = ApiTestHelper.CreateResponse<LoginGetResponse>(result);

            response.Login.Should().Be(request.Login);
            response.AccessToken.Should().NotBeEmpty();

            #endregion

            return response.AccessToken;
        }

        [Fact]
        public async Task Post_Login_Returns_Unauthorized()
        {
            #region Criando os dados da requisição

            var request = new LoginPostRequest
            {
                Login = "abcde2022",
                Senha = "@Abcde2022"
            };

            #endregion

            #region Executando o serviço da API e capturando a resposta

            var result = await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Login",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validar o resultado do teste

            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            #endregion
        }
    }
}