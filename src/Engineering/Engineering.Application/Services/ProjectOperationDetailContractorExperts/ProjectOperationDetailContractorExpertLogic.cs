using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreatePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.DeletePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdatePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.CreatePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.DeletePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.UpdatePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Queries.GetPODContractorExpertsByCServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Queries.GetPODContractorExpertsByCVEId;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts;

public class ProjectOperationDetailContractorExpertLogic : IProjectOperationDetailContractorExpertLogic
{
    private IMediator _mediator;
    private ILogger<ProjectOperationDetailContractorExpertLogic> _logger;
    private IUnitOfWork _unitOfWork;

    public ProjectOperationDetailContractorExpertLogic(
        IMediator mediator,
        ILogger<ProjectOperationDetailContractorExpertLogic> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreatePODContractorExpertsResponse?>> CreatePODContractorExperts(
        CreatePODContractorExpertsRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreatePODContractorExperts");
        var create = await _mediator.Send(new CreatePODContractorExpertsCommand(request.ConsumableVolumeExpertId,
            request.ProjectOperationDetailContractorServiceId,
            request.Volume,
            request.IsActive), ct);
        if (create.IsBad())
            return create.Failure<CreatePODContractorExpertsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreatePODContractorExpertsResponse(true);
    }

    public async Task<Result<UpdatePODContractorExpertsResponse?>> UpdatePODContractorExperts(
        UpdatePODContractorExpertsRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdatePODContractorExperts");
        var update = await _mediator.Send(new UpdatePODContractorExpertsCommand(request.Id,
            request.ConsumableVolumeExpertId,
            request.ProjectOperationDetailContractorServiceId,
            request.Volume,
            request.IsActive), ct);
        if (update.IsBad())
            return update.Failure<UpdatePODContractorExpertsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdatePODContractorExpertsResponse(true);
    }

    public async Task<Result<DeletePODContractorExpertsResponse?>> DeletePODContractorExperts(
        DeletePODContractorExpertsRequest request, CT ct)
    {
        _logger.LogInformation("Request for DeletePODContractorExperts");
        var delete = await _mediator.Send(new DeletePODContractorExpertsCommand(request.Id), ct);
        if (delete.IsBad())
            return delete.Failure<DeletePODContractorExpertsResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeletePODContractorExpertsResponse(true);
    }

    public async Task<Result<GetPODContractorExpertsByCServiceIdResponse?>> GetPODContractorExpertsByCServiceId(
        GetPODContractorExpertsByCServiceIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPODContractorExpertsByCServiceId");
        var result = await _mediator.Send(new GetPODContractorExpertsByCServiceIdQuery(request.ProjectOperationDetailContractorServiceId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetPODContractorExpertsByCServiceIdResponse>()!;

        return result;
    }

    public async Task<Result<GetPODContractorExpertsByCVEIdResponse?>> GetPODContractorExpertsByCVEId(
        GetPODContractorExpertsByCVEIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetPODContractorExpertsByCVEId");
        var result = await _mediator.Send(new GetPODContractorExpertsByCVEIdQuery(request.ConsumableVolumeExpertId,
            request.PageIndex,
            request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetPODContractorExpertsByCVEIdResponse>()!;

        return result;
    }
}