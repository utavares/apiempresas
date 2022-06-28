using ApiEmpresas.Domain.Contracts.Reports;
using ApiEmpresas.Domain.Contracts.Repositories;
using ApiEmpresas.Domain.Contracts.Services;
using ApiEmpresas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Services
{
    public class EmpresaDomainService : IEmpresaDomainService
    {
        //atributos        
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IReportService<Empresa> _reportService;

        //construtor para que o AspNet inicialize os atributos (injeção de dependência)
        public EmpresaDomainService(IEmpresaRepository empresaRepository, IFuncionarioRepository funcionarioRepository, IReportService<Empresa> reportService)
        {
            _empresaRepository = empresaRepository;
            _funcionarioRepository = funcionarioRepository;
            _reportService = reportService;
        }

        public void Add(Empresa entity)
        {
            #region Regra: Não podem exitir empresas com a mesma razão social

            if (_empresaRepository.Get(e => e.RazaoSocial.Equals(entity.RazaoSocial)) != null)
                throw new ArgumentException("A Razão social informada já está cadastrado no sistema, tente outro.");

            #endregion

            #region Regra: Não podem existir empresas com o mesmo CNPJ

            if (_empresaRepository.Get(e => e.Cnpj.Equals(entity.Cnpj)) != null)
                throw new ArgumentException("O CNPJ informado já está cadastrado no sistema, tente outro.");

            #endregion

            _empresaRepository.Add(entity);
        }

        public void Update(Empresa entity)
        {
            #region Regra: A Empresa deve existir no sistema

            if (_empresaRepository.Get(entity.IdEmpresa) == null)
                throw new ArgumentException("A Empresa informada não foi encontrada, verifique o ID.");

            #endregion

            #region Regra: Não podem exitir empresas com a mesma razão social

            if (_empresaRepository.Get(e => e.RazaoSocial.Equals(entity.RazaoSocial) && e.IdEmpresa != entity.IdEmpresa) != null)
                throw new ArgumentException("A Razão social informada já está cadastrado para outra empresa no sistema.");

            #endregion

            #region Regra: Não podem existir empresas com o mesmo CNPJ

            if (_empresaRepository.Get(e => e.Cnpj.Equals(entity.Cnpj) && e.IdEmpresa != entity.IdEmpresa) != null)
                throw new ArgumentException("O CNPJ informado já está cadastrado para outra empresa no sistema.");

            #endregion

            _empresaRepository.Update(entity);
        }

        public void Delete(Empresa entity)
        {
            #region Regra: A Empresa deve existir no sistema

            if (_empresaRepository.Get(entity.IdEmpresa) == null)
                throw new ArgumentException("A Empresa informada não foi encontrada, verifique o ID.");

            #endregion

            #region Regra: A Empresa não deve conter funcionários

            if (_funcionarioRepository.Count(f => f.IdEmpresa.Equals(entity.IdEmpresa)) > 0)
                throw new ArgumentException("A Empresa não pode ser excluída pois possui funcionários relacionados.");

            #endregion

            _empresaRepository.Delete(entity);
        }

        public List<Empresa> GetAll()
        {
            return _empresaRepository.GetAll();
        }

        public Empresa Get(Guid id)
        {
            return _empresaRepository.Get(id);
        }

        public byte[] GetReport(List<Empresa> empresas, ReportType formato)
        {
            return _reportService.GenerateReport(empresas, formato);
        }

        public void Dispose()
        {
            _empresaRepository.Dispose();
            _funcionarioRepository.Dispose();
        }
    }
}