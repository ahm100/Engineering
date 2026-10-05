using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class TransportationRequestExcels
{
    public static byte[] TransportationRequestToExcel(List<GetsTransportationRequestExcelExporterResponseModel>? result,
        List<TransportationRequestExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "درخواست ترابری");

        var dataExcel = result!;

        List<TransportationRequestExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(TransportationRequestExcelEnum))
                .Cast<TransportationRequestExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<TransportationRequestExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<TransportationRequestExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;

        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<TransportationRequestExcelEnum, GetsTransportationRequestExcelExporterResponseModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] AirplaneRequestToExcel(List<GetsAirplaneExcelExporterResponseModel>? result,
        List<AirplaneExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "درخواست هواپیما");

        var dataExcel = result!;

        List<AirplaneExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(AirplaneExcelEnum))
                .Cast<AirplaneExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<AirplaneExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<AirplaneExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;

        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<AirplaneExcelEnum, GetsAirplaneExcelExporterResponseModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] SnapRequestToExcel(List<GetsSnapExcelExporterResponseModel>? result,
        List<SnapExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "درخواست اسنپ");

        var dataExcel = result!;

        List<SnapExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(SnapExcelEnum))
                .Cast<SnapExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<SnapExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<SnapExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;

        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<SnapExcelEnum, GetsSnapExcelExporterResponseModel>(
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