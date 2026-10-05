using Engineering.Application.Services.ProjectRisks.Commands.CreateProjectRisk;
using Engineering.Application.Services.ProjectRisks.Commands.DeleteProjectRisk;
using Engineering.Application.Services.ProjectRisks.Commands.UpdateProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.CreateProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.DeleteProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetFltrProjectRisk;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskById;
using Engineering.Application.Services.ProjectRisks.Contracts.GetProjectRiskByProjectId;
using Engineering.Application.Services.ProjectRisks.Contracts.UpdateProjectRisk;
using Engineering.Application.Services.ProjectRisks.Queries.GetFltrProjectRisk;
using Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskById;
using Engineering.Application.Services.ProjectRisks.Queries.GetProjectRiskByProjectId;

namespace Engineering.Application.Services.ProjectRisks;

public class ProjectRiskLogic : IProjectRiskLogic
{
    private IMediator _mediator;
    private ILogger<ProjectRiskLogic> _logger;
    private IUnitOfWork _unitOfWork;

    public ProjectRiskLogic(
        IMediator mediator,
        ILogger<ProjectRiskLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectRiskResponse?>> CreateProjectRisk(
        CreateProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectRisk, ProjectRisk:{ProjectRisk},", request.Title);
        var create = await _mediator.Send(new CreateProjectRiskCommand(request.Code,
            request.Title,
            request.ProjectId,
            request.RiskProbability,
            request.RiskImpact,
            request.RiskStatus,
            request.IsActive), ct);
        if (create.IsBad())
            return create.Failure<CreateProjectRiskResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectRiskResponse(create.Value!.Id);
    }

    public async Task<Result<UpdateProjectRiskResponse?>> UpdateProjectRisk(
        UpdateProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectRisk, ProjectRisk:{ProjectRisk},", request.Title);
        var update = await _mediator.Send(new UpdateProjectRiskCommand(request.Id,
            request.Code,
            request.Title,
            request.ProjectId,
            request.RiskProbability,
            request.RiskImpact,
            request.RiskStatus,
            request.IsActive), ct);
        if (update.IsBad())
            return update.Failure<UpdateProjectRiskResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectRiskResponse(update.Value!.Id, true);
    }

    public async Task<Result<DeleteProjectRiskResponse?>> DeleteProjectRisk(
        DeleteProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectRisk, ProjectRiskId:{ProjectRiskId},", request.Id);
        var delete = await _mediator.Send(new DeleteProjectRiskCommand(request.Id), ct);
        if (delete.IsBad())
            return delete.Failure<DeleteProjectRiskResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectRiskResponse(true);
    }

    public async Task<Result<GetFltrProjectRiskResponse?>> GetFltrProjectRisk(
        GetFltrProjectRiskRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrProjectRisk");
        var result = await _mediator.Send(new GetFltrProjectRiskQuery(request.ProjectIds,
            request.FilterData,
            request.RiskProbability,
            request.RiskImpact,
            request.RiskStatus,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetFltrProjectRiskResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectRiskByIdResponse?>> GetProjectRiskById(
        GetProjectRiskByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrProjectRisk");
        var result = await _mediator.Send(new GetProjectRiskByIdQuery(request.Id), ct);
        if (result.IsBad())
            return result.Failure<GetProjectRiskByIdResponse>()!;

        return result;
    }

    public async Task<Result<GetProjectRiskByProjectIdResponse?>> GetProjectRiskByProjectId(
        GetProjectRiskByProjectIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectRiskByProjectId");
        var result = await _mediator.Send(new GetProjectRiskByProjectIdQuery(request.ProjectId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetProjectRiskByProjectIdResponse>()!;

        return result;
    }
}
