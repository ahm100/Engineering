using Engineering.Application.Services.ProjectOperationWbses.Commands.CreateProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Commands.DeleteProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Commands.UpdateProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.CreateProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.DeleteProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetDetailPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;
using Engineering.Application.Services.ProjectOperationWbses.Contracts.UpdateProjectOperationWbs;
using Engineering.Application.Services.ProjectOperationWbses.Queries.GetDetailPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Queries.GetFltrPOWbs;
using Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByPOId;
using Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByProjectWbsId;
using Engineering.Application.Services.ProjectOperationWbses.Queries.GetProjectOperationWbsById;

namespace Engineering.Application.Services.ProjectOperationWbses;

public class ProjectOperationWbsLogic : IProjectOperationWbsLogic
{
    private IMediator _mediator;
    private ILogger<ProjectOperationWbsLogic> _logger;
    private IUnitOfWork _unitOfWork;

    public ProjectOperationWbsLogic(
        IMediator mediator,
        ILogger<ProjectOperationWbsLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProjectOperationWbsResponse?>> CreateProjectOperationWbs(
        CreateProjectOperationWbsRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateProjectOperationWbs");
        var create = await _mediator.Send(new CreateProjectOperationWbsCommand(request.ProjectWbsId,
            request.ProjectOperationIds,
            request.IsActive), ct);
        if (create.IsBad())
            return create.Failure<CreateProjectOperationWbsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectOperationWbsResponse(true);
    }

    public async Task<Result<UpdateProjectOperationWbsResponse?>> UpdateProjectOperationWbs(
        UpdateProjectOperationWbsRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateProjectRisk");
        var update = await _mediator.Send(new UpdateProjectOperationWbsCommand(request.Id,
            request.Payload.ProjectWbsId,
            request.Payload.ProjectOperationId,
            request.Payload.IsActive), ct);
        if (update.IsBad())
            return update.Failure<UpdateProjectOperationWbsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectOperationWbsResponse(true);
    }

    public async Task<Result<DeleteProjectOperationWbsResponse?>> DeleteProjectOperationWbs(
        DeleteProjectOperationWbsRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeleteProjectOperationWbs");
        var update = await _mediator.Send(new DeleteProjectOperationWbsCommand(request.Id), ct);
        if (update.IsBad())
            return update.Failure<DeleteProjectOperationWbsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectOperationWbsResponse(true);
    }

    public async Task<Result<GetProjectOperationWbsByIdResponse?>> GetProjectOperationWbsById(
        GetProjectOperationWbsByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetProjectOperationWbsById");
        var result = await _mediator.Send(new GetProjectOperationWbsByIdQuery(request.Id), ct);
        if (result.IsBad())
            return result.Failure<GetProjectOperationWbsByIdResponse>()!;

        return result;
    }

    public async Task<Result<GetFltrPOWbsResponse?>> GetFltrPOWbs(
        GetFltrPOWbsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetFltrPOWbs");
        var result = await _mediator.Send(new GetFltrPOWbsQuery(
            request.ProjectId,
            request.ProjectOperationIds,
            request.ProjectWbsIds,
            request.ProjectOperationWbsIds,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetFltrPOWbsResponse>()!;

        return result;
    }

    public async Task<Result<GetPOWbsByPOIdResponse?>> GetPOWbsByPOId(
        GetPOWbsByPOIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPOWbsByPOId");
        var result = await _mediator.Send(new GetPOWbsByPOIdQuery(request.ProjectOperationId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetPOWbsByPOIdResponse>()!;

        return result;
    }

    public async Task<Result<GetPOWbsByProjectWbsIdResponse?>> GetPOWbsByProjectWbsId(
        GetPOWbsByProjectWbsIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPOWbsByProjectWbsId");
        var result = await _mediator.Send(new GetPOWbsByProjectWbsIdQuery(request.ProjectWbsId,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetPOWbsByProjectWbsIdResponse>()!;

        return result;
    }

    public async Task<Result<GetDetailPOWbsByProjectWbsIdResponse?>> GetDetailPOWbsByProjectWbsId(
        GetDetailPOWbsByProjectWbsIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPOWbsByProjectWbsId");
        var result = await _mediator.Send(new GetDetailPOWbsByProjectWbsIdQuery(request.ProjectWbsId,
            request.FilterData,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetDetailPOWbsByProjectWbsIdResponse>()!;

        return result;
    }
}