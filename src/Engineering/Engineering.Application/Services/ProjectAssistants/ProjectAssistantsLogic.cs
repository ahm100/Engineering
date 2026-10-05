using Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.CreateImplementationAssistans;
using Engineering.Application.Services.ProjectAssistants.Commands.ImplementationAssistans.DeleteImplementationAssistans;
using Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.CreateTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Commands.TechnicalAssistans.DeleteTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.CreateImplementationAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.DeletemplementationAssistants;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsGetsByProjectId;
using Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsModels;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.CreateTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.DeleteTechnicalAssistans;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansGetsByProjectId;
using Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansModels;
using Engineering.Application.Services.ProjectAssistants.Queries.ImplementationAssistans.ImplementationAssistansGetsByProjectId;
using Engineering.Application.Services.ProjectAssistants.Queries.TechnicalAssistans.TechnicalAssistansGetsByProjectId;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.Services.Projects.Queries.GetProjectById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;

namespace Engineering.Application.Services.ProjectAssistants;

public class ProjectAssistantsLogic : IProjectAssistantsLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectAssistantsLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectAssistantsLogic(
        IMediator mediator,
        ILogger<ProjectAssistantsLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateImplementationAssistansResponse?>> CreateImplementationAssistant(
        CreateImplementationAssistansRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateImplementationAssistans, ImplementationAssistantUserId:{ImplementationAssistantUserId}, ProjectId:{ProjectId},",
            request.ImplementationAssistantUserId, request.ProjectId);

        var isValidRequest = await request.IsValidAsync<CreateImplementationAssistansValidator, CreateImplementationAssistansRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateImplementationAssistansResponse>(isValidRequest.Error!);

        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (project.IsFailure)
            return Result.Failure<CreateImplementationAssistansResponse>(project.Error!);

        var response = await _mediator.Send(new CreateImplementationAssistansCommand(project.Value!, request.ImplementationAssistantUserId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateImplementationAssistansResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        var value = response.Value!;
        return new CreateImplementationAssistansResponse(value.Id, value.ImplementationAssistantUserId, project.Value!.Id);
    }

    public async Task<Result<DeleteImplementationAssistansResponse?>> DeleteImplementationAssistant(
        DeleteImplementationAssistansRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteImplementationAssistans, Id:{lementationAssistansId}", request.ImplementationAssistantUserId);

        var isValidRequest = await request.IsValidAsync<DeleteImplementationAssistansValidator, DeleteImplementationAssistansRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteImplementationAssistansResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteImplementationAssistansCommand(request.ImplementationAssistantUserId, request.ProjectId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteImplementationAssistansResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        var value = response.Value!;
        return new DeleteImplementationAssistansResponse(value.Id, value.ImplementationAssistantUserId);
    }

    public async Task<Result<ImplementationAssistansGetsByProjectIdResponse?>> ImplementationAssistantGetsByProjectId(
        ImplementationAssistansGetsByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for ImplementationAssistansGetsByProjectId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<ImplementationAssistansGetsByProjectIdValidator, ImplementationAssistansGetsByProjectIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<ImplementationAssistansGetsByProjectIdResponse>(isValidRequest.Error!);

        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (project.IsFailure)
            return Result.Failure<ImplementationAssistansGetsByProjectIdResponse>(project.Error!);

        var response = await _mediator.Send(new ImplementationAssistansGetsByProjectIdQuery(request.ProjectId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<ImplementationAssistansGetsByProjectIdResponse>(response.Error!);
        var value = response.Value!.Data;

        var allIds = new List<long>();
        if (value!.Any())
            allIds = value!.Select(x => x.ImplementationAssistantUserId).ToList();

        allIds = allIds.Where(x => x! != default).Distinct().ToList();
        var metaDataInfos = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, allIds.Count, allIds, null, true, null), ct); // بره سراغ متا دیتا
        if (metaDataInfos.IsFailure)
            return Result.Failure<ImplementationAssistansGetsByProjectIdResponse>(metaDataInfos.Error!);
        var usersInfos = metaDataInfos.Value!.Data!.Adapt<List<GetUserInfo>>(); // تبدیل به نوع مورد نیاز

        var dataResult = new List<ImplementationAssistansGetsByProjectIdModel>();
        foreach (var item in value!)
            dataResult.Add(new ImplementationAssistansGetsByProjectIdModel(item.Id, item.ImplementationAssistantUserId,
                usersInfos.Where(x => x.Id == item.ImplementationAssistantUserId).Select(x => x.FullName).FirstOrDefault()!));

        return new ImplementationAssistansGetsByProjectIdResponse(dataResult.Adapt<List<ImplementationAssistansGetsByProjectIdModel>>() ?? new List<ImplementationAssistansGetsByProjectIdModel>(0),
            response.Value?.RowCount ?? 0);
    }

    public async Task<Result<CreateTechnicalAssistansResponse?>> CreateTechnicalAssistant(
        CreateTechnicalAssistansRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateTechnicalAssistans, TechnicalAssistantUserId:{TechnicalAssistantUserId}, ProjectId:{ProjectId},",
            request.TechnicalAssistantUserId, request.ProjectId);

        var isValidRequest = await request.IsValidAsync<CreateTechnicalAssistansValidator, CreateTechnicalAssistansRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateTechnicalAssistansResponse>(isValidRequest.Error!);

        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (project.IsFailure)
            return Result.Failure<CreateTechnicalAssistansResponse>(project.Error!);

        var response = await _mediator.Send(new CreateTechnicalAssistansCommand(project.Value!, request.TechnicalAssistantUserId), ct);
        if (response.IsFailure)
            return Result.Failure<CreateTechnicalAssistansResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        var value = response.Value!;
        return new CreateTechnicalAssistansResponse(value.Id, value.TechnicalAssistantUserId, project.Value!.Id);
    }

    public async Task<Result<DeleteTechnicalAssistansResponse?>> DeleteTechnicalAssistant(
        DeleteTechnicalAssistansRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteTechnicalAssistans, Id:{TechnicalAssistansId}", request.TechnicalAssistansId);

        var isValidRequest = await request.IsValidAsync<DeleteTechnicalAssistansValidator, DeleteTechnicalAssistansRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteTechnicalAssistansResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteTechnicalAssistansCommand(request.TechnicalAssistansId, request.ProjectId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteTechnicalAssistansResponse>(response.Error!);

        var value = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new DeleteTechnicalAssistansResponse(value.Id, value.TechnicalAssistantUserId);
    }

    public async Task<Result<TechnicalAssistansGetsByProjectIdResponse?>> TechnicalAssistantGetsByProjectId(
        TechnicalAssistansGetsByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for TechnicalAssistansGetsByProjectId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<TechnicalAssistansGetsByProjectIdValidator, TechnicalAssistansGetsByProjectIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<TechnicalAssistansGetsByProjectIdResponse>(isValidRequest.Error!);

        var project = await _mediator.Send(new GetProjectByIdQuery(request.ProjectId), ct);
        if (project.IsFailure)
            return Result.Failure<TechnicalAssistansGetsByProjectIdResponse>(project.Error!);

        var response = await _mediator.Send(new TechnicalAssistansGetsByProjectIdQuery(request.ProjectId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<TechnicalAssistansGetsByProjectIdResponse>(response.Error!);
        var value = response.Value!.Data;

        var allIds = new List<long>();
        if (value!.Any())
            allIds = value!.Select(x => x.TechnicalAssistantUserId).ToList();

        allIds = allIds.Where(x => x! != default).ToList();
        var metaDataInfos = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, allIds.Count, allIds, null, true, null), ct); // بره سراغ متا دیتا
        if (metaDataInfos.IsFailure)
            return Result.Failure<TechnicalAssistansGetsByProjectIdResponse>(metaDataInfos.Error!);
        var usersInfos = metaDataInfos.Value!.Data!.Adapt<List<GetUserInfo>>(); // تبدیل به نوع مورد نیاز

        var dataResult = new List<TechnicalAssistansGetsByProjectIdModel>();
        foreach (var item in value!)
            dataResult.Add(new TechnicalAssistansGetsByProjectIdModel(item.Id, item.TechnicalAssistantUserId,
                usersInfos.Where(x => x.Id == item.TechnicalAssistantUserId).Select(x => x.FullName).FirstOrDefault()!));

        return new TechnicalAssistansGetsByProjectIdResponse(dataResult.Adapt<List<TechnicalAssistansGetsByProjectIdModel>>() ?? new List<TechnicalAssistansGetsByProjectIdModel>(0),
            response.Value?.RowCount ?? 0);
    }

}