using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectDetailData;

public record GetProjectDetailDataResponse(
    List<GetProjectDetailDataModel> Data,
    int RowCount
    );

public class GetProjectDetailDataModel
{
    public long ProjectId { get; set; }
    public long ProjectProductId { get; set; }
    public long ProjectOperationDetailId =>
        long.Parse($"{ProjectId}{ProductGroupId ?? 0}{ProductCategoryId ?? 0}");
    public ProjectProductType Type { get; set; }
    public long? ProductGroupId { get; set; }
    public long? ProductCategoryId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public float? TolerancePercentage { get; set; } = 0;
    public float? ToleranceCount { get; set; } = 0;
    public float? TotalEstimatedCount { get; set; } = 0;
    public float? TotalRequestedCount { get; set; } = 0;
    public float? TotalSupplyCount { get; set; } = 0;
    public float? TotalRemainedCount { get; set; } = 0;
    public string? Description { get; set; } = string.Empty;
}
