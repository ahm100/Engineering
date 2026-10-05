using Gita.Backend.Shared.Domain.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Engineering.Api.Helpers.ExcelTools;

public static class ExcelExporter
{
    public static byte[] ExportToExcel<TModel, TEnum>(
        List<TModel> data,
        List<TEnum>? excelFilters,
        string sheetName)
        where TEnum : struct, Enum
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, sheetName);

        var filters = excelFilters == null || excelFilters.Count == 0
            ? Enum.GetValues<TEnum>().ToList()
            : excelFilters;

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<TEnum>();
        var columns = ExcelHelpers.SetupHeaders(
            worksheet, filters, defaultHeaders);

        var currentRow = 1;
        foreach (var item in data)
        {
            currentRow++;
            ExcelHelpers.FillRow(
                worksheet, item, currentRow, columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] ExportToExcel<TModel, TChildModel, TEnum, TChildEnum>(
        List<TModel> data,
        List<TChildModel>? childData,
        List<TEnum>? excelFilters,
        List<TChildEnum>? childExcelFilters,
        string sheetName,
        string? childSheetName)
        where TEnum : struct, Enum
        where TChildEnum : struct, Enum
    {
        using var workbook = new ExcelPackage();

        var worksheet = ExcelHelpers.CreateWorksheet(workbook, sheetName);
        var childWorksheet = ExcelHelpers.CreateWorksheet(workbook, childSheetName ?? "Childs Data");

        var filters = excelFilters == null || excelFilters.Count == 0
            ? Enum.GetValues<TEnum>().ToList()
            : excelFilters;

        var childFilters = childExcelFilters == null || childExcelFilters.Count == 0
            ? Enum.GetValues<TChildEnum>().ToList()
            : childExcelFilters;

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<TEnum>();
        var childDefaultHeaders = ExcelHelpers.GetDefaultHeaders<TChildEnum>();

        var columns = ExcelHelpers.SetupHeaders(worksheet, filters, defaultHeaders);
        var childColumns = ExcelHelpers.SetupHeaders(childWorksheet, childFilters, childDefaultHeaders);

        var currentRow = 1;
        foreach (var item in data)
        {
            currentRow++;
            ExcelHelpers.FillRow(worksheet, item, currentRow, columns);
        }

        var childCurrentRow = 1;
        if (childData != null && childData.Count > 0)
            foreach (var childItem in childData)
            {
                childCurrentRow++;
                ExcelHelpers.FillRow(childWorksheet, childItem, childCurrentRow, childColumns);
            }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] ExportImportErrorsToExcel<TModel, TEnum>(
    List<TModel> data,
    string sheetName)
    where TEnum : struct, Enum
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add(sheetName);

        // Force Left-To-Right for import templates
        worksheet.View.RightToLeft = false;

        var headers = Enum.GetValues<TEnum>().ToList();
        var columns = new Dictionary<TEnum, int>();

        // Row 1: English Headers (Enum Name)
        for (int i = 0; i < headers.Count; i++)
        {
            worksheet.Cells[1, i + 1].Value = headers[i].ToString();
            columns[headers[i]] = i + 1;
        }

        ExcelStyles.SetHeaderStyle(
            worksheet.Cells[1, 1, 1, headers.Count],
            ExcelBorderStyle.Medium);

        // Row 2: Persian Headers (Enum Description)
        for (int i = 0; i < headers.Count; i++)
            worksheet.Cells[2, i + 1].Value = headers[i].GetEnumDescription();

        ExcelStyles.SetHeaderStyle(
            worksheet.Cells[2, 1, 2, headers.Count],
            ExcelBorderStyle.Medium);

        // Used to detect which rows have an error (safe: null if model has no Error property)
        var errorProperty = typeof(TModel).GetProperty("Error");

        // Data rows start at Row 3
        var currentRow = 2;
        foreach (var item in data)
        {
            currentRow++;
            ExcelHelpers.FillRow(worksheet, item, currentRow, columns);

            // Highlight the whole row if it has an error
            var errorValue = errorProperty?.GetValue(item)?.ToString();
            if (!string.IsNullOrWhiteSpace(errorValue))
            {
                using var rowRange = worksheet.Cells[currentRow, 1, currentRow, headers.Count];

                // Light red background (Excel "Bad" style: #FFC7CE)
                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 199, 206));

                // Dark red text (#9C0006)
                rowRange.Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(156, 0, 6));
            }
        }

        if (worksheet.Dimension != null)
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}