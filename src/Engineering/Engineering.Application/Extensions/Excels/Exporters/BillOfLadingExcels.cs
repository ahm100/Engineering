using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelEnum;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class BillOfLadingExcels
{
    public static byte[] BillOfLadingToExcel(
        ICollection<GetsBillOfLadingExcelExporterModel> result,
        List<BillOfLadingExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "بارنامه");

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<BillOfLadingExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<BillOfLadingExcelEnum>(
            worksheet,
            excelFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in result)
        {
            currentRow++;
            ExcelHelpers.FillRow<BillOfLadingExcelEnum, GetsBillOfLadingExcelExporterModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        ExcelHelpers.ApplySummaryStyles(
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            EnumHelpers.GetEnumCount<BillOfLadingExcelEnum>());

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}