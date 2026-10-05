using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelEnum;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ServiceInfoExcels
{
    public static byte[] ServiceInfoToExcel(
    ICollection<GetsServiceInfoExcelExporterModel> result,
    List<ServiceInfoExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "خدمت");

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<ServiceInfoExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<ServiceInfoExcelEnum>(worksheet, excelFilters, defaultHeaders);

        var currentRow = 1;
        foreach (var item in result)
        {
            currentRow++;
            ExcelHelpers.FillRow<ServiceInfoExcelEnum, GetsServiceInfoExcelExporterModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        ExcelHelpers.ApplySummaryStyles(
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            EnumHelpers.GetEnumCount<ServiceInfoExcelEnum>());

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
