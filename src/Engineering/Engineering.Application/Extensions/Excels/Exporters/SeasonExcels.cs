using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelEnum;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class SeasonExcels
{
    public static byte[] SeasonToExcel(
        ICollection<GetsSeasonExcelExporterModel> result,
        List<SeasonExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("فصل");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;


        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<SeasonExcelEnum, int>();
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
                if (columns.ContainsKey(SeasonExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(SeasonExcelEnum.SeasonName))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.SeasonName]].Value = item.SeasonName;
                if (columns.ContainsKey(SeasonExcelEnum.SeasonCode))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.SeasonCode]].Value = item.SeasonCode;
                if (columns.ContainsKey(SeasonExcelEnum.BranchName))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.BranchName]].Value = item.BranchName;
                if (columns.ContainsKey(SeasonExcelEnum.BranchCode))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.BranchCode]].Value = item.BranchCode;
                if (columns.ContainsKey(SeasonExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(SeasonExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[SeasonExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = SeasonExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = SeasonExcelEnum.SeasonName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = SeasonExcelEnum.SeasonCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = SeasonExcelEnum.BranchName.GetEnumDescription();
            worksheet.Cells[1, 5].Value = SeasonExcelEnum.BranchCode.GetEnumDescription();
            worksheet.Cells[1, 6].Value = SeasonExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 7].Value = SeasonExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.SeasonName;
                worksheet.Cells[currentRow, 3].Value = item.SeasonCode;
                worksheet.Cells[currentRow, 4].Value = item.BranchName;
                worksheet.Cells[currentRow, 5].Value = item.BranchCode;
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
        var content = stream.ToArray();
        return content;
    }
}