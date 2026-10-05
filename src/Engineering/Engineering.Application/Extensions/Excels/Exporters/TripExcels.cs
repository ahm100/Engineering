using Engineering.Application.Services.Trips.Models.GetsTripExcelEnum;
using Engineering.Application.Services.Trips.Models.GetsTripExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class TripExcels
{
    public static byte[] TripToExcel(
        ICollection<GetsTripExcelExporterModel> result,
        List<TripExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("سفر");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<TripExcelEnum, int>();
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
                if (columns.ContainsKey(TripExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[TripExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(TripExcelEnum.TripName))
                    worksheet.Cells[currentRow, columns[TripExcelEnum.TripName]].Value = item.TripName;
                if (columns.ContainsKey(TripExcelEnum.TripCode))
                    worksheet.Cells[currentRow, columns[TripExcelEnum.TripCode]].Value = item.TripCode;
                if (columns.ContainsKey(TripExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[TripExcelEnum.IsActive]].Value = item.IsActive == true ? "فعال" : "غیرفعال";
                if (columns.ContainsKey(TripExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[TripExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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

            worksheet.Cells[1, 1].Value = TripExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = TripExcelEnum.TripName.GetEnumDescription();
            worksheet.Cells[1, 3].Value = TripExcelEnum.TripCode.GetEnumDescription();
            worksheet.Cells[1, 4].Value = TripExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 5].Value = TripExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.TripName;
                worksheet.Cells[currentRow, 3].Value = item.TripCode;
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
            worksheet.Cells[2, 7, 4, 9],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            6);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
