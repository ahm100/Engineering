using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelEnum;
using Engineering.Application.Services.TransportationContractors.Contracts.PriceWeightImportExcel;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class TransportationContractorExcels
{
    public static byte[] TransportationContractorToExcel(
        ICollection<GetsFilteredTransportationContractorResponseModel> result,
        List<TransportationContractorExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "پیمانکاران حمل");

        var dataExcel = result!;

        List<TransportationContractorExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(TransportationContractorExcelEnum))
                .Cast<TransportationContractorExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        #region TransportationContractor
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<TransportationContractorExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<TransportationContractorExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<TransportationContractorExcelEnum, GetsFilteredTransportationContractorResponseModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] PriceWeightImportExcel()
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "نمونه اکسل ورودی قیمت وزنی");

        List<PriceWeightImportExcel>? newFilters = [];

        newFilters = Enum.GetValues(typeof(PriceWeightImportExcel))
            .Cast<PriceWeightImportExcel>()
            .Select(x => x)
            .ToList();

        #region TransportationContractor
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<PriceWeightImportExcel>();
        var columns = ExcelHelpers.SetupHeaders<PriceWeightImportExcel>(
            worksheet,
            newFilters,
            defaultHeaders);
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}