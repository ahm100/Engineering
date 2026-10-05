using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelEnum;
using Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class CostCenterTypeExcels
{
    public static byte[] CostCenterTypeToExcel(
        ICollection<GetsCostCenterTypeExcelExporterModel> result,
        List<CostCenterTypeExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("نوع مرکز هزینه");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<CostCenterTypeExcelEnum, int>();
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
                if (columns.ContainsKey(CostCenterTypeExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[CostCenterTypeExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(CostCenterTypeExcelEnum.CostCenterTypeTitle))
                    worksheet.Cells[currentRow, columns[CostCenterTypeExcelEnum.CostCenterTypeTitle]].Value = item.CostCenterTypeTitle;
                if (columns.ContainsKey(CostCenterTypeExcelEnum.CostCenterTypeCode))
                    worksheet.Cells[currentRow, columns[CostCenterTypeExcelEnum.CostCenterTypeCode]].Value = item.CostCenterTypeCode;
                if (columns.ContainsKey(CostCenterTypeExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[CostCenterTypeExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(CostCenterTypeExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[CostCenterTypeExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = CostCenterTypeExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = CostCenterTypeExcelEnum.CostCenterTypeTitle.GetEnumDescription();
            worksheet.Cells[1, 3].Value = CostCenterTypeExcelEnum.CostCenterTypeCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = CostCenterTypeExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = CostCenterTypeExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CostCenterTypeTitle;
                worksheet.Cells[currentRow, 3].Value = item.CostCenterTypeCode;
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
