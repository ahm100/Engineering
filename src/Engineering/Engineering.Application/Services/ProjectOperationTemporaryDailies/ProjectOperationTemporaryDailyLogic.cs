using Engineering.Application.Services.CostCenters.Queries.GetCostCenterByIdIncludeless;
using Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationByIdNoIncluding;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.ChangeProjectOperationTemporaryDailyStatus;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.CreateProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.DeleteProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.UpdateProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.CreateProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.DeleteProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetCurrentUserTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetFilteredProjectOperationTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyById;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyStatus;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GroupProjectOperationTemporaryDailyStatusChanger;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyDocumentModel;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyGroupDelete;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyStatusChanger;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.UpdateProjectOperationTemporaryDaily;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetCurrentUserTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetFilteredProjectOperationTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetProjectOperationTemporaryDailyById;
using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies;

public class ProjectOperationTemporaryDailyLogic : IProjectOperationTemporaryDailyLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationTemporaryDailyLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserProfileService _userProfileService;
    private readonly IUserInfoService _userInfoService;

    public ProjectOperationTemporaryDailyLogic(IMediator mediator, ILogger<ProjectOperationTemporaryDailyLogic> logger, IUnitOfWork unitOfWork, IUserProfileService userProfileService, IUserInfoService userInfoService)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userProfileService = userProfileService;
        _userInfoService = userInfoService;
    }

    public async Task<Result<CreateProjectOperationTemporaryDailyResponse?>> CreateProjectOperationTemporaryDaily(CreateProjectOperationTemporaryDailyRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<CreateProjectOperationTemporaryDailyValidator, CreateProjectOperationTemporaryDailyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateProjectOperationTemporaryDailyResponse>(isValidRequest.Error!);

        var getCostCenter = await _mediator.Send(new GetCostCenterByIdIncludelessQuery(request.CostCenterId), ct);
        if (getCostCenter.IsFailure)
            return Result.Failure<CreateProjectOperationTemporaryDailyResponse>(getCostCenter.Error!);
        var costCenter = getCostCenter.Value!;

        var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.projectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<CreateProjectOperationTemporaryDailyResponse>(getProject.Error!);
        var project = getProject.Value!;

        ProjectOperation? projectOperation = null;
        if (request.ProjectOperationId != null && request.ProjectOperationId > 0)
        {
            var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.ProjectOperationId.Value), ct);
            if (getProjectOperation.IsFailure)
                return Result.Failure<CreateProjectOperationTemporaryDailyResponse>(getProjectOperation.Error!);
            projectOperation = getProjectOperation.Value!;
        }

        if (request.EndDate.Date < request.StartDate.Date)
            return Result.Failure<CreateProjectOperationTemporaryDailyResponse>(ProjectOperationTemporaryDailyErrors.UnValidDate);

        var createResponse = await _mediator.Send(new CreateProjectOperationTemporaryDailyCommand(request.StartDate, request.EndDate,
            request.Status == null ? TemporaryDailyStatus.NotStarted : request.Status, request.Description, costCenter, project,
            projectOperation, request.Documents), ct);
        if (createResponse.IsFailure)
            return Result.Failure<CreateProjectOperationTemporaryDailyResponse>(createResponse.Error!);
        var projectOperationTemporaryDaily = createResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationTemporaryDailyResponse(projectOperationTemporaryDaily.Id);
    }

    public async Task<Result<UpdateProjectOperationTemporaryDailyResponse?>> UpdateProjectOperationTemporaryDaily(UpdateProjectOperationTemporaryDailyRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<UpdateProjectOperationTemporaryDailyValidator, UpdateProjectOperationTemporaryDailyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateProjectOperationTemporaryDailyResponse>(isValidRequest.Error!);

        var getCostCenter = await _mediator.Send(new GetCostCenterByIdIncludelessQuery(request.CostCenterId), ct);
        if (getCostCenter.IsFailure)
            return Result.Failure<UpdateProjectOperationTemporaryDailyResponse>(getCostCenter.Error!);
        var costCenter = getCostCenter.Value!;

        var getProject = await _mediator.Send(new GetProjectByIdIncludelessQuery(request.projectId), ct);
        if (getProject.IsFailure)
            return Result.Failure<UpdateProjectOperationTemporaryDailyResponse>(getProject.Error!);
        var project = getProject.Value!;

        ProjectOperation? projectOperation = null;
        if (request.ProjectOperationId != null && request.ProjectOperationId > 0)
        {
            var getProjectOperation = await _mediator.Send(new GetProjectOperationByIdNoIncludingQuery(request.ProjectOperationId.Value), ct);
            if (getProjectOperation.IsFailure)
                return Result.Failure<UpdateProjectOperationTemporaryDailyResponse>(getProjectOperation.Error!);
            projectOperation = getProjectOperation.Value!;
        }

        if (request.EndDate.Date < request.StartDate.Date)
            return Result.Failure<UpdateProjectOperationTemporaryDailyResponse>(ProjectOperationTemporaryDailyErrors.UnValidDate);

        var updateResponse = await _mediator.Send(new UpdateProjectOperationTemporaryDailyCommand(request.Id, request.StartDate,
            request.EndDate, request.Status, request.Description, costCenter, project, projectOperation, request.DocumentUrls),
            ct);
        if (updateResponse.IsFailure)
            return Result.Failure<UpdateProjectOperationTemporaryDailyResponse>(updateResponse.Error!);
        var response = updateResponse.Value!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationTemporaryDailyResponse(response.Id);
    }

    public async Task<Result<GroupProjectOperationTemporaryDailyStatusChangerResponse?>> GroupProjectOperationTemporaryDailyStatusChanger(GroupProjectOperationTemporaryDailyStatusChangerRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GroupProjectOperationTemporaryDailyStatusChangerValidator, GroupProjectOperationTemporaryDailyStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GroupProjectOperationTemporaryDailyStatusChangerResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var getProjectOperationTemporaryDaily = await _mediator.Send(new GetProjectOperationTemporaryDailyByIdQuery(item), ct);
            if (getProjectOperationTemporaryDaily.IsFailure)
                return Result.Failure<GroupProjectOperationTemporaryDailyStatusChangerResponse>(getProjectOperationTemporaryDaily.Error!);
            var projectOperationTemporaryDaily = getProjectOperationTemporaryDaily.Value!;

            var response = await _mediator.Send(new ChangeProjectOperationTemporaryDailyStatusCommand(projectOperationTemporaryDaily, request.Status), ct);
            if (response.IsFailure)
                return Result.Failure<GroupProjectOperationTemporaryDailyStatusChangerResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new GroupProjectOperationTemporaryDailyStatusChangerResponse(true);
    }

    public async Task<Result<ProjectOperationTemporaryDailyStatusChangerResponse?>> ProjectOperationTemporaryDailyStatusChanger(ProjectOperationTemporaryDailyStatusChangerRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<ProjectOperationTemporaryDailyStatusChangerValidator, ProjectOperationTemporaryDailyStatusChangerRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationTemporaryDailyStatusChangerResponse>(isValidRequest.Error!);

        var getProjectOperationTemporaryDaily = await _mediator.Send(new GetProjectOperationTemporaryDailyByIdQuery(request.Id), ct);
        if (getProjectOperationTemporaryDaily.IsFailure)
            return Result.Failure<ProjectOperationTemporaryDailyStatusChangerResponse>(getProjectOperationTemporaryDaily.Error!);
        var projectOperationTemporaryDaily = getProjectOperationTemporaryDaily.Value!;

        var response = await _mediator.Send(new ChangeProjectOperationTemporaryDailyStatusCommand(projectOperationTemporaryDaily, request.Status), ct);
        if (response.IsFailure)
            return Result.Failure<ProjectOperationTemporaryDailyStatusChangerResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationTemporaryDailyStatusChangerResponse(request.Id);
    }

    public async Task<Result<DeleteProjectOperationTemporaryDailyResponse?>> DeleteProjectOperationTemporaryDaily(DeleteProjectOperationTemporaryDailyRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<DeleteProjectOperationTemporaryDailyValidator, DeleteProjectOperationTemporaryDailyRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteProjectOperationTemporaryDailyResponse>(isValidRequest.Error!);

        var deleteResponse = await _mediator.Send(new DeleteProjectOperationTemporaryDailyCommand(request.Id), ct);
        if (deleteResponse.IsFailure)
            return Result.Failure<DeleteProjectOperationTemporaryDailyResponse>(deleteResponse.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationTemporaryDailyResponse(request.Id);
    }

    public async Task<Result<ProjectOperationTemporaryDailyGroupDeleteResponse?>> ProjectOperationTemporaryDailyGroupDelete(ProjectOperationTemporaryDailyGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for ProjectOperationTemporaryDailyGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<ProjectOperationTemporaryDailyGroupDeleteValidator, ProjectOperationTemporaryDailyGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ProjectOperationTemporaryDailyGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var deleteResponse = await _mediator.Send(new DeleteProjectOperationTemporaryDailyCommand(item), ct);
            if (deleteResponse.IsFailure)
                return Result.Failure<ProjectOperationTemporaryDailyGroupDeleteResponse>(deleteResponse.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new ProjectOperationTemporaryDailyGroupDeleteResponse(true);
    }

    public async Task<Result<GetFilteredProjectOperationTemporaryDailiesResponse?>> GetFilteredProjectOperationTemporaryDailies(GetFilteredProjectOperationTemporaryDailiesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetFilteredProjectOperationTemporaryDailiesValidator, GetFilteredProjectOperationTemporaryDailiesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetFilteredProjectOperationTemporaryDailiesResponse>(isValidRequest.Error!);

        var getFiltered = await _mediator.Send(new GetFilteredProjectOperationTemporaryDailiesQuery(request.CostCenterId, request.ProjectId, request.ProjectOperationId,
              request.Status, request.StartDate, request.EndDate, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetFilteredProjectOperationTemporaryDailiesResponse>(getFiltered.Error!);
        var temporaryDailies = getFiltered.Value!.Data!;

        var creatorIds = temporaryDailies.Select(x => x.CreatorId).Where(x => x > 0).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var data = temporaryDailies.Adapt<List<GetFilteredProjectOperationTemporaryDailiesModel>>();
        foreach (var item in data)
        {
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            var values = temporaryDailies.FirstOrDefault(x => x.Id == item.Id)?.ProjectOperationTemporaryDailyDocuments.ToList();
            if (values != null && values.Count > 0)
                item.Documents = values.Adapt<List<ProjectOperationTemporaryDailyDocumentResponseModel>>();
        }

        return new GetFilteredProjectOperationTemporaryDailiesResponse(data ?? new List<GetFilteredProjectOperationTemporaryDailiesModel>(0),
            getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetCurrentUserTemporaryDailiesResponse?>> GetCurrentUserTemporaryDailies(GetCurrentUserTemporaryDailiesRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetCurrentUserTemporaryDailiesValidator, GetCurrentUserTemporaryDailiesRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCurrentUserTemporaryDailiesResponse>(isValidRequest.Error!);

        var curretUserId = _userProfileService.GetProfileInfo().UserId;
        if (curretUserId < 1)
            return Result.Failure<GetCurrentUserTemporaryDailiesResponse>(ProjectOperationTemporaryDailyErrors.UnValidRequester);

        var getFiltered = await _mediator.Send(new GetCurrentUserTemporaryDailiesQuery(curretUserId, request.CostCenterId, request.ProjectId, request.ProjectOperationId,
              request.Status, request.StartDate, request.EndDate, request.FilterData, request.OrderBy, request.PageIndex, request.PageSize), ct);
        if (getFiltered.IsFailure)
            return Result.Failure<GetCurrentUserTemporaryDailiesResponse>(getFiltered.Error!);
        var temporaryDailies = getFiltered.Value!.Data!;

        var creatorIds = temporaryDailies.Select(x => x.CreatorId).Where(x => x > 0).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var data = temporaryDailies.Adapt<List<GetCurrentUserTemporaryDailiesModel>>();
        foreach (var item in data)
        {
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            var values = temporaryDailies.FirstOrDefault(x => x.Id == item.Id)?.ProjectOperationTemporaryDailyDocuments.ToList();
            if (values != null && values.Count > 0)
                item.Documents = values.Adapt<List<ProjectOperationTemporaryDailyDocumentResponseModel>>();
        }

        return new GetCurrentUserTemporaryDailiesResponse(data ?? new List<GetCurrentUserTemporaryDailiesModel>(0),
            getFiltered.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetProjectOperationTemporaryDailyByIdResponse?>> GetProjectOperationTemporaryDailyById(GetProjectOperationTemporaryDailyByIdRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetProjectOperationTemporaryDailyByIdValidator, GetProjectOperationTemporaryDailyByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetProjectOperationTemporaryDailyByIdResponse>(isValidRequest.Error!);

        var getProjectOperationTemporaryDaily = await _mediator.Send(new GetProjectOperationTemporaryDailyByIdQuery(request.Id), ct);
        if (getProjectOperationTemporaryDaily.IsFailure)
            return Result.Failure<GetProjectOperationTemporaryDailyByIdResponse>(getProjectOperationTemporaryDaily.Error!);
        var temporaryDaily = getProjectOperationTemporaryDaily.Value!;

        var creatorId = temporaryDaily.CreatorId;
        var creators = await WebServicesLogic.UserDataReceiver([creatorId], null, _mediator, ct);

        var response = temporaryDaily.Adapt<GetProjectOperationTemporaryDailyByIdResponse>();
        response.Creator = creators?.FirstOrDefault()?.FullName;
        if (temporaryDaily.ProjectOperationTemporaryDailyDocuments is not null &&
            temporaryDaily.ProjectOperationTemporaryDailyDocuments.Count > 0)
            response.Documents = temporaryDaily.ProjectOperationTemporaryDailyDocuments.Adapt<List<ProjectOperationTemporaryDailyDocumentResponseModel>>();

        return response;
    }

    public async Task<Result<GetProjectOperationTemporaryDailyStatusResponse?>> GetProjectOperationTemporaryDailyStatus(GetProjectOperationTemporaryDailyStatusRequest request, CT ct)
    {
        var response = await Task.Run(() => EnumExt.GetEnumObjectList<TemporaryDailyStatus>());
        return new GetProjectOperationTemporaryDailyStatusResponse(response);
    }
}
