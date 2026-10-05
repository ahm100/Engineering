using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorExperts.Commands.UpdateBatchPODContractorExpert;

public class UpdateBatchPODContractorExpertCommandHandler : ICommandHandler<UpdateBatchPODContractorExpertCommand, bool?>
{
    private readonly ILogger<UpdateBatchPODContractorExpertCommandHandler> _logger;
    private readonly IProjectOperationDetailContractorExpertRepository _repository;

    public UpdateBatchPODContractorExpertCommandHandler(ILogger<UpdateBatchPODContractorExpertCommandHandler> logger,
        IProjectOperationDetailContractorExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(UpdateBatchPODContractorExpertCommand request, CT ct)
    {
        try
        {
            var ids = request.Models.Listed(x => x.Id);
            var contractorExperts = await _repository.GetByIds(ids, ct);
            if (contractorExperts is null || contractorExperts.Count != ids.Count || ids.Except(contractorExperts.Select(x => x.Id)).Any())
                return Result.Failure<bool?>(ProjectOperationDetailErrors.ContractorExpertNotFound);

            var serviceIds = contractorExperts
                .Listed(x => x.ProjectOperationDetailContractorServiceId);

            foreach (var item in request.Models)
            {
                var expert = contractorExperts.First(x => x.Id == item.Id);

                var service = expert.ProjectOperationDetailContractorService;

                var delta = item.Volume - expert.Volume;
                expert.Update(null, null, item.Volume, item.IsActive);
                service.SubtractRemainingVolume(delta);

                if (service.RemainingVolume < 0)
                    return Result.Failure<bool?>(ProjectOperationDetailErrors.ContractorExpertVolumeAssignedCanNotBeBigger);

                await _repository.Update(expert);
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