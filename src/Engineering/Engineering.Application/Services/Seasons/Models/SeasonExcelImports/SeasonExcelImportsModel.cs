namespace Engineering.Application.Services.Seasons.Models.SeasonExcelImports;

public record SeasonExcelImportsModel
{
    public string SeasonName { get; private set; } = string.Empty;
    public string SeasonCode { get; private set; } = string.Empty;
    public string BranchCode { get; private set; } = string.Empty;
    public string CategoryCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}
