using Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.GetCostCenterVirtualGroupByCostCenterId;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroupAdmin;

namespace Engineering.Application.Services.CostCenterVirtualGroups;

public interface ICostCenterVirtualGroupLogic
{
    Task<Result<CreateCostCenterVirtualGroupResponse?>> CreateCostCenterVirtualGroupAsync(
        CreateCostCenterVirtualGroupRequest request, CT ct);

    Task<Result<CreateCostCenterVirtualGroupAdminResponse?>> CreateCostCenterVirtualGroupAdminAsync(
        CreateCostCenterVirtualGroupAdminRequest request, CT ct);

    Task<Result<DeleteCostCenterVirtualGroupResponse?>> DeleteCostCenterVirtualGroupAsync(
        DeleteCostCenterVirtualGroupRequest request, CT ct);

    Task<Result<DeleteCostCenterVirtualGroupAdminResponse?>> DeleteCostCenterVirtualGroupAdminAsync(
        DeleteCostCenterVirtualGroupAdminRequest request, CT ct);

    Task<Result<GetCostCenterVirtualGroupByCostCenterIdResponse?>> GetCostCenterVirtualGroupByCostCenterIdAsync(
        GetCostCenterVirtualGroupByCostCenterIdRequest request, CT ct);

    Task<Result<UpdateCostCenterVirtualGroupResponse?>> UpdateCostCenterVirtualGroupAsync(
        UpdateCostCenterVirtualGroupRequest request, CT ct);

    Task<Result<UpdateCostCenterVirtualGroupAdminResponse?>> UpdateCostCenterVirtualGroupAdminAsync(
        UpdateCostCenterVirtualGroupAdminRequest request, CT ct);
}
