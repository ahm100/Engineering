using Engineering.Application.Extensions.Pagination;
using Engineering.Application.Services.PublicGroups.Commands.CreatePublicGroup;
using Engineering.Application.Services.PublicGroups.Commands.DeletePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.CreatePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.DisablePublicGroup;
using Engineering.Application.Services.PublicGroups.Models.GetById;
using Engineering.Application.Services.PublicGroups.Models.GetFilteredPublicGroups;
using Engineering.Application.Services.PublicGroups.Models.PublicGroupGroupDelete;
using Engineering.Application.Services.PublicGroups.Queries.GetById;
using Engineering.Application.Services.PublicGroups.Queries.GetFilteredPublicGroups;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;

namespace Engineering.Application.Services.PublicGroups;

public class PublicGroupLogic : IPublicGroupLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<PublicGroupLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public PublicGroupLogic(IMediator mediator, ILogger<PublicGroupLogic> logger, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreatePublicGroupResponse?>> CreatePublicGroup(CreatePublicGroupRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreatePublicGroup");

        var isValidRequest = await request.IsValidAsync<CreatePublicGroupValidator, CreatePublicGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreatePublicGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFilteredPublicGroupsQuery(request.ProductGroupIds), ct);
        if (response is { IsSuccess: true, Value.Data: not null })
            return Result.Failure<CreatePublicGroupResponse>(OperationInfoErrors.PublicGroupIsDuplicate);

        var productGroupIds = request.ProductGroupIds.Where(x => x > 0).Distinct().ToList();
        var productGroupQuery = await _mediator.Send(new GetGroupsByIdsQuery(productGroupIds, 1, productGroupIds.Count), ct);
        if (productGroupQuery.IsFailure || productGroupQuery.Value is null || productGroupQuery.Value.Data is null)
            return Result.Failure<CreatePublicGroupResponse>(productGroupQuery.Error!);
        var productGroups = productGroupQuery.Value.Data;

        var groupIds = productGroups.Where(x => x.Id > 0).Select(x => x.Id).Distinct().ToList();

        var createCommand = await _mediator.Send(new CreatePublicGroupCommand(groupIds), ct);
        if (createCommand.IsFailure)
            return Result.Failure<CreatePublicGroupResponse>(createCommand.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreatePublicGroupResponse(true);
    }

    public async Task<Result<DisablePublicGroupResponse?>> DisablePublicGroup(DisablePublicGroupRequest request, CT ct)
    {
        _logger.LogInformation("Disable PublicGroup, Id:{Id}", request.Id);

        var isValidRequest = await request.IsValidAsync<DisablePublicGroupValidator, DisablePublicGroupRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DisablePublicGroupResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeletePublicGroupCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DisablePublicGroupResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DisablePublicGroupResponse(response.Value!.Id, response.Value!.IsDeleted);
    }

    public async Task<Result<PublicGroupGroupDeleteResponse?>> PublicGroupGroupDelete(PublicGroupGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for PublicGroupGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<PublicGroupGroupDeleteValidator, PublicGroupGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<PublicGroupGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeletePublicGroupCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<PublicGroupGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new PublicGroupGroupDeleteResponse(true);
    }

    public async Task<Result<GetPublicGroupByIdResponse?>> GetPublicGroupById(GetPublicGroupByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPublicGroupById");

        var isValidRequest = await request.IsValidAsync<GetPublicGroupByIdValidator, GetPublicGroupByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetPublicGroupByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetPublicGroupByIdQuery(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<GetPublicGroupByIdResponse>(response.Error!);
        var value = response.Value;

        List<long>? ids = [];
        if (value?.ProductGroupId > 0)
            ids.Add(value?.ProductGroupId ?? 0);
        var groups = await WebServicesLogic.GetFilteredGroupsDataReceiver(ids, null, _mediator, ct);

        var group = groups?.FirstOrDefault(x => x.Id == value?.ProductGroupId);
        return new GetPublicGroupByIdResponse(value!.Id, value.ProductGroupId, group?.Name, group?.Code, group?.MeasureUnitName);
    }

    public async Task<Result<GetFilteredPublicGroupsResponse?>> GetFilteredPublicGroups(GetFilteredPublicGroupsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFilteredPublicGroups");

        var isValidRequest = await request.IsValidAsync<GetFilteredPublicGroupsValidator, GetFilteredPublicGroupsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredPublicGroupsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetFilteredPublicGroupsQuery(request.ProductGroupIds), ct);
        if (response.IsFailure)
            return Result.Failure<GetFilteredPublicGroupsResponse>(response.Error!);
        var values = response.Value?.Data;

        var groupIds = values?.Where(x => x.ProductGroupId > 0).Select(x => x.ProductGroupId).Distinct().ToList();

        List<FilteredGroup>? filteredGroups = [];
        var data = new List<GetFilteredPublicGroupsModel>();
        if (values is not null && values.Count > 0)
        {
            var groups = await WebServicesLogic.GetFilteredGroupsDataReceiver(groupIds, request.FilterData, _mediator, ct);
            if (groups is not null && groups.Count > 0)
                foreach (var item in groups)
                    filteredGroups.Add(item);

            if (!string.IsNullOrEmpty(request.FilterData))
            {
                foreach (var item in values)
                {
                    if (!filteredGroups.Any(x => x?.Id == item.ProductGroupId))
                        continue;

                    var filteredGroup = filteredGroups.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();
                    if (filteredGroup != null)
                        data.Add(new GetFilteredPublicGroupsModel(item.Id, item.ProductGroupId, filteredGroup?.Name, filteredGroup?.Code, filteredGroup?.MeasureUnitName));
                }
            }
            else
            {
                foreach (var item in values)
                {
                    if (!filteredGroups.Any(x => x?.Id == item.ProductGroupId))
                        continue;

                    var filteredGroup = filteredGroups.Where(x => x?.Id == item.ProductGroupId).FirstOrDefault();
                    if (filteredGroup != null)
                        data.Add(new GetFilteredPublicGroupsModel(item.Id, item.ProductGroupId, filteredGroup?.Name, filteredGroup?.Code, filteredGroup?.MeasureUnitName));
                }
            }
        }
        var responseData = data.SetPaging(request.PageIndex - 1, request.PageSize);
        return new GetFilteredPublicGroupsResponse(responseData ?? new List<GetFilteredPublicGroupsModel>(0), filteredGroups?.Count ?? 0);
    }
}