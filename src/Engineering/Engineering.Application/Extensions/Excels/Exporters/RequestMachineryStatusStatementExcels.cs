using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelEnum;
using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class RequestMachineryStatusStatementExcels
{
    public static byte[] RequestMachineryStatusStatementToExcel(
        ICollection<GetsRequestMachineryStatusStatementExcelExporterModel> result,
        List<RequestMachineryStatusStatementExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("صورت وضعیت درخواست ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<RequestMachineryStatusStatementExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;

                ExcelStyles.SetHeaderStyle(
                    worksheet.Cells[1, 1, 1, counter + 1],

                    ExcelBorderStyle.Medium);
            }

            foreach (var item in result)
            {
                currentRow++;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.CostCenter))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.CostCenter]].Value = item.CostCenter;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.Project))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.Project]].Value = item.Project;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.ContractorNickname))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.ContractorNickname]].Value = item.ContractorNickname;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.ContractorIBAN))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.ContractorIBAN]].Value = item.ContractorIBAN;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.FromDateShamsi))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.FromDateShamsi]].Value = item.FromDateShamsi;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.ToDateShamsi))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.ToDateShamsi]].Value = item.ToDateShamsi;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.TotalRequestedCount))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.TotalRequestedCount]].Value = item.TotalRequestedCount;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.TotalFinalPrice))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.TotalFinalPrice]].Value = item.TotalFinalPrice;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.ContractorFinalPrice))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.ContractorFinalPrice]].Value = item.ContractorFinalPrice;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.Created]].Value = item.CreatedShamsi;
                if (columns.ContainsKey(RequestMachineryStatusStatementExcelEnum.PaymentDate))
                    worksheet.Cells[currentRow, columns[RequestMachineryStatusStatementExcelEnum.PaymentDate]].Value = item.PaymentDateShamsi;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }
        else
        {
            ExcelStyles.SetHeaderStyle(
                worksheet.Cells[1, 1, 1, 16],

                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = RequestMachineryStatusStatementExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = RequestMachineryStatusStatementExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = RequestMachineryStatusStatementExcelEnum.CostCenter.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = RequestMachineryStatusStatementExcelEnum.Project.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = RequestMachineryStatusStatementExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = RequestMachineryStatusStatementExcelEnum.ContractorNickname.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = RequestMachineryStatusStatementExcelEnum.ContractorIBAN.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = RequestMachineryStatusStatementExcelEnum.FromDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = RequestMachineryStatusStatementExcelEnum.ToDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = RequestMachineryStatusStatementExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = RequestMachineryStatusStatementExcelEnum.TotalRequestedCount.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = RequestMachineryStatusStatementExcelEnum.TotalFinalPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = RequestMachineryStatusStatementExcelEnum.ContractorFinalPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = RequestMachineryStatusStatementExcelEnum.Description.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = RequestMachineryStatusStatementExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = RequestMachineryStatusStatementExcelEnum.PaymentDate.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.CostCenter;
                worksheet.Cells[currentRow, 4].Value = item.Project;
                worksheet.Cells[currentRow, 5].Value = item.Contractor;
                worksheet.Cells[currentRow, 6].Value = item.ContractorNickname;
                worksheet.Cells[currentRow, 7].Value = item.ContractorIBAN;
                worksheet.Cells[currentRow, 8].Value = item.FromDateShamsi;
                worksheet.Cells[currentRow, 9].Value = item.ToDateShamsi;
                worksheet.Cells[currentRow, 10].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 11].Value = item.TotalRequestedCount;
                worksheet.Cells[currentRow, 12].Value = item.TotalFinalPrice;
                worksheet.Cells[currentRow, 13].Value = item.ContractorFinalPrice;
                worksheet.Cells[currentRow, 14].Value = item.Description;
                worksheet.Cells[currentRow, 15].Value = item.CreatedShamsi;
                worksheet.Cells[currentRow, 16].Value = item.PaymentDateShamsi;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}