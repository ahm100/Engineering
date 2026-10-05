using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelEnum;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class MachineriesGroupExcels
{
    public static byte[] MachineriesGroupToExcel(
        ICollection<GetsMachineriesGroupExcelExporterModel> result,
        List<MachineriesGroupExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گروه ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<MachineriesGroupExcelEnum, int>();
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
                if (columns.ContainsKey(MachineriesGroupExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[MachineriesGroupExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(MachineriesGroupExcelEnum.GroupName))
                    worksheet.Cells[currentRow, columns[MachineriesGroupExcelEnum.GroupName]].Value = item.GroupName;
                if (columns.ContainsKey(MachineriesGroupExcelEnum.GroupCode))
                    worksheet.Cells[currentRow, columns[MachineriesGroupExcelEnum.GroupCode]].Value = item.GroupCode;
                if (columns.ContainsKey(MachineriesGroupExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[MachineriesGroupExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(MachineriesGroupExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[MachineriesGroupExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = MachineriesGroupExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = MachineriesGroupExcelEnum.GroupName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = MachineriesGroupExcelEnum.GroupCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = MachineriesGroupExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = MachineriesGroupExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.GroupName;
                worksheet.Cells[currentRow, 3].Value = item.GroupCode;
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
