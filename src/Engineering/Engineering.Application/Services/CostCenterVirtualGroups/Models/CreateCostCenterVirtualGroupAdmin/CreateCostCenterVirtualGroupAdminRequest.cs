namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroupAdmin;

public record CreateCostCenterVirtualGroupAdminRequest(long ThirdPartyId,
                                                       string UserName,
                                                       long CostCenterVirtualGroupId) : IHttpRequest;
