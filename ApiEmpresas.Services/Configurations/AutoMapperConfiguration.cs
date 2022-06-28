namespace ApiEmpresas.Services.Configurations
{
    public class AutoMapperConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}