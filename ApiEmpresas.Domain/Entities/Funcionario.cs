using ApiEmpresas.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Entities
{
    /// <summary>
    /// Entidade de domínio
    /// </summary>
    public class Funcionario
    {
        #region Atributos

        private Guid _idFuncionario;
        private string _nome;
        private string _cpf;
        private string _matricula;
        private DateTime _dataAdmissao;
        private Guid _idEmpresa;

        #endregion

        #region Propriedades

        public Guid IdFuncionario
        {
            get => _idFuncionario;
            set
            {
                if (value == null || value == Guid.Empty)
                    throw new ArgumentException("Id do Funcionário é obrigatório.");

                _idFuncionario = value;
            }
        }

        public string Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Nome do funcionário é obrigatório.");

                else if (value.Trim().Length < 6 || value.Trim().Length > 150)
                    throw new ArgumentException("Nome do funcionário deve ter de 6 a 150 caracteres.");

                _nome = value;
            }
        }

        public string Cpf
        {
            get => _cpf;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("CPF é obrigatório.");

                else if (!CpfValidation.IsValid(value))
                    throw new ArgumentException("CPF é inválido.");

                _cpf = value;
            }
        }

        public string Matricula
        {
            get => _matricula;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Matrícula é obrigatório.");

                else if (value.Trim().Length < 4 || value.Trim().Length > 10)
                    throw new ArgumentException("Matrícula deve ter de 4 a 10 caracteres.");

                _matricula = value;
            }
        }

        public DateTime DataAdmissao
        {
            get => _dataAdmissao;
            set
            {
                if (value == null || value == DateTime.MinValue)
                    throw new ArgumentException("Data de admissão é obrigatório.");

                _dataAdmissao = value;
            }
        }

        public Guid IdEmpresa
        {
            get => _idEmpresa;
            set
            {
                if (value == null || value == Guid.Empty)
                    throw new ArgumentException("Id da Empresa é obrigatório.");

                _idEmpresa = value;
            }
        }

        #endregion

        #region Associações

        public Empresa Empresa { get; set; }

        #endregion

    }
}