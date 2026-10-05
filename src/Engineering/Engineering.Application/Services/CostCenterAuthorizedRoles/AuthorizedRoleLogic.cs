using Engineering.Application.IdentityServices.Roles.Queries.GetRoleById;
using Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRole;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.DeleteAuthorizedRole;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleGetsByCostCenterId;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.AuthorizedRoleModels;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRole;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.CreateAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Models.DeleteAuthorizedRole;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Queries.AuthorizedRoleGetsByCostCenterId;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesAndUsersInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesInclude;

namespace Engineering.Application.Services.CostCenterAuthorizedRoles;

public class AuthorizedRoleLogic : IAuthorizedRoleLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthorizedRoleLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizedRoleLogic(
        IMediator mediator,
        ILogger<AuthorizedRoleLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateAuthorizedRoleResponse?>> CreateAuthorizedRole(
        CreateAuthorizedRoleRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateAuthorizedRole, CostCenterId:{CostCenterId}, RoleId:{RoleId},", request.CostCenterId, request.AuthorizedRoleId);

        var isValidRequest = await request.IsValidAsync<CreateAuthorizedRoleValidator, CreateAuthorizedRoleRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateAuthorizedRoleResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithRolesIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateAuthorizedRoleResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<CreateAuthorizedRoleResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value!.CostCenterAuthorizedRoles.Any(oo => oo.AuthorizedRoleId == request.AuthorizedRoleId))
            return Result.Failure<CreateAuthorizedRoleResponse>(CostCenterErrors.ChildIsDuplicate);

        var rolesData = await _mediator.Send(new GetRoleByIdQuery(request.AuthorizedRoleId), ct); // بره سراغ متا دیتا
        if (rolesData.IsFailure)
            return Result.Failure<CreateAuthorizedRoleResponse>(CostCenterAuthorizedRoleErrors.UnValidAuthorizedRoles);

        var response = await _mediator.Send(new CreateAuthorizedRoleCommand(costCenter.Value!, request.AuthorizedRoleId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateAuthorizedRoleResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateAuthorizedRoleResponse(response.Value!.AuthorizedRoleId, true);
    }

    public async Task<Result<CreateAuthorizedRolesResponse?>> CreateAuthorizedRoles(
        CreateAuthorizedRolesRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateAuthorizedRoles, CostCenterId:{CostCenterId}, RoleIds:{RoleIds},", request.CostCenterId, request.RoleIds);

        var isValidRequest = await request.IsValidAsync<CreateAuthorizedRolesValidator, CreateAuthorizedRolesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateAuthorizedRolesResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithRolesAndUsersIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateAuthorizedRolesResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value is null)
            return Result.Failure<CreateAuthorizedRolesResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        if (request.RoleIds?.Count > 0 && request.RoleIds is not null)
        {
            var roleIds = request.RoleIds.Select(c => c).Where(x => x != default)?.ToList();
            var authorizedRolesData = await _mediator.Send(new GetsRoleByIdQuery(roleIds!), ct); // بره سراغ متا دیتا
            if (authorizedRolesData.IsFailure)
                return Result.Failure<CreateAuthorizedRolesResponse>(CostCenterErrors.UnValidAuthorizedRoles);

            var response = await _mediator.Send(new CreateAuthorizedRolesCommand(costCenter.Value!, request.RoleIds), ct);
            if (response.IsFailure)
                return Result.Failure<CreateAuthorizedRolesResponse>(response.Error!);
        }
        else
        {
            if (costCenter.Value.CostCenterAuthorizedRoles is not null && costCenter.Value.CostCenterAuthorizedRoles.Count > 0)
            {
                foreach (var item in costCenter.Value.CostCenterAuthorizedRoles)
                {
                    var response = await _mediator.Send(new DeleteAuthorizedRoleCommand(item.AuthorizedRoleId, request.CostCenterId), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreateAuthorizedRolesResponse>(response.Error!);
                }
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateAuthorizedRolesResponse(costCenter.Value!.Id, true);
    }

    public async Task<Result<DeleteAuthorizedRoleResponse?>> DeleteAuthorizedRole(
        DeleteAuthorizedRoleRequest request, CT ct)
    {
        _logger.LogInformation($"Request for DeleteAuthorizedRole, AuthorizedRoleId: {request.AuthorizedRoleId},  CostCenterId: {request.CostCenterId}.");

        var isValidRequest = await request.IsValidAsync<DeleteAuthorizedRoleValidator, DeleteAuthorizedRoleRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteAuthorizedRoleResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteAuthorizedRoleCommand(request.AuthorizedRoleId, request.CostCenterId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteAuthorizedRoleResponse>(response.Error!);

        var value = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new DeleteAuthorizedRoleResponse(value.AuthorizedRoleId, request.CostCenterId, true);
    }

    public async Task<Result<AuthorizedRoleGetsByCostCenterIdResponse?>> AuthorizedRoleGetsByCostCenterId(
        AuthorizedRoleGetsByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for AuthorizedRoleGetsByCostCenterId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<AuthorizedRoleGetsByCostCenterIdValidator, AuthorizedRoleGetsByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<AuthorizedRoleGetsByCostCenterIdResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<AuthorizedRoleGetsByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value is null)
            return Result.Failure<AuthorizedRoleGetsByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        var response = await _mediator.Send(new AuthorizedRoleGetsByCostCenterIdQuery(request.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<AuthorizedRoleGetsByCostCenterIdResponse>(response.Error!);
        if (response.Value?.Data?.Count <= 0)
            return Result.Failure<AuthorizedRoleGetsByCostCenterIdResponse>(CostCenterAuthorizedRoleErrors.DataIsNull);

        var roleIds = response.Value?.Data?.Select(c => c.AuthorizedRoleId).Where(x => x != default).ToList();
        var rolesData = await WebServicesLogic.RoleDataReceiver(roleIds!, _mediator, ct); // بره سراغ سرویس امنیتی

        var data = new List<AuthorizedRoleGetsByCostCenterIdModel>();
        foreach (var item in response.Value!.Data!)
            data.Add(new AuthorizedRoleGetsByCostCenterIdModel(item.AuthorizedRoleId,
                rolesData?.Where(x => x.Id == item.AuthorizedRoleId).Select(x => x.Name).FirstOrDefault()));

        return new AuthorizedRoleGetsByCostCenterIdResponse(data ?? new List<AuthorizedRoleGetsByCostCenterIdModel>(0), response.Value?.RowCount ?? 0);
    }

}