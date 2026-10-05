namespace Engineering.Application.Services.CostCenters.Models.GetsByEmployerId;

public record GetsCostCenterByEmployerIdResponse(
    List<GetsCostCenterByEmployerIdModel> Data,
    int RowCount);
