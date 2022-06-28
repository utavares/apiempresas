using ApiEmpresas.Services.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Configura��es de inicializa��o do projeto

DependencyInjectionConfiguration.Configure(builder);
AutoMapperConfiguration.Configure(builder);
JwtConfiguration.Configure(builder);
SwaggerConfiguration.AddSwaggerConfiguration(builder);
CorsConfiguration.AddCorsConfiguration(builder);

#endregion

var app = builder.Build();

SwaggerConfiguration.UseSwaggerConfiguration(app);
CorsConfiguration.UseCorsConfiguration(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();