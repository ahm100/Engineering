namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroupAdmin;

public record UpdateCostCenterVirtualGroupAdminRequest(long CostCenterVirtualGroupAdminId,
                                                       long ThirdPartyId,
                                                       string UserName) : IHttpRequest;
