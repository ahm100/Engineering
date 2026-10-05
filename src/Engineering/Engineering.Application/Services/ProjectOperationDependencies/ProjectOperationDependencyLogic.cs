using Engineering.Application.Services.ProjectOperationDependencies.Commands.CreateProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Commands.DeleteProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Commands.UpdateProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.Create;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.DeleteProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPODependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetPORequirements;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.UpdateProjectOperationDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.GetDependencyByPOId;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.GetFltrDependency;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPODependencies;
using Engineering.Application.Services.ProjectOperationDependencies.Queries.GetPORequirements;

namespace Engineering.Application.Services.ProjectOperationDependencies;

public class ProjectOperationDependencyLogic : IProjectOperationDependencyLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectOperationDependencyLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectOperationDependencyLogic(IMediator mediator,
        ILogger<ProjectOperationDependencyLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectOperationDependencyResponse?>> CreateProjectOperationDependency(
        CreateProjectOperationDependencyRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectOperationDependency");

        var response = await _mediator.Send(new CreateProjectOperationDependencyCommand(request.PredecessorProjectOperationId,
            request.SuccessorProjectOperationId,
            request.LagDays,
            request.DependencyType), ct);
        if (response.IsFailure)
            return Result.Failure<CreateProjectOperationDependencyResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationDependencyResponse(response.Value!.Id);
    }

    public async Task<Result<UpdateProjectOperationDependencyResponse?>> UpdateProjectOperationDependency(
        UpdateProjectOperationDependencyRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDependenc");

        var response = await _mediator.Send(new UpdateProjectOperationDependencyCommand(request.Id,
            request.Payload.PredecessorProjectOperationId,
            request.Payload.SuccessorProjectOperationId,
            request.Payload.LagDays,
            request.Payload.DependencyType), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateProjectOperationDependencyResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationDependencyResponse(response.Value!.Id);
    }

    public async Task<Result<DeleteProjectOperationDependencyResponse?>> DeleteProjectOperationDependency(
        DeleteProjectOperationDependencyRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectOperationDependenc");

        var response = await _mediator.Send(new DeleteProjectOperationDependencyCommand(request.Id), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteProjectOperationDependencyResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationDependencyResponse(response.Value!.Id);
    }

    public async Task<Result<GetDependencyByPOIdResponse?>> GetDependencyByPOId(
        GetDependencyByPOIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetDependencyByPOId");
        var result = await _mediator.Send(new GetDependencyByPOIdQuery(request.ProjectOperationId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetDependencyByPOIdResponse?>();

        return result;
    }

    public async Task<Result<GetFltrDependencyResponse?>> GetFltrDependency(
        GetFltrDependencyRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrDependency");
        var result = await _mediator.Send(new GetFltrDependencyQuery(request.ProjectOperationIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetFltrDependencyResponse?>();

        return result;
    }

    public async Task<Result<GetPORequirementsResponse?>> GetPORequirements(
        GetPORequirementsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrDependency");
        var result = await _mediator.Send(new GetPORequirementsQuery(request.ProjectOperationId), ct);
        if (result.IsBad())
            return result.Failure<GetPORequirementsResponse?>();

        return result;
    }

    public async Task<Result<GetPODependenciesResponse?>> GetPODependencies(
        GetPODependenciesRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPODependencies");
        var result = await _mediator.Send(new GetPODependenciesQuery(request.ProjectOperationId), ct);
        if (result.IsBad())
            return result.Failure<GetPODependenciesResponse?>();

        return result;
    }
}