namespace ApiEmpresas.Services.Configurations
{
    public class CorsConfiguration
    {
        public static void AddCorsConfiguration(WebApplicationBuilder builder)
        {
            builder.Services
                .AddCors(s => s.AddPolicy("CORS_POLICY",
                    builder =>
                    {
                        builder.AllowAnyOrigin()  //qualquer servidor
                               .AllowAnyMethod()  //qualquer método (POST, PUT, DELETE, GET etc)
                               .AllowAnyHeader(); //qualquer cabeçalho
                    }));
        }

        public static void UseCorsConfiguration(WebApplication app)
        {
            app.UseCors("CORS_POLICY");
        }
    }
}