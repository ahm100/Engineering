using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelEnum;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class OperationInfoGroupExcels
{
    public static byte[] OperationInfoGroupToExcel(
        ICollection<GetsOperationInfoGroupExcelExporterModel> result,
        List<OperationInfoGroupExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گروه شرح عملیات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<OperationInfoGroupExcelEnum, int>();
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
                if (columns.ContainsKey(OperationInfoGroupExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[OperationInfoGroupExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(OperationInfoGroupExcelEnum.OperationInfoGroupName))
                    worksheet.Cells[currentRow, columns[OperationInfoGroupExcelEnum.OperationInfoGroupName]].Value = item.OperationInfoGroupName;
                if (columns.ContainsKey(OperationInfoGroupExcelEnum.OperationInfoGroupCode))
                    worksheet.Cells[currentRow, columns[OperationInfoGroupExcelEnum.OperationInfoGroupCode]].Value = item.OperationInfoGroupCode;
                if (columns.ContainsKey(OperationInfoGroupExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[OperationInfoGroupExcelEnum.IsActive]].Value = item.IsActive;
                if (columns.ContainsKey(OperationInfoGroupExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[OperationInfoGroupExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 5],

                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = OperationInfoGroupExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = OperationInfoGroupExcelEnum.OperationInfoGroupName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = OperationInfoGroupExcelEnum.OperationInfoGroupCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = OperationInfoGroupExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = OperationInfoGroupExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.OperationInfoGroupName;
                worksheet.Cells[currentRow, 3].Value = item.OperationInfoGroupCode;
                worksheet.Cells[currentRow, 4].Value = item.IsActive;
                worksheet.Cells[currentRow, 5].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 7, 4, 8],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            7);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
