namespace Engineering.Application.Services.OperationInfos.Models.FehrestBaha;
public class FehrestBahaExcelModel
{
    public string OperationInfoCode { get; set; } = string.Empty;

    public string OperationInfoName { get; set; } = string.Empty;

    public string UnitOfMeasurement { get; set; } = string.Empty;

    public decimal BasePrice { get; set; }

    public string SeasonCode { get; set; } = string.Empty;
}