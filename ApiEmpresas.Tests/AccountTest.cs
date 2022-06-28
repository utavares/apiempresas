using ApiEmpresas.Services.Requests;
using ApiEmpresas.Tests.Helpers;
using Bogus;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ApiEmpresas.Tests
{
    public class AccountTest
    {
        //atributos
        private readonly HttpClient _httpClient;
        private readonly Faker _faker;

        //m�todo construtor
        public AccountTest()
        {
            _httpClient = new HttpClient();
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Post_Account_Returns_Created()
        {
            #region Criando os dados da requisi��o

            var login = _faker.Internet.UserName();

            var request = new AccountPostRequest
            {
                Nome = _faker.Person.FullName,
                Login = login.Length <= 20 ? login : login.Substring(0, 20),
                Senha = "@Teste123"
            };

            #endregion

            #region Executando o servi�o da API e capturando a resposta

            var result = await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Account",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validando o resultado obtido da API

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            #endregion
        }

        [Fact]
        public async Task Post_Account_Returns_BadRequest()
        {
            #region Criando os dados da requisi��o

            var login = _faker.Internet.UserName();

            var request = new AccountPostRequest
            {
                Nome = _faker.Person.FullName,
                Login = login.Length <= 20 ? login : login.Substring(0, 20),
                Senha = "@Teste123"
            };

            #endregion

            #region Executando o servi�o da API e capturando a resposta

            //realizando o cadastro do usu�rio na API
            await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Account",
                ApiTestHelper.CreateContent(request));

            //tentando cadastrar o mesmo usu�rio
            var result = await _httpClient.PostAsync($"{ApiTestHelper.Endpoint}/Account",
                ApiTestHelper.CreateContent(request));

            #endregion

            #region Validando o resultado obtido da API

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            #endregion
        }
    }
}