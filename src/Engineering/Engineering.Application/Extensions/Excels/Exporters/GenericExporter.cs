namespace Engineering.Application.Extensions.Excels.Exporters;

public class GenericExporter
{
    public static byte[] ExportToExcel<TModel, TEnum>(
        List<TModel> data,
        List<TEnum>? excelFilters,
        string sheetName)
        where TEnum : struct, Enum
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, sheetName);

        var filters = (excelFilters == null || excelFilters.Count == 0)
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

}
