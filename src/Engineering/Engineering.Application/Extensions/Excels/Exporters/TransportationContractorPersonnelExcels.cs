using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelEnum;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class TransportationContractorPersonnelExcels
{
    public static byte[] TransportationContractorPersonnelToExcel(
        ICollection<GetsFilteredTransportationContractorPersonnelResponseModel> result,
        List<TransportationContractorPersonnelExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "پرسنل های پیمانکاران حمل");

        var dataExcel = result!;

        List<TransportationContractorPersonnelExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(TransportationContractorPersonnelExcelEnum))
                .Cast<TransportationContractorPersonnelExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        #region TransportationContractorPersonnel
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<TransportationContractorPersonnelExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<TransportationContractorPersonnelExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<TransportationContractorPersonnelExcelEnum, GetsFilteredTransportationContractorPersonnelResponseModel>(
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
}