namespace Engineering.Application.Services.Adjustments.Contracts;

public class AdjustmentIndexExcelModel
{
    public string IndexName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
    public string? SeasonCode { get; set; }
    public bool IsActive { get; set; }
}

public class AdjustmentIndexValueExcelModel
{
    public string IndexName { get; set; } = string.Empty;
    public string YearName { get; set; } = string.Empty;
    public int Period { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Value { get; set; }
}