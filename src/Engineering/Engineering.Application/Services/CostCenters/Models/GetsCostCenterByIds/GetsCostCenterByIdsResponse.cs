
namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByIds;

public record GetsCostCenterByIdsResponse(
    List<GetsCostCenterByIdsResponseModel> Data,
    int RowCount);
