using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelEnum;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class MachineryExcels
{
    public static byte[] MachineryToExcel(
        ICollection<GetsMachineryExcelExporterModel> result,
        List<MachineryExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<MachineryExcelEnum, int>();
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
                if (columns.ContainsKey(MachineryExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(MachineryExcelEnum.MachineryName))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(MachineryExcelEnum.MachineryCode))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.MachineryCode]].Value = item.MachineryCode;
                if (columns.ContainsKey(MachineryExcelEnum.GroupName))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.GroupName]].Value = item.GroupName;
                if (columns.ContainsKey(MachineryExcelEnum.GroupCode))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.GroupCode]].Value = item.GroupCode;
                if (columns.ContainsKey(MachineryExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(MachineryExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[MachineryExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 7],
                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = MachineryExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = MachineryExcelEnum.MachineryName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = MachineryExcelEnum.MachineryCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = MachineryExcelEnum.GroupName.GetEnumDescription();
            worksheet.Cells[1, 5].Value = MachineryExcelEnum.GroupCode.GetEnumDescription();
            worksheet.Cells[1, 6].Value = MachineryExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 7].Value = MachineryExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.MachineryName;
                worksheet.Cells[currentRow, 3].Value = item.MachineryCode;
                worksheet.Cells[currentRow, 4].Value = item.GroupName;
                worksheet.Cells[currentRow, 5].Value = item.GroupCode;
                worksheet.Cells[currentRow, 6].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                worksheet.Cells[currentRow, 7].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 9, 4, 10],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            8);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
