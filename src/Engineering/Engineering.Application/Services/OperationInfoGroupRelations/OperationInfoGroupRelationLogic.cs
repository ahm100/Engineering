using Engineering.Application.Services.OperationInfoGroupRelations.Commands.CreateOperationInfoGroupRelation;
using Engineering.Application.Services.OperationInfoGroupRelations.Commands.DeleteOperationInfoGroupRelation;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.CreateOperationInfoGroupRelation;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoGroupId;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.OperationInfoGroupRelationModels;
using Engineering.Application.Services.OperationInfoGroupRelations.Queries.GetsByOperationInfoGroupId;
using Engineering.Application.Services.OperationInfoGroupRelations.Queries.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfoGroups.Queries.GetsOperationInfoGroupByIds;
using Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoByIdByGroupRelations;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoGroupRelations;

public class OperationInfoGroupRelationLogic : IOperationInfoGroupRelationLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<OperationInfoGroupRelationLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OperationInfoGroupRelationLogic(
        IMediator mediator,
        ILogger<OperationInfoGroupRelationLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateOperationInfoGroupRelationResponse?>> CreateOperationInfoGroupRelation(
        CreateOperationInfoGroupRelationRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateOperationInfoGroupRelation, OperationInfoId:{OperationInfoId}, SeasonsId:{SeasonsId},", request.OperationInfoId, request.OperationInfoGroupIds);

        var isValidRequest = await request.IsValidAsync<CreateOperationInfoGroupRelationValidator, CreateOperationInfoGroupRelationRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateOperationInfoGroupRelationResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoByIdByGroupRelationsQuery(request.OperationInfoId), ct);
        if (operationInfo.IsFailure)
            return Result.Failure<CreateOperationInfoGroupRelationResponse>(operationInfo.Error!);
        if (operationInfo.Value is null)
            return Result.Failure<CreateOperationInfoGroupRelationResponse>(operationInfo.Error!);

        if (request.OperationInfoGroupIds is null || request.OperationInfoGroupIds?.Count == 0)
        {
            if (operationInfo.Value.OperationInfoGroupRelations.Count > 0)
            {
                foreach (var relation in operationInfo.Value.OperationInfoGroupRelations)
                {
                    var deleteRelation = await _mediator.Send(new DeleteOperationInfoGroupRelationCommand(relation.OperationInfoGroup.Id, operationInfo.Value.Id), ct); // بره سراغ متا دیتا
                    if (deleteRelation.IsFailure)
                        return Result.Failure<CreateOperationInfoGroupRelationResponse>(deleteRelation.Error!);
                }

                await _unitOfWork.CommitAsync(ct);
                return new CreateOperationInfoGroupRelationResponse(request.OperationInfoId, true);
            }
            else
                return Result.Failure<CreateOperationInfoGroupRelationResponse>(OperationInfoGroupRelationErrors.NoHaveGroup);
        }
        else
        {
            if (operationInfo.Value.OperationInfoGroupRelations.Count > 0)
            {
                foreach (var relation in operationInfo.Value.OperationInfoGroupRelations)
                {
                    var deleteRelation = await _mediator.Send(new DeleteOperationInfoGroupRelationCommand(relation.OperationInfoGroup.Id, operationInfo.Value.Id), ct); // بره سراغ متا دیتا
                    if (deleteRelation.IsFailure)
                        return Result.Failure<CreateOperationInfoGroupRelationResponse>(deleteRelation.Error!);
                }
            }

            var groupIds = new List<long>();
            foreach (var id in request.OperationInfoGroupIds!)
            {
                if (groupIds.Any(x => x == id)) continue;
                else if (id == default) continue;
                else groupIds.Add(id);
            }

            if (groupIds?.Count == 0)
                return Result.Failure<CreateOperationInfoGroupRelationResponse>(OperationInfoGroupRelationErrors.OperationInfoGroupRelationIsNotOk);

            var groupsData = await _mediator.Send(new GetsOperationInfoGroupByIdsQuery(groupIds!, 1, groupIds!.Count), ct); // بره سراغ متا دیتا
            if (groupsData.IsFailure)
                return Result.Failure<CreateOperationInfoGroupRelationResponse>(OperationInfoGroupRelationErrors.UnValidGroups);

            var response = await _mediator.Send(new CreateOperationInfoGroupRelationCommand(operationInfo.Value, groupsData.Value!.Data!), ct);
            if (response.IsFailure)
                return Result.Failure<CreateOperationInfoGroupRelationResponse>(response.Error!);

            await _unitOfWork.CommitAsync(ct);
            return new CreateOperationInfoGroupRelationResponse(request.OperationInfoId, true);
        }
    }

    public async Task<Result<GetsOperationInfoGroupRelationByGroupIdResponse?>> GetsByOperationInfoGroupId(
        GetsOperationInfoGroupRelationByGroupIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByOperationInfoGroupId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoGroupRelationByGroupIdValidator, GetsOperationInfoGroupRelationByGroupIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoGroupRelationByGroupIdResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoGroupByIdQuery(request.OprationInfoGroupId), ct);
        if (operationInfo.IsFailure)
            return Result.Failure<GetsOperationInfoGroupRelationByGroupIdResponse>(operationInfo.Error!);
        if (operationInfo.Value!.OperationInfoGroupRelations is null || operationInfo.Value!.OperationInfoGroupRelations.Count <= 0)
            return Result.Failure<GetsOperationInfoGroupRelationByGroupIdResponse>(OperationInfoGroupRelationErrors.OperationInfoGroupRelationWithIdNotFound);

        var response = await _mediator.Send(new GetsByOperationInfoGroupIdQuery(request.OprationInfoGroupId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoGroupRelationByGroupIdResponse>(response.Error!);
        var values = response.Value!.Data;

        var data = GetsByGroupIdModeling(values);

        return new GetsOperationInfoGroupRelationByGroupIdResponse(data?.OperationInfoData, data?.OperationInfoGroupData);
    }

    public async Task<Result<GetsOperationInfoGroupRelationByIdResponse?>> GetsByOperationInfoId(
        GetsOperationInfoGroupRelationByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsByOperationInfoId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsOperationInfoGroupRelationByIdValidator, GetsOperationInfoGroupRelationByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsOperationInfoGroupRelationByIdResponse>(isValidRequest.Error!);

        var operationInfo = await _mediator.Send(new GetOperationInfoByIdByGroupRelationsQuery(request.OprationInfoId), ct);
        if (operationInfo.IsFailure)
            return Result.Failure<GetsOperationInfoGroupRelationByIdResponse>(operationInfo.Error!);
        if (operationInfo.Value!.OperationInfoGroupRelations is null || operationInfo.Value!.OperationInfoGroupRelations.Count <= 0)
            return Result.Failure<GetsOperationInfoGroupRelationByIdResponse>(OperationInfoGroupRelationErrors.OperationInfoGroupRelationWithIdNotFound);

        var response = await _mediator.Send(new GetsByOperationInfoIdQuery(request.OprationInfoId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsOperationInfoGroupRelationByIdResponse>(response.Error!);
        var values = response.Value!.Data;

        var data = GetsByGroupIdModeling(values);

        return new GetsOperationInfoGroupRelationByIdResponse(data?.OperationInfoData, data?.OperationInfoGroupData);
    }

    private GetsOperationInfoGroupRelationResponseModel? GetsByGroupIdModeling(
        List<OperationInfoGroupRelation>? items)
    {
        List<GroupRelationsOperationInfoModel>? operationInfos = [];
        List<GroupRelationsOperationInfoGroupModel>? operationInfoGroups = [];

        if (items is not null && items.Count > 0)
            foreach (var item in items)
            {
                operationInfos.Add(new GroupRelationsOperationInfoModel(item.OperationInfo.Id, item.OperationInfo.OperationInfoName, item.OperationInfo.OperationInfoCode));
                operationInfoGroups.Add(new GroupRelationsOperationInfoGroupModel(item.OperationInfoGroup.Id, item.OperationInfoGroup.OperationInfoGroupTitle, item.OperationInfoGroup.OperationInfoGroupCode));
            }

        return new GetsOperationInfoGroupRelationResponseModel(operationInfos, operationInfoGroups);
    }
}