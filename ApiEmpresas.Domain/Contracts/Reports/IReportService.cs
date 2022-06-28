using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Domain.Contracts.Reports
{
    /// <summary>
    /// Interface padrão para geração de relatórios
    /// </summary>
    /// <typeparam name="TEntity">Entidade para o qual o relatório será gerado</typeparam>
    public interface IReportService<TEntity>
        where TEntity : class
    {
        byte[] GenerateReport(List<TEntity> data, ReportType reportType);
    }

    /// <summary>
    /// Tipos de relatório do sistema
    /// </summary>
    public enum ReportType
    {
        PDF = 1,
        EXCEL = 2
    }
}