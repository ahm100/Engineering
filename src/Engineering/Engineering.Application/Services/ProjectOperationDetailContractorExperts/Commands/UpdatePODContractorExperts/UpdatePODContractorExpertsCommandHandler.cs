using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ConsumableVolumes.Queries.Experts.GetExpertConsumableVolumeById;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetById;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Errors.WbsTemplates;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdatePODContractorExperts;

public class UpdatePODContractorExpertsCommandHandler : ICommandHandler<UpdatePODContractorExpertsCommand, ProjectOperationDetailContractorExpert?>
{
    private readonly ILogger<UpdatePODContractorExpertsCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorExpertRepository _repository;
    private readonly IMediator _mediator;

    public UpdatePODContractorExpertsCommandHandler(ILogger<UpdatePODContractorExpertsCommandHandler> logger,
        IProjectOperationDetailContractorExpertRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<ProjectOperationDetailContractorExpert?>> Handle(UpdatePODContractorExpertsCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailContractorExpert?>(WbsTemplateErrors.ProjectOperationWbsWithIdNotFound);

            ProjectOperationDetailContractorService? contractorService = null;
            if (request.ProjectOperationDetailContractorServiceId is not null)
            {
                var pODContractorService = await _mediator.Send(new GetContractorServiceByIdQuery(
                    request.ProjectOperationDetailContractorServiceId.Value), ct);
                if (pODContractorService.IsBad())
                    return pODContractorService.Failure<ProjectOperationDetailContractorExpert?>();
                contractorService = pODContractorService.Value;
            }

            ConsumableVolumeExpert? expert = null;
            if (request.ConsumableVolumeExpertId is not null)
            {
                var consumeVolumeExpert = await _mediator.Send(new GetConsumableVolumeExpertByIdQuery(
                    request.ConsumableVolumeExpertId.Value), ct);
                if (consumeVolumeExpert.IsBad())
                    return consumeVolumeExpert.Failure<ProjectOperationDetailContractorExpert?>();
                expert = consumeVolumeExpert.Value;
            }

            entity.Update(contractorService,
                expert,
                request.Volume,
                request.IsActive);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailContractorExpert?>(SharedErrors.UnknownError);
        }
    }
}