using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEmpresas.Services.Authentication
{
    /// <summary>
    /// Classe para geração do TOKEN
    /// </summary>
    public class TokenService
    {
        //atributo
        private readonly TokenSettings _tokenSettings;

        //construtor para injeção de dependência
        public TokenService(TokenSettings tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public string CreateToken(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //nome do usuário para o qual o token está sendo gerado
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, login) }),

                //data de expiração (validade) do token
                Expires = ExpirationDate,

                //chave antifalsificação do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public DateTime ExpirationDate
        {
            get => DateTime.UtcNow.AddHours(_tokenSettings.ExpirationInHours);
        }
    }
}