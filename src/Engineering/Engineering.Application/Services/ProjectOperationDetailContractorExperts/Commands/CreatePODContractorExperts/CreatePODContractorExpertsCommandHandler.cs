using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreatePODContractorExperts;

public class CreatePODContractorExpertsCommandHandler : ICommandHandler<CreatePODContractorExpertsCommand, ProjectOperationDetailContractorExpert?>
{
    private readonly ILogger<CreatePODContractorExpertsCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorExpertRepository _repository;
    private readonly IMediator _mediator;

    public CreatePODContractorExpertsCommandHandler(ILogger<CreatePODContractorExpertsCommandHandler> logger,
        IProjectOperationDetailContractorExpertRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectOperationDetailContractorExpert?>> Handle(CreatePODContractorExpertsCommand request, CT ct)
    {
        try
        {
            var pODContractorService = await _mediator.Send(new GetContractorServiceByIdQuery(
                request.ProjectOperationDetailContractorServiceId), ct);
            if (pODContractorService.IsBad())
                return pODContractorService.Failure<ProjectOperationDetailContractorExpert?>();

            var consumeVolumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery(request.ConsumableVolumeExpertId), ct);
            if (consumeVolumeExpert.IsBad())
                return consumeVolumeExpert.Failure<ProjectOperationDetailContractorExpert?>();

            var entity = new ProjectOperationDetailContractorExpert(pODContractorService.Value!,
                consumeVolumeExpert.Value!,
                request.Volume,
                request.IsActive);

            pODContractorService.Value!.SubtractRemainingVolume(request.Volume);

            if (pODContractorService.Value.RemainingVolume < 0)
                return Result.Failure<ProjectOperationDetailContractorExpert?>(ProjectOperationDetailErrors.ContractorExpertVolumeAssignedCanNotBeBigger);

            var create = await _repository.Create(entity, ct);
            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorExpert?>(SharedErrors.UnknownError);
        }
    }
}