using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelEnum;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class OperationInfoExcels
{
    public static byte[] OperationInfoToExcel(
        ICollection<GetsOperationInfoExcelExporterModel> result,
        List<OperationInfoExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("شرح عملیات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<OperationInfoExcelEnum, int>();
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
                if (columns.ContainsKey(OperationInfoExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(OperationInfoExcelEnum.OperationInfoName))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(OperationInfoExcelEnum.OperationInfoCode))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(OperationInfoExcelEnum.OperationLatinName))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.OperationLatinName]].Value = item.OperationLatinName;
                if (columns.ContainsKey(OperationInfoExcelEnum.Priority))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.Priority]].Value = item.Priority;
                if (columns.ContainsKey(OperationInfoExcelEnum.MeasurementName))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.MeasurementName]].Value = item.MeasurementName;
                if (columns.ContainsKey(OperationInfoExcelEnum.DependencyName))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.DependencyName]].Value = item.DependencyName;
                if (columns.ContainsKey(OperationInfoExcelEnum.DependencyCode))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.DependencyCode]].Value = item.DependencyCode;
                if (columns.ContainsKey(OperationInfoExcelEnum.DependencyPriority))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.DependencyPriority]].Value = item.DependencyPriority;
                if (columns.ContainsKey(OperationInfoExcelEnum.WorkingDays))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.WorkingDays]].Value = item.WorkingDays;
                if (columns.ContainsKey(OperationInfoExcelEnum.DependencyType))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.DependencyType]].Value = item.DependencyType;
                if (columns.ContainsKey(OperationInfoExcelEnum.Categories))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.Categories]].Value = item.Categories;
                if (columns.ContainsKey(OperationInfoExcelEnum.Branchs))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.Branchs]].Value = item.Branchs;
                if (columns.ContainsKey(OperationInfoExcelEnum.Seasons))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.Seasons]].Value = item.Seasons;
                if (columns.ContainsKey(OperationInfoExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(OperationInfoExcelEnum.Groups))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.Groups]].Value = item.Groups;
                if (columns.ContainsKey(OperationInfoExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[OperationInfoExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 17],

                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = OperationInfoExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = OperationInfoExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = OperationInfoExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = OperationInfoExcelEnum.OperationLatinName.GetEnumDescription();
            worksheet.Cells[1, 5].Value = OperationInfoExcelEnum.Priority.GetEnumDescription();
            worksheet.Cells[1, 6].Value = OperationInfoExcelEnum.MeasurementName.GetEnumDescription();
            worksheet.Cells[1, 7].Value = OperationInfoExcelEnum.DependencyName.GetEnumDescription();
            worksheet.Cells[1, 8].Value = OperationInfoExcelEnum.DependencyCode.GetEnumDescription();
            worksheet.Cells[1, 9].Value = OperationInfoExcelEnum.DependencyPriority.GetEnumDescription();
            worksheet.Cells[1, 10].Value = OperationInfoExcelEnum.WorkingDays.GetEnumDescription();
            worksheet.Cells[1, 11].Value = OperationInfoExcelEnum.DependencyType.GetEnumDescription();
            worksheet.Cells[1, 12].Value = OperationInfoExcelEnum.Categories.GetEnumDescription();
            worksheet.Cells[1, 13].Value = OperationInfoExcelEnum.Branchs.GetEnumDescription();
            worksheet.Cells[1, 14].Value = OperationInfoExcelEnum.Seasons.GetEnumDescription();
            worksheet.Cells[1, 15].Value = OperationInfoExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 16].Value = OperationInfoExcelEnum.Groups.GetEnumDescription();
            worksheet.Cells[1, 17].Value = OperationInfoExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.OperationInfoName;
                worksheet.Cells[currentRow, 3].Value = item.OperationInfoCode;
                worksheet.Cells[currentRow, 4].Value = item.OperationLatinName;
                worksheet.Cells[currentRow, 5].Value = item.Priority;
                worksheet.Cells[currentRow, 6].Value = item.MeasurementName;
                worksheet.Cells[currentRow, 7].Value = item.DependencyName;
                worksheet.Cells[currentRow, 8].Value = item.DependencyCode;
                worksheet.Cells[currentRow, 9].Value = item.DependencyPriority;
                worksheet.Cells[currentRow, 10].Value = item.WorkingDays;
                worksheet.Cells[currentRow, 11].Value = item.DependencyType;
                worksheet.Cells[currentRow, 12].Value = item.Categories;
                worksheet.Cells[currentRow, 13].Value = item.Branchs;
                worksheet.Cells[currentRow, 14].Value = item.Seasons;
                worksheet.Cells[currentRow, 15].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                worksheet.Cells[currentRow, 16].Value = item.Groups;
                worksheet.Cells[currentRow, 17].Value = item.CompanyNameFa;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 19, 4, 21],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            18);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}