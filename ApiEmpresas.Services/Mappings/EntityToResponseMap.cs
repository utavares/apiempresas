using ApiEmpresas.Domain.Entities;
using ApiEmpresas.Services.Responses;
using AutoMapper;

namespace ApiEmpresas.Services.Mappings
{
    /// <summary>
    /// Mapeamento DE/PARA -> ENTITY/RESPONSE
    /// </summary>
    public class EntityToResponseMap : Profile
    {
        public EntityToResponseMap()
        {
            CreateMap<Empresa, EmpresasGetResponse>();
            CreateMap<Funcionario, FuncionariosGetResponse>();
            CreateMap<Usuario, LoginGetResponse>();
        }
    }
}