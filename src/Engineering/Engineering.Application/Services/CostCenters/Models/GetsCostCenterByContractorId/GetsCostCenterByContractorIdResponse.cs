
namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;

public record GetsCostCenterByContractorIdResponse(
    List<GetsCostCenterByContractorIdModel> Data,
    int RowCount);
