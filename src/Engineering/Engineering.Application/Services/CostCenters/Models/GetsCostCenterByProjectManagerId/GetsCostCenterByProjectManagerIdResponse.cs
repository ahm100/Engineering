
namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByProjectManagerId;

public record GetsCostCenterByProjectManagerIdResponse(
    List<GetsCostCenterByProjectManagerIdModel> Data,
    int RowCount);
