using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Commands.CreateCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Commands.DeleteCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Commands.UpdateCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.CreateCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.DeleteCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.GetCostCenterVirtualGroupByCostCenterId;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroup;
using Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroupAdmin;
using Engineering.Application.Services.CostCenterVirtualGroups.Queries.CostCenterVirtualGroupById;
using Engineering.Application.Services.CostCenterVirtualGroups.Queries.GetCreateCostCenterVirtualGroupById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;

namespace Engineering.Application.Services.CostCenterVirtualGroups;

public class CostCenterVirtualGroupLogic : ICostCenterVirtualGroupLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CostCenterVirtualGroupLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CostCenterVirtualGroupLogic(
        IMediator mediator,
        ILogger<CostCenterVirtualGroupLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateCostCenterVirtualGroupAdminResponse?>> CreateCostCenterVirtualGroupAdminAsync(
        CreateCostCenterVirtualGroupAdminRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateCostCenterVirtualGroupAdminValidator, CreateCostCenterVirtualGroupAdminRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCostCenterVirtualGroupAdminResponse>(isValidRequest.Error!);

        var getCreateCostCenterVirtualGroupByIdQuery = await _mediator.Send(new GetCreateCostCenterVirtualGroupByIdQuery(request.CostCenterVirtualGroupId), ct);
        if (getCreateCostCenterVirtualGroupByIdQuery.IsFailure)
            return Result.Failure<CreateCostCenterVirtualGroupAdminResponse>(getCreateCostCenterVirtualGroupByIdQuery.Error!);
        var costCenterVirtualGroup = getCreateCostCenterVirtualGroupByIdQuery.Value!;

        var createCostCenterVirtualGroupAdminCommand = await _mediator.Send(new CreateCostCenterVirtualGroupAdminCommand(request.ThirdPartyId, request.UserName, costCenterVirtualGroup), ct);
        if (createCostCenterVirtualGroupAdminCommand.IsFailure)
            return Result.Failure<CreateCostCenterVirtualGroupAdminResponse>(createCostCenterVirtualGroupAdminCommand.Error!);
        var costCenterVirtualGroupAdmin = createCostCenterVirtualGroupAdminCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateCostCenterVirtualGroupAdminResponse(costCenterVirtualGroupAdmin.Id);
    }

    public async Task<Result<CreateCostCenterVirtualGroupResponse?>> CreateCostCenterVirtualGroupAsync(
        CreateCostCenterVirtualGroupRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateCostCenterVirtualGroupValidator, CreateCostCenterVirtualGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCostCenterVirtualGroupResponse>(isValidRequest.Error!);

        var getCostCenterByIdQuery = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (getCostCenterByIdQuery.IsFailure)
            return Result.Failure<CreateCostCenterVirtualGroupResponse>(getCostCenterByIdQuery.Error!);
        var costCenter = getCostCenterByIdQuery.Value!;

        var createCostCenterVirtualGroupCommand = await _mediator.Send(new CreateCostCenterVirtualGroupCommand(request.Title, request.Link, request.Identifier, request.Description, request.SendToday, request.TodayTime, request.SendYesterday, request.YesterdayTime, costCenter), ct);
        if (createCostCenterVirtualGroupCommand.IsFailure)
            return Result.Failure<CreateCostCenterVirtualGroupResponse>(createCostCenterVirtualGroupCommand.Error!);
        var costCenterVirtualGroup = createCostCenterVirtualGroupCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new CreateCostCenterVirtualGroupResponse(costCenterVirtualGroup.Id);
    }

    public async Task<Result<DeleteCostCenterVirtualGroupAdminResponse?>> DeleteCostCenterVirtualGroupAdminAsync(
        DeleteCostCenterVirtualGroupAdminRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteCostCenterVirtualGroupAdminValidator, DeleteCostCenterVirtualGroupAdminRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteCostCenterVirtualGroupAdminResponse>(isValidRequest.Error!);

        var deleteCostCenterVirtualGroupAdminCommand = await _mediator.Send(new DeleteCostCenterVirtualGroupAdminCommand(request.CostCenterVirtualGroupAdminId), ct);
        if (deleteCostCenterVirtualGroupAdminCommand.IsFailure)
            return Result.Failure<DeleteCostCenterVirtualGroupAdminResponse>(deleteCostCenterVirtualGroupAdminCommand.Error!);
        var centerVirtualGroupAdmin = deleteCostCenterVirtualGroupAdminCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteCostCenterVirtualGroupAdminResponse(centerVirtualGroupAdmin.Id);
    }

    public async Task<Result<DeleteCostCenterVirtualGroupResponse?>> DeleteCostCenterVirtualGroupAsync(
        DeleteCostCenterVirtualGroupRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteCostCenterVirtualGroupValidator, DeleteCostCenterVirtualGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteCostCenterVirtualGroupResponse>(isValidRequest.Error!);

        var deleteCostCenterVirtualGroupCommand = await _mediator.Send(new DeleteCostCenterVirtualGroupCommand(request.CostCenterVirtualGroupId), ct);
        if (deleteCostCenterVirtualGroupCommand.IsFailure)
            return Result.Failure<DeleteCostCenterVirtualGroupResponse>(deleteCostCenterVirtualGroupCommand.Error!);
        var centerVirtualGroup = deleteCostCenterVirtualGroupCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new DeleteCostCenterVirtualGroupResponse(centerVirtualGroup.Id);
    }

    public async Task<Result<GetCostCenterVirtualGroupByCostCenterIdResponse?>> GetCostCenterVirtualGroupByCostCenterIdAsync(
        GetCostCenterVirtualGroupByCostCenterIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetCostCenterVirtualGroupByCostCenterIdValidator, GetCostCenterVirtualGroupByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterVirtualGroupByCostCenterIdResponse>(isValidRequest.Error!);

        var getCostCenterVirtualGroupByCostCenterIdQuery = await _mediator.Send(new GetCostCenterVirtualGroupByCostCenterIdQuery(request.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (getCostCenterVirtualGroupByCostCenterIdQuery.IsFailure)
            return Result.Failure<GetCostCenterVirtualGroupByCostCenterIdResponse>(getCostCenterVirtualGroupByCostCenterIdQuery.Error!);
        var costCenterVirtualGroups = getCostCenterVirtualGroupByCostCenterIdQuery.Value!.Data!;

        var groupData = new List<GetCostCenterVirtualGroupByCostCenterIdModel>();
        foreach (var costCenterVirtualGroup in costCenterVirtualGroups)
        {
            var detail = new GetCostCenterVirtualGroupByCostCenterIdModel()
            {
                CostCenterVirtualGroupId = costCenterVirtualGroup.Id,
                Description = costCenterVirtualGroup.Description,
                Identifier = costCenterVirtualGroup.Identifier,
                Link = costCenterVirtualGroup.Link,
                SendToday = costCenterVirtualGroup.SendToday,
                SendYesterday = costCenterVirtualGroup.SendYesterday,
                Title = costCenterVirtualGroup.Title,
                TodayTime = costCenterVirtualGroup.TodayTime,
                YesterdayTime = costCenterVirtualGroup.YesterdayTime
            };

            var admins = new List<GetCostCenterVirtualGroupAdminByCostCenterIdModel>();
            foreach (var virtualGroupAdmin in costCenterVirtualGroup.Admins)
            {
                var getWithSkillOnlyByIdsQuery = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, 1, new List<long>() { virtualGroupAdmin.ThirdPartyId }, null, true, null), ct);
                if (getWithSkillOnlyByIdsQuery.IsFailure)
                    return Result.Failure<GetCostCenterVirtualGroupByCostCenterIdResponse>(getWithSkillOnlyByIdsQuery.Error!);
                var thirdParty = getWithSkillOnlyByIdsQuery.Value!.Data!.FirstOrDefault()!;

                var admin = new GetCostCenterVirtualGroupAdminByCostCenterIdModel()
                {
                    CostCenterVirtualGroupAdminId = virtualGroupAdmin.Id,
                    ThirdPartyId = virtualGroupAdmin.ThirdPartyId,
                    UserName = virtualGroupAdmin.UserName,
                    ThirdPartyName = thirdParty.FullName,
                };

                admins.Add(admin);
            }

            detail.Admins = admins;
            groupData.Add(detail);
        }

        return new GetCostCenterVirtualGroupByCostCenterIdResponse(groupData, getCostCenterVirtualGroupByCostCenterIdQuery.Value!.RowCount!);

    }

    public async Task<Result<UpdateCostCenterVirtualGroupAdminResponse?>> UpdateCostCenterVirtualGroupAdminAsync(
        UpdateCostCenterVirtualGroupAdminRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateCostCenterVirtualGroupAdminValidator, UpdateCostCenterVirtualGroupAdminRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCostCenterVirtualGroupAdminResponse>(isValidRequest.Error!);

        var updateCostCenterVirtualGroupAdminCommand = await _mediator.Send(new UpdateCostCenterVirtualGroupAdminCommand(request.CostCenterVirtualGroupAdminId, request.ThirdPartyId, request.UserName), ct);
        if (updateCostCenterVirtualGroupAdminCommand.IsFailure)
            return Result.Failure<UpdateCostCenterVirtualGroupAdminResponse>(updateCostCenterVirtualGroupAdminCommand.Error!);
        var costCenterVirtualGroupAdmin = updateCostCenterVirtualGroupAdminCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateCostCenterVirtualGroupAdminResponse(costCenterVirtualGroupAdmin.Id);
    }

    public async Task<Result<UpdateCostCenterVirtualGroupResponse?>> UpdateCostCenterVirtualGroupAsync(
        UpdateCostCenterVirtualGroupRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateCostCenterVirtualGroupValidator, UpdateCostCenterVirtualGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCostCenterVirtualGroupResponse>(isValidRequest.Error!);

        var updateCostCenterVirtualGroupCommand = await _mediator.Send(new UpdateCostCenterVirtualGroupCommand(request.CostCenterVirtualGroupId, request.Title, request.Link, request.Identifier, request.Description, request.SendToday, request.TodayTime, request.SendYesterday, request.YesterdayTime), ct);
        if (updateCostCenterVirtualGroupCommand.IsFailure)
            return Result.Failure<UpdateCostCenterVirtualGroupResponse>(updateCostCenterVirtualGroupCommand.Error!);
        var costCenterVirtualGroup = updateCostCenterVirtualGroupCommand.Value!;

        await _unitOfWork.CommitAsync(ct);

        return new UpdateCostCenterVirtualGroupResponse(costCenterVirtualGroup.Id);
    }
}
