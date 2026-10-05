using System.ComponentModel;

namespace Engineering.Application.Services.TransportationContractors.Contracts.PriceWeightImportExcel;

public record PriceWeightImportExcelResponse(
    FileContentResult File
    );

public enum PriceWeightImportExcel
{
    [Description("UntilWeight")] UntilWeight = 1,
    [Description("Price")] Price = 2,
    [Description("IsFixed")] IsFixed = 3
}