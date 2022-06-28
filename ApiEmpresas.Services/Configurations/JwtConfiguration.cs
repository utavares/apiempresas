using ApiEmpresas.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiEmpresas.Services.Configurations
{
    public class JwtConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            #region Ler os parametros do arquivo appsettings.json

            var settings = builder.Configuration.GetSection("TokenSettings");
            builder.Services.Configure<TokenSettings>(settings);

            var tokenSettings = settings.Get<TokenSettings>();

            #endregion

            #region Definindo o tipo de autenticação e suas regras

            var secretKey = Encoding.ASCII.GetBytes(tokenSettings.SecretKey);

            builder.Services.AddAuthentication(
                auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                )
                .AddJwtBearer(
                auth =>
                {
                    auth.RequireHttpsMetadata = false;
                    auth.SaveToken = true;
                    auth.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                }
                );

            #endregion

            #region Injeção de dependência para classe que irá gerar o TOKEN

            builder.Services.AddTransient(map => new TokenService(tokenSettings));

            #endregion
        }
    }
}