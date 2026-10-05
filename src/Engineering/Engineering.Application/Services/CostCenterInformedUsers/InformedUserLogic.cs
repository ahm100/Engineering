using Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Commands.CreateInformedUsers;
using Engineering.Application.Services.CostCenterInformedUsers.Commands.DeleteInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUsers;
using Engineering.Application.Services.CostCenterInformedUsers.Models.DeleteInformedUser;
using Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserGetsByCostCenterId;
using Engineering.Application.Services.CostCenterInformedUsers.Models.InformedUserModels;
using Engineering.Application.Services.CostCenterInformedUsers.Queries.InformedUserGetsByCostCenterId;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithInformedUsersInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;

namespace Engineering.Application.Services.CostCenterInformedUsers;

public class InformedUserLogic : IInformedUserLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<InformedUserLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public InformedUserLogic(
        IMediator mediator,
        ILogger<InformedUserLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateInformedUserResponse?>> CreateInformedUser(
        CreateInformedUserRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateInformedUser, CostCenterId:{CostCenterId}, UserId:{UserId},", request.CostCenterId, request.EmployeeId);

        var isValidRequest = await request.IsValidAsync<CreateInformedUserValidator, CreateInformedUserRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateInformedUserResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithInformedUsersIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateInformedUserResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<CreateInformedUserResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value!.InformedUsers.Any(oo => oo.EmployeeId == request.EmployeeId))
            return Result.Failure<CreateInformedUserResponse>(CostCenterErrors.ChildIsDuplicate);

        var informedUserIds = new List<long> { request.EmployeeId };
        var informedUserData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, informedUserIds.Count, informedUserIds, null, false, null), ct); // بره سراغ متا دیتا
        if (informedUserData.IsFailure)
            return Result.Failure<CreateInformedUserResponse>(CostCenterErrors.UnValidInformedUsers);

        var response = await _mediator.Send(new CreateInformedUserCommand(costCenter.Value!, request.EmployeeId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateInformedUserResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        var value = response.Value!;

        return new CreateInformedUserResponse(value.EmployeeId, true);
    }

    public async Task<Result<CreateInformedUsersResponse?>> CreateInformedUsers(
        CreateInformedUsersRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateInformedUsers, CostCenterId:{CostCenterId}, UserId:{UserId},", request.CostCenterId, request.EmployeeIds);

        var isValidRequest = await request.IsValidAsync<CreateInformedUsersValidator, CreateInformedUsersRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateInformedUsersResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithInformedUsersIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateInformedUsersResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value is null)
            return Result.Failure<CreateInformedUsersResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        if (request.EmployeeIds?.Count > 0 && request.EmployeeIds is not null)
        {
            var Ids = request.EmployeeIds.Select(c => (long)c!).Where(x => x != default).ToList();
            var informedUsersData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, Ids!.Count, Ids, null, false, null), ct); // بره سراغ متا دیتا
            if (informedUsersData.IsFailure)
                return Result.Failure<CreateInformedUsersResponse>(CostCenterErrors.UnValidAuthorizedUsers);

            var response = await _mediator.Send(new CreateInformedUsersCommand(costCenter.Value!, request.EmployeeIds), ct);
            if (response.IsFailure)
                return Result.Failure<CreateInformedUsersResponse>(response.Error!);
        }
        else
        {
            if (costCenter.Value.InformedUsers is not null && costCenter.Value.InformedUsers.Count > 0)
            {
                foreach (var item in costCenter.Value.InformedUsers)
                {
                    var response = await _mediator.Send(new DeleteInformedUserCommand(item.EmployeeId, request.CostCenterId), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreateInformedUsersResponse>(response.Error!);
                }
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreateInformedUsersResponse(true);
    }

    public async Task<Result<DeleteInformedUserResponse?>> DeleteInformedUser(
        DeleteInformedUserRequest request, CT ct)
    {
        _logger.LogInformation($"Request for DeleteInformedUser, UserId: {request.EmployeeId},  CostCenterId: {request.CostCenterId}.");

        var isValidRequest = await request.IsValidAsync<DeleteInformedUserValidator, DeleteInformedUserRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteInformedUserResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteInformedUserCommand(request.EmployeeId, request.CostCenterId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteInformedUserResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteInformedUserResponse(response.Value!.EmployeeId, true);
    }

    public async Task<Result<InformedUserGetsByCostCenterIdResponse?>> InformedUserGetsByCostCenterId(
        InformedUserGetsByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for InformedUserGetsByCostCenterId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<InformedUserGetsByCostCenterIdValidator, InformedUserGetsByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<InformedUserGetsByCostCenterIdResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<InformedUserGetsByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value is null)
            return Result.Failure<InformedUserGetsByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        var response = await _mediator.Send(new InformedUserGetsByCostCenterIdQuery(request.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<InformedUserGetsByCostCenterIdResponse>(response.Error!);
        if (response.Value?.Data?.Count <= 0)
            return Result.Failure<InformedUserGetsByCostCenterIdResponse>(CostCenterInformedUserErrors.DataIsNull);

        var informedUserIds = response.Value?.Data?.Select(c => c.EmployeeId).Where(x => x != default).ToList();
        var informedUsersData = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(informedUserIds, null, null, _mediator, ct); // بره سراغ متا دیتا

        var data = new List<InformedUserGetsByCostCenterIdModel>();
        foreach (var item in response.Value!.Data!)
            data.Add(new InformedUserGetsByCostCenterIdModel(item.EmployeeId,
                informedUsersData?.Where(x => x?.Id == item.EmployeeId).Select(x => x?.FullName).FirstOrDefault(),
                informedUsersData?.Where(x => x?.Id == item.EmployeeId).Select(x => x?.OrganizationCode).FirstOrDefault()));

        return new InformedUserGetsByCostCenterIdResponse(data ?? new List<InformedUserGetsByCostCenterIdModel>(0), response.Value?.RowCount ?? 0);
    }

}