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
    public class EmpresaReportService : IReportService<Empresa>
    {
        public byte[] GenerateReport(List<Empresa> data, ReportType reportType)
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

        private byte[] GenerateExcel(List<Empresa> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                var planilha = excelPackage.Workbook.Worksheets.Add("empresas");

                planilha.Cells["A1"].Value = "Relatório de Empresas";
                planilha.Cells["A2"].Value = DateTime.Now.ToString("dddd, dd/MM/yyyy");

                planilha.Cells["A4"].Value = "ID";
                planilha.Cells["B4"].Value = "NOME FANTASIA";
                planilha.Cells["C4"].Value = "RAZÃO SOCIAL";
                planilha.Cells["D4"].Value = "CNPJ";

                var linha = 5;

                foreach (var item in data)
                {
                    planilha.Cells[$"A{linha}"].Value = item.IdEmpresa;
                    planilha.Cells[$"B{linha}"].Value = item.NomeFantasia;
                    planilha.Cells[$"C{linha}"].Value = item.RazaoSocial;
                    planilha.Cells[$"D{linha}"].Value = item.Cnpj;

                    linha++;
                }

                planilha.Cells[$"A1:D{linha - 1}"].AutoFitColumns();

                return excelPackage.GetAsByteArray();
            }
        }

        private byte[] GeneratePdf(List<Empresa> data)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            using (var document = new Document(pdf))
            {
                document.Add(new Paragraph("Relatório de Empresas"));
                document.Add(new Paragraph(DateTime.Now.ToString("dddd, dd/MM/yyyy")));

                var table = new Table(4);

                table.AddHeaderCell("ID");
                table.AddHeaderCell("NOME FANTASIA");
                table.AddHeaderCell("RAZÃO SOCIAL");
                table.AddHeaderCell("CNPJ");

                foreach (var item in data)
                {
                    table.AddCell(item.IdEmpresa.ToString());
                    table.AddCell(item.NomeFantasia);
                    table.AddCell(item.RazaoSocial);
                    table.AddCell(item.Cnpj);
                }

                document.Add(table);
            }

            return memoryStream.ToArray();
        }
    }
}