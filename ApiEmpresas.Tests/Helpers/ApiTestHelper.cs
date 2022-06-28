using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Tests.Helpers
{
    public class ApiTestHelper
    {
        /// <summary>
        /// Método para retornar o endereço da API de testes
        /// </summary>
        public static string Endpoint
        {
            get => "http://utavares-001-site1.itempurl.com/api";
        }

        /// <summary>
        /// Método para converter um objeto C# para formato JSON (Serialização), de maneira a montar o REQUEST BODY das requisições
        /// </summary>
        /// <typeparam name="TRequest">Tipo de dados da requisição</typeparam>
        /// <param name="request">Modelo de dados da requisição</param>
        /// <returns>Conteúdo JSON pronto para envio na API</returns>
        public static StringContent CreateContent<TRequest>(TRequest request)
            where TRequest : class
        {
            return new StringContent(JsonConvert.SerializeObject(request),
                Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Método para converter um conteudo JSON obtido da API para objeto (Deserialização)
        /// </summary>
        /// <typeparam name="TResponse">Tipo do objeto obtido da API</typeparam>
        /// <param name="message">Conteúdo JSON que será deserializado</param>
        /// <returns>Conteúdo do objeto obtido da API</returns>
        public static TResponse CreateResponse<TResponse>(HttpResponseMessage message)
            where TResponse : class
        {
            return JsonConvert.DeserializeObject<TResponse>
                (message.Content.ReadAsStringAsync().Result);
        }
    }
}



