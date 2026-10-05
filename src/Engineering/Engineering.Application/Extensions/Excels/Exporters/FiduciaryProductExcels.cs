using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class FiduciaryProductExcels
{
    public static byte[] FiduciaryProductToExcel(
        List<GetFilteredFiduciaryProductsExcelExporterResponseModel> data,
        List<GetFilteredFiduciaryProductDetailsExcelExporterModel> details,
        List<FiduciaryProductsExcelEnum>? excelFilters,
        List<FiduciaryProductDetailsExcelEnum>? excelDetailFilters
        )
    {
        using var workbook = new ExcelPackage();
        var worksheet1 = ExcelHelpers.CreateWorksheet(workbook, "درخواست های امانی");
        var worksheet2 = ExcelHelpers.CreateWorksheet(workbook, "کالا های درخواست امانی");

        var dataExcel = data!;
        var detailExcel = details!;

        List<FiduciaryProductsExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(FiduciaryProductsExcelEnum))
                .Cast<FiduciaryProductsExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        List<FiduciaryProductDetailsExcelEnum>? newDetailFilters = [];
        if (excelDetailFilters is null || excelDetailFilters.Count <= 0)
        {
            newDetailFilters = Enum.GetValues(typeof(FiduciaryProductDetailsExcelEnum))
                .Cast<FiduciaryProductDetailsExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newDetailFilters = excelDetailFilters;
        }

        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<FiduciaryProductsExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<FiduciaryProductsExcelEnum>(
            worksheet1,
            newFilters,
            defaultHeaders);

        var currentRow1 = 1;

        foreach (var item in dataExcel)
        {
            currentRow1++;
            ExcelHelpers.FillRow<FiduciaryProductsExcelEnum, GetFilteredFiduciaryProductsExcelExporterResponseModel>(
                worksheet1,
                item,
                currentRow1,
                columns);
        }

        var defaultDetailHeaders = ExcelHelpers.GetDefaultHeaders<FiduciaryProductDetailsExcelEnum>();
        var detailColumns = ExcelHelpers.SetupHeaders<FiduciaryProductDetailsExcelEnum>(
            worksheet2,
            newDetailFilters,
            defaultDetailHeaders);

        var currentRow2 = 1;

        foreach (var item in detailExcel)
        {
            currentRow2++;
            ExcelHelpers.FillRow<FiduciaryProductDetailsExcelEnum, GetFilteredFiduciaryProductDetailsExcelExporterModel>(
                worksheet2,
                item,
                currentRow2,
                detailColumns);
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}