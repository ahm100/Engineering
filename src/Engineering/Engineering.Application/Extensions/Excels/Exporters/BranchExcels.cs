using Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class BranchExcels
{
    public static byte[] BranchToExcel(
        ICollection<GetsBranchExcelExporterModel> result,
        List<BranchExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("رشته");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<BranchExcelEnum, int>();
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
                if (columns.ContainsKey(BranchExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(BranchExcelEnum.BranchName))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.BranchName]].Value = item.BranchName;
                if (columns.ContainsKey(BranchExcelEnum.BranchCode))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.BranchCode]].Value = item.BranchCode;
                if (columns.ContainsKey(BranchExcelEnum.CategoryName))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.CategoryName]].Value = item.CategoryName;
                if (columns.ContainsKey(BranchExcelEnum.CategoryCode))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.CategoryCode]].Value = item.CategoryCode;
                if (columns.ContainsKey(BranchExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(BranchExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[BranchExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = BranchExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = BranchExcelEnum.BranchName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = BranchExcelEnum.BranchCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = BranchExcelEnum.CategoryName.GetEnumDescription();
            worksheet.Cells[1, 5].Value = BranchExcelEnum.CategoryCode.GetEnumDescription();
            worksheet.Cells[1, 6].Value = BranchExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 7].Value = BranchExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.BranchName;
                worksheet.Cells[currentRow, 3].Value = item.BranchCode;
                worksheet.Cells[currentRow, 4].Value = item.CategoryName;
                worksheet.Cells[currentRow, 5].Value = item.CategoryCode;
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