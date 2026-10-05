using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.CreateBatchPODContractorExpert;

public class CreateBatchPODContractorExpertCommandHandler : ICommandHandler<CreateBatchPODContractorExpertCommand, bool?>
{
    private readonly ILogger<CreateBatchPODContractorExpertCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorExpertRepository _repository;

    public CreateBatchPODContractorExpertCommandHandler(ILogger<CreateBatchPODContractorExpertCommandHandler> logger,
        IProjectOperationDetailContractorExpertRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(CreateBatchPODContractorExpertCommand request, CT ct)
    {
        try
        {
            var details = await _repository.GetByProjectOperationDetailContractorServiceIds(request.Models.Listed(x => x.ProjectOperationDetailContractorService.Id), ct);

            foreach (var item in request.Models)
            {
                var contractorServiceId = item.ProjectOperationDetailContractorService.Id;

                var entity = new ProjectOperationDetailContractorExpert(
                    item.ProjectOperationDetailContractorService,
                    item.ConsumableVolumeExpert,
                    item.Volume,
                    item.IsActive);

                item.ProjectOperationDetailContractorService.SubtractRemainingVolume(item.Volume);

                if (item.ProjectOperationDetailContractorService.RemainingVolume < 0)
                    return Result.Failure<bool?>(ProjectOperationDetailErrors.ContractorExpertVolumeAssignedCanNotBeBigger);

                await _repository.Create(entity, ct);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}