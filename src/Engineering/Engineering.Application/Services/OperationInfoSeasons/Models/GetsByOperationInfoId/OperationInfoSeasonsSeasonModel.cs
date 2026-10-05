
namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

public record OperationInfoSeasonsSeasonModel
{
    public long Id { get; set; }
    public long BranchId { get; set; }
    public long OperationInfoSeasonId { get; set; }
    public string SeasonName { get; set; } = string.Empty;
    public string SeasonCode { get; set; } = string.Empty;
    public bool IsLast { get; set; }
    public bool IsActive { get; set; }
}
