using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Enum;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Enum;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class EmployerContractExcels
{
    public static byte[] GetFltrEContractHeadsToExcel(
        List<GetFltrEContractHeadsModel>? data,
        List<FltrEContractHeadsExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "گزارش سربرگ قرارداد کارفرما");

        var dataExcel = data ?? new List<GetFltrEContractHeadsModel>();

        // Filters
        var newFilters = excelFilters != null && excelFilters.Count > 0
            ? excelFilters
            : Enum.GetValues(typeof(FltrEContractHeadsExcelEnum))
                .Cast<FltrEContractHeadsExcelEnum>()
                .ToList();

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<FltrEContractHeadsExcelEnum>();
        var columns =
            ExcelHelpers.SetupHeaders<FltrEContractHeadsExcelEnum>(worksheet, newFilters, defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<FltrEContractHeadsExcelEnum, GetFltrEContractHeadsModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] GetFltrEContractsToExcel(
        List<GetFltrEContractsModel>? data,
        List<FltrEContractsEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "گزارش قرارداد کارفرما");

        var dataExcel = data ?? new List<GetFltrEContractsModel>();

        // Filters
        var newFilters = excelFilters != null && excelFilters.Count > 0
            ? excelFilters
            : Enum.GetValues(typeof(FltrEContractsEnum))
                .Cast<FltrEContractsEnum>()
                .ToList();

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<FltrEContractsEnum>();
        var columns =
            ExcelHelpers.SetupHeaders<FltrEContractsEnum>(worksheet, newFilters, defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<FltrEContractsEnum, GetFltrEContractsModel>(
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
