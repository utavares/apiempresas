using ApiEmpresas.Domain.Contracts.Reports;
using ApiEmpresas.Domain.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEmpresas.Infra.Reports
{
    public class FuncionarioReportService : IReportService<Funcionario>
    {
        public byte[] GenerateReport(List<Funcionario> data, ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.EXCEL:
                    return GenerateExcel(data);
                case ReportType.PDF:
                    return GeneratePdf(data);
                default:
                    return null;
            }
        }

        private byte[] GenerateExcel(List<Funcionario> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                var planilha = excelPackage.Workbook.Worksheets.Add("funcionários");

                planilha.Cells["A1"].Value = "Relatório de Funcionários";
                planilha.Cells["A2"].Value = DateTime.Now.ToString("dddd, dd/MM/yyyy");

                planilha.Cells["A4"].Value = "ID";
                planilha.Cells["B4"].Value = "NOME";
                planilha.Cells["C4"].Value = "CPF";
                planilha.Cells["D4"].Value = "MATRÍCULA";
                planilha.Cells["E4"].Value = "DATA DE ADMISSÃO";
                planilha.Cells["F4"].Value = "EMPRESA";
                planilha.Cells["G4"].Value = "RAZÃO SOCIAL";
                planilha.Cells["H4"].Value = "CNPJ";

                var linha = 5;

                foreach (var item in data)
                {
                    planilha.Cells[$"A{linha}"].Value = item.IdFuncionario;
                    planilha.Cells[$"B{linha}"].Value = item.Nome;
                    planilha.Cells[$"C{linha}"].Value = item.Cpf;
                    planilha.Cells[$"D{linha}"].Value = item.Matricula;
                    planilha.Cells[$"E{linha}"].Value = item.DataAdmissao.ToString("dd/MM/yyyy");
                    planilha.Cells[$"F{linha}"].Value = item.Empresa.NomeFantasia;
                    planilha.Cells[$"G{linha}"].Value = item.Empresa.RazaoSocial;
                    planilha.Cells[$"H{linha}"].Value = item.Empresa.Cnpj;

                    linha++;
                }

                planilha.Cells[$"A1:H{linha - 1}"].AutoFitColumns();

                return excelPackage.GetAsByteArray();
            }
        }

        private byte[] GeneratePdf(List<Funcionario> data)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            using (var document = new Document(pdf))
            {
                document.Add(new Paragraph("Relatório de Funcionários"));
                document.Add(new Paragraph(DateTime.Now.ToString("dddd, dd/MM/yyyy")));

                var table = new Table(8);

                table.AddHeaderCell("ID");
                table.AddHeaderCell("NOME");
                table.AddHeaderCell("CPF");
                table.AddHeaderCell("MATRÍCULA");
                table.AddHeaderCell("DATA DE ADMISSÃO");
                table.AddHeaderCell("EMPRESA");
                table.AddHeaderCell("RAZÃO SOCIAL");
                table.AddHeaderCell("CNPJ");

                foreach (var item in data)
                {
                    table.AddCell(item.IdFuncionario.ToString());
                    table.AddCell(item.Nome);
                    table.AddCell(item.Cpf);
                    table.AddCell(item.Matricula);
                    table.AddCell(item.DataAdmissao.ToString("dd/MM/yyyy"));
                    table.AddCell(item.Empresa.NomeFantasia);
                    table.AddCell(item.Empresa.RazaoSocial);
                    table.AddCell(item.Empresa.Cnpj);
                }

                document.Add(table);
            }

            return memoryStream.ToArray();
        }
    }
}