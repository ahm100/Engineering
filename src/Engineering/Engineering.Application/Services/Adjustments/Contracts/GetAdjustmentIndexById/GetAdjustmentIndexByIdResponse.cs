namespace Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;

public class GetAdjustmentIndexByIdResponse
{
    public long Id { get; set; }
    public long YearId { get; set; }
    public long AdjustmentReferenceId { get; set; }
    public string AdjustmentReferenceTitle { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string BranchTitle { get; set; } = string.Empty;

    public long? SeasonId { get; set; }
    public string? SeasonTitle { get; set; }

    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DocumentFile { get; set; }
    public bool IsActive { get; set; }

    public List<GetAdjustmentIndexValueModel> Values { get; set; } = [];
}
