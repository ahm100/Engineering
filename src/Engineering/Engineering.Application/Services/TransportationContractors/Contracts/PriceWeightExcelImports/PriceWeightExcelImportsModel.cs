namespace Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;

public record PriceWeightExcelImportsModel
{
    public string UntilWeight { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public int IsFixed { get; set; }
}