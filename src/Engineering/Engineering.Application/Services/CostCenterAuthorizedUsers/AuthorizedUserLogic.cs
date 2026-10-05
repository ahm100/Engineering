using Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;
using Engineering.Application.IdentityServices.Users.Models;
using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.CreateAuthorizedRoles;
using Engineering.Application.Services.CostCenterAuthorizedRoles.Commands.DeleteAuthorizedRole;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUser;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.CreateAuthorizedUsers;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Commands.DeleteAuthorizedUser;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserGetsByCostCenterId;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.AuthorizedUserModels;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUser;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.CreateAuthorizedUsers;
using Engineering.Application.Services.CostCenterAuthorizedUsers.Models.DeleteAuthorizedUser;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithRolesAndUsersInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithUsersInclude;

namespace Engineering.Application.Services.CostCenterAuthorizedUsers;

public class AuthorizedUserLogic : IAuthorizedUserLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthorizedUserLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizedUserLogic(
        IMediator mediator,
        ILogger<AuthorizedUserLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateAuthorizedUserResponse?>> CreateAuthorizedUser(
        CreateAuthorizedUserRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateAuthorizedUser, CostCenterId:{CostCenterId}, AuthorizedUserId:{AuthorizedUserId},", request.CostCenterId, request.AuthorizedUserId);

        var isValidRequest = await request.IsValidAsync<CreateAuthorizedUserValidator, CreateAuthorizedUserRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateAuthorizedUserResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithUsersIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateAuthorizedUserResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<CreateAuthorizedUserResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value!.CostCenterAuthorizedUsers.Any(oo => oo.AuthorizedUserId == request.AuthorizedUserId))
            return Result.Failure<CreateAuthorizedUserResponse>(CostCenterErrors.ChildIsDuplicate);

        var userIds = new List<long>
        {
            request.AuthorizedUserId
        };
        var userData = await _mediator.Send(new GetsUserByIdQuery(userIds), ct); // بره سراغ متا دیتا
        if (userData.IsFailure)
            return Result.Failure<CreateAuthorizedUserResponse>(CostCenterAuthorizedUserErrors.UnValidAuthorizedUser);

        var response = await _mediator.Send(new CreateAuthorizedUserCommand(costCenter.Value!, request.AuthorizedUserId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateAuthorizedUserResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateAuthorizedUserResponse(response.Value!.AuthorizedUserId, true);
    }

    public async Task<Result<CreateAuthorizedUsersResponse?>> CreateAuthorizedUsers(
        CreateAuthorizedUsersRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateAuthorizedUsers, CostCenterId:{CostCenterId}, UserId:{UserId},", request.CostCenterId, request.UserIds);

        var isValidRequest = await request.IsValidAsync<CreateAuthorizedUsersValidator, CreateAuthorizedUsersRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateAuthorizedUsersResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithRolesAndUsersIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateAuthorizedUsersResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value is null)
            return Result.Failure<CreateAuthorizedUsersResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        if (request.UserIds?.Count > 0 && request.UserIds is not null)
        {
            var userIds = request.UserIds.Select(c => c).Where(x => x != default).ToList();
            var authorizedUsersData = await _mediator.Send(new GetsUserByIdQuery(userIds), ct); // بره سراغ متا دیتا
            if (authorizedUsersData.IsFailure)
                return Result.Failure<CreateAuthorizedUsersResponse>(CostCenterErrors.UnValidAuthorizedUsers);

            var response = await _mediator.Send(new CreateAuthorizedUsersCommand(costCenter.Value!, request.UserIds), ct);
            if (response.IsFailure)
                return Result.Failure<CreateAuthorizedUsersResponse>(response.Error!);
        }
        else
        {
            if (costCenter.Value.CostCenterAuthorizedUsers is not null && costCenter.Value.CostCenterAuthorizedUsers.Count > 0)
            {
                foreach (var item in costCenter.Value.CostCenterAuthorizedUsers)
                {
                    var response = await _mediator.Send(new DeleteAuthorizedUserCommand(item.AuthorizedUserId, request.CostCenterId), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreateAuthorizedUsersResponse>(response.Error!);
                }
            }
        }

        if (request.RoleIds?.Count > 0 && request.RoleIds is not null)
        {
            var roleIds = request.RoleIds.Select(c => c).Where(x => x != default)?.ToList();
            var authorizedRolesData = await _mediator.Send(new GetsRoleByIdQuery(roleIds!), ct); // بره سراغ متا دیتا
            if (authorizedRolesData.IsFailure)
                return Result.Failure<CreateAuthorizedUsersResponse>(CostCenterErrors.UnValidAuthorizedRoles);

            var response = await _mediator.Send(new CreateAuthorizedRolesCommand(costCenter.Value!, request.RoleIds), ct);
            if (response.IsFailure)
                return Result.Failure<CreateAuthorizedUsersResponse>(response.Error!);
        }
        else
        {
            if (costCenter.Value.CostCenterAuthorizedRoles is not null && costCenter.Value.CostCenterAuthorizedRoles.Count > 0)
            {
                foreach (var item in costCenter.Value.CostCenterAuthorizedRoles)
                {
                    var response = await _mediator.Send(new DeleteAuthorizedRoleCommand(item.AuthorizedRoleId, request.CostCenterId), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreateAuthorizedUsersResponse>(response.Error!);
                }
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateAuthorizedUsersResponse(true);
    }

    public async Task<Result<DeleteAuthorizedUserResponse?>> DeleteAuthorizedUser(
        DeleteAuthorizedUserRequest request, CT ct)
    {
        _logger.LogInformation($"Request for DeleteAuthorizedUser, UserId: {request.UserId},  CostCenterId: {request.CostCenterId}.");

        var isValidRequest = await request.IsValidAsync<DeleteAuthorizedUserValidator, DeleteAuthorizedUserRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteAuthorizedUserResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteAuthorizedUserCommand(request.UserId, request.CostCenterId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteAuthorizedUserResponse>(response.Error!);

        var value = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new DeleteAuthorizedUserResponse(value.AuthorizedUserId, request.CostCenterId, true);
    }

    public async Task<Result<AuthorizedUserGetsByCostCenterIdResponse?>> AuthorizedUserGetsByCostCenterId(
        AuthorizedUserGetsByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for AuthorizedUserGetsByCostCenterId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<AuthorizedUserGetsByCostCenterIdValidator, AuthorizedUserGetsByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<AuthorizedUserGetsByCostCenterIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (response.IsFailure)
            return Result.Failure<AuthorizedUserGetsByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (response.Value is null)
            return Result.Failure<AuthorizedUserGetsByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (response.Value.CostCenterAuthorizedUsers.Count <= 0)
            return Result.Failure<AuthorizedUserGetsByCostCenterIdResponse>(CostCenterAuthorizedUserErrors.DataIsNull);

        var userIds = response.Value?.CostCenterAuthorizedUsers!.Select(c => c.AuthorizedUserId).Where(x => x != default).ToList();
        var userData = new List<User>();
        var data = new List<AuthorizedUserGetsByCostCenterIdModel>();
        if (response.Value!.CostCenterAuthorizedRoles.Count > 0)
        {
            var roleIds = response.Value?.CostCenterAuthorizedRoles!.Select(c => c.AuthorizedRoleId).Where(x => x != default).ToList();
            userData = await WebServicesLogic.GetUsersByRoleIdsDataReceiver(roleIds, userIds, request.FilterData, _mediator, ct); // بره سراغ آی دنتیتی
        }
        else
            userData = await WebServicesLogic.GetUsersDataReceiver(userIds, _mediator, ct); // بره سراغ متا دیتا

        if (userData?.Count <= 0 || userData is null)
            return Result.Failure<AuthorizedUserGetsByCostCenterIdResponse>(CostCenterAuthorizedUserErrors.DatasIsNull);

        foreach (var item in userData)
            data.Add(new(item.Id, item?.FullName, item?.OrganizationCode, item?.RoleIds));

        return new AuthorizedUserGetsByCostCenterIdResponse(data ?? new List<AuthorizedUserGetsByCostCenterIdModel>(0), (int)data?.Count!);
    }

}