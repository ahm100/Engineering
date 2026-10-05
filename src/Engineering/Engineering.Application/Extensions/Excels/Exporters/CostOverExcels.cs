using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelEnum;
using Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;
public class CostOverExcels
{
    public static byte[] CostOverToExcel(
        ICollection<GetsCostOverExcelExporterModel> result,
        List<CostOverExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("هزینه های سربار");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<CostOverExcelEnum, int>();
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
                if (columns.ContainsKey(CostOverExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[CostOverExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(CostOverExcelEnum.CostOverName))
                    worksheet.Cells[currentRow, columns[CostOverExcelEnum.CostOverName]].Value = item.CostOverName;
                if (columns.ContainsKey(CostOverExcelEnum.CostOverCode))
                    worksheet.Cells[currentRow, columns[CostOverExcelEnum.CostOverCode]].Value = item.CostOverCode;
                if (columns.ContainsKey(CostOverExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[CostOverExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(CostOverExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[CostOverExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = CostOverExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = CostOverExcelEnum.CostOverName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = CostOverExcelEnum.CostOverCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = CostOverExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = CostOverExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CostOverName;
                worksheet.Cells[currentRow, 3].Value = item.CostOverCode;
                worksheet.Cells[currentRow, 4].Value = item.IsActive == true ? "فعال" : "غیرفعال";
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
            6);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
