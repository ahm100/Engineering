namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverByNamesOrCodes;

public record GetsCostOverByNamesOrCodesModel
{
    public long Id { get; set; }
    public string CostOverName { get; set; } = string.Empty;
    public string CostOverCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}