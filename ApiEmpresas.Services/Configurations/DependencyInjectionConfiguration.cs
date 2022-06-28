using ApiEmpresas.Domain.Contracts.Reports;
using ApiEmpresas.Domain.Contracts.Repositories;
using ApiEmpresas.Domain.Contracts.Services;
using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Domain.Services;
using ApiEmpresas.Infra.Data.Repositories;
using ApiEmpresas.Infra.Reports;

namespace ApiEmpresas.Services.Configurations
{
    public class DependencyInjectionConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            #region Empresas

            builder.Services.AddTransient<IEmpresaDomainService, EmpresaDomainService>();
            builder.Services.AddTransient<IEmpresaRepository, EmpresaRepository>();

            #endregion

            #region Funcionários

            builder.Services.AddTransient<IFuncionarioDomainService, FuncionarioDomainService>();
            builder.Services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();

            #endregion

            #region Usuários

            builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            #endregion

            #region Reports

            builder.Services.AddTransient<IReportService<Empresa>, EmpresaReportService>();
            builder.Services.AddTransient<IReportService<Funcionario>, FuncionarioReportService>();

            #endregion
        }
    }
}