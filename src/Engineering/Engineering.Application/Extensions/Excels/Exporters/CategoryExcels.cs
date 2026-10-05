using Engineering.Application.Services.Categories.Models.GetsCategoryExcelEnum;
using Engineering.Application.Services.Categories.Models.GetsCategoryExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;
public class CategoryExcels
{
    public static byte[] CategoryToExcel(
        ICollection<GetsCategoryExcelExporterModel> result,
        List<CategoryExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("رسته");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<CategoryExcelEnum, int>();
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
                if (columns.ContainsKey(CategoryExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[CategoryExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(CategoryExcelEnum.CategoryName))
                    worksheet.Cells[currentRow, columns[CategoryExcelEnum.CategoryName]].Value = item.CategoryName;
                if (columns.ContainsKey(CategoryExcelEnum.CategoryCode))
                    worksheet.Cells[currentRow, columns[CategoryExcelEnum.CategoryCode]].Value = item.CategoryCode;
                if (columns.ContainsKey(CategoryExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[CategoryExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(CategoryExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[CategoryExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = CategoryExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = CategoryExcelEnum.CategoryName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = CategoryExcelEnum.CategoryCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = CategoryExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = CategoryExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CategoryName;
                worksheet.Cells[currentRow, 3].Value = item.CategoryCode;
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