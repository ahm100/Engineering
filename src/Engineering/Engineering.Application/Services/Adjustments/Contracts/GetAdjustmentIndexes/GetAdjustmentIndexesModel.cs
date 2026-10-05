
namespace Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
public class GetAdjustmentIndexesModel
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string AdjustmentReferenceTitle { get; set; } = string.Empty;
    public long BranchId { get; set; }
    public string BranchTitle { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonTitle { get; set; }
    public bool IsActive { get; set; }
}