using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Services.Requests;
using AutoMapper;

namespace ApiEmpresas.Services.Mappings
{
    /// <summary>
    /// Mapeamento DE/PARA -> REQUEST/ENTITY
    /// </summary>
    public class RequestToEntityMap : Profile
    {
        public RequestToEntityMap()
        {
            #region Empresas

            CreateMap<EmpresasPostRequest, Empresa>()
                .AfterMap((src, dest) =>
                {
                    dest.IdEmpresa = Guid.NewGuid();
                });

            CreateMap<EmpresasPutRequest, Empresa>();

            #endregion

            #region Funcionários

            CreateMap<FuncionariosPostRequest, Funcionario>()
                .AfterMap((src, dest) =>
                {
                    dest.IdFuncionario = Guid.NewGuid();
                });

            CreateMap<FuncionariosPutRequest, Funcionario>();

            #endregion

            #region Usuários

            CreateMap<AccountPostRequest, Usuario>()
                .AfterMap((src, dest) =>
                {
                    dest.IdUsuario = Guid.NewGuid();
                });

            #endregion
        }
    }
}