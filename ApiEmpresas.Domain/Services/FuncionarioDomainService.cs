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
    public class FuncionarioDomainService : IFuncionarioDomainService
    {
        //atributos
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IReportService<Funcionario> _reportService;

        //construtor para injeção de dependência (inicialização dos atributos)
        public FuncionarioDomainService(IFuncionarioRepository funcionarioRepository, IEmpresaRepository empresaRepository, IReportService<Funcionario> reportService)
        {
            _funcionarioRepository = funcionarioRepository;
            _empresaRepository = empresaRepository;
            _reportService = reportService;
        }

        public void Add(Funcionario entity)
        {
            #region Regra: Não podem exitir funcionários com o mesmo CPF

            if (_funcionarioRepository.Get(f => f.Cpf.Equals(entity.Cpf)) != null)
                throw new ArgumentException("O CPF informado já está cadastrado no sistema, tente outro.");

            #endregion

            #region Regra: Não podem exitir funcionários com a mesma Matrícula

            if (_funcionarioRepository.Get(f => f.Matricula.Equals(entity.Matricula)) != null)
                throw new ArgumentException("A Matrícula informada já está cadastrada no sistema, tente outra.");

            #endregion

            #region Regra: A Empresa deve existir no sistema

            if (_empresaRepository.Get(entity.IdEmpresa) == null)
                throw new ArgumentException("A Empresa informada não foi encontrada, verifique o ID.");

            #endregion

            _funcionarioRepository.Add(entity);
        }

        public void Update(Funcionario entity)
        {
            #region Regra: O Funcionário deve existir no sistema

            if (_funcionarioRepository.Get(entity.IdFuncionario) == null)
                throw new ArgumentException("O Funcionário informado não foi encontrado, verifique o ID.");

            #endregion

            #region Regra: Não podem exitir funcionários com o mesmo CPF

            if (_funcionarioRepository.Get(f => f.Cpf.Equals(entity.Cpf) && f.IdFuncionario != entity.IdFuncionario) != null)
                throw new ArgumentException("O CPF informado já está cadastrado para outro funcionário no sistema.");

            #endregion

            #region Regra: Não podem exitir funcionários com a mesma Matrícula

            if (_funcionarioRepository.Get(f => f.Matricula.Equals(entity.Matricula) && f.IdFuncionario != entity.IdFuncionario) != null)
                throw new ArgumentException("A Matrícula informada já está cadastrada para outro funcionário no sistema.");

            #endregion

            #region Regra: A Empresa deve existir no sistema

            if (_empresaRepository.Get(entity.IdEmpresa) == null)
                throw new ArgumentException("A Empresa informada não foi encontrada, verifique o ID.");

            #endregion

            _funcionarioRepository.Update(entity);
        }

        public void Delete(Funcionario entity)
        {
            #region Regra: O Funcionário deve existir no sistema

            if (_funcionarioRepository.Get(entity.IdFuncionario) == null)
                throw new ArgumentException("O Funcionário informado não foi encontrado, verifique o ID.");

            #endregion

            _funcionarioRepository.Delete(entity);
        }

        public List<Funcionario> GetAll()
        {
            return _funcionarioRepository.GetAll();
        }

        public Funcionario Get(Guid id)
        {
            return _funcionarioRepository.Get(id);
        }

        public byte[] GetReport(List<Funcionario> funcionarios, ReportType formato)
        {
            return _reportService.GenerateReport(funcionarios, formato);
        }

        public void Dispose()
        {
            _funcionarioRepository.Dispose();
            _empresaRepository.Dispose();
        }
    }
}