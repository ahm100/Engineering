using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelEnum;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class TransportationContractorMachineExcels
{
    public static byte[] TransportationContractorMachineToExcel(
        ICollection<GetsFilteredTransportationContractorMachineResponseModel> result,
        List<TransportationContractorMachineExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "ماشین های پیمانکاران حمل");

        var dataExcel = result!;

        List<TransportationContractorMachineExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(TransportationContractorMachineExcelEnum))
                .Cast<TransportationContractorMachineExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        #region TransportationContractorMachine
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<TransportationContractorMachineExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<TransportationContractorMachineExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<TransportationContractorMachineExcelEnum, GetsFilteredTransportationContractorMachineResponseModel>(
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