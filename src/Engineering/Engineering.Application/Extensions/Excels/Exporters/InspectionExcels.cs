using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelEnum;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class InspectionExcels
{
    public static byte[] InspectionReportsToExcel(ICollection<GetsInspectionReportModel> result,
        List<InspectionReportExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "گزارش بازرسی");

        List<InspectionReportExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(InspectionReportExcelEnum))
                                      .Cast<InspectionReportExcelEnum>()
                                      .Select(x => x)
                                      .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<InspectionReportExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<InspectionReportExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in result)
        {
            currentRow++;
            ExcelHelpers.FillRow<InspectionReportExcelEnum, GetsInspectionReportModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}