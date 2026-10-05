namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.GetCostCenterVirtualGroupByCostCenterId;

public record GetCostCenterVirtualGroupByCostCenterIdRequest(long CostCenterId,
                                                             int PageIndex,
                                                             int PageSize) : IHttpRequest;
