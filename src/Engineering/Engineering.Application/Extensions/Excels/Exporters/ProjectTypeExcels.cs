using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelEnum;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;
public class ProjectTypeExcels
{
    public static byte[] ProjectTypeToExcel(
        ICollection<GetsProjectTypeExcelExporterModel> result,
        List<ProjectTypeExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("نوع پروژه");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ProjectTypeExcelEnum, int>();
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
                if (columns.ContainsKey(ProjectTypeExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ProjectTypeExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ProjectTypeExcelEnum.ProjectTypeName))
                    worksheet.Cells[currentRow, columns[ProjectTypeExcelEnum.ProjectTypeName]].Value = item.ProjectTypeName;
                if (columns.ContainsKey(ProjectTypeExcelEnum.ProjectTypeCode))
                    worksheet.Cells[currentRow, columns[ProjectTypeExcelEnum.ProjectTypeCode]].Value = item.ProjectTypeCode;
                if (columns.ContainsKey(ProjectTypeExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[ProjectTypeExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(ProjectTypeExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[ProjectTypeExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = ProjectTypeExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = ProjectTypeExcelEnum.ProjectTypeName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = ProjectTypeExcelEnum.ProjectTypeCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = ProjectTypeExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = ProjectTypeExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.ProjectTypeName;
                worksheet.Cells[currentRow, 3].Value = item.ProjectTypeCode;
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
