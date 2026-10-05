using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetailCostOver;

public class UpdateContractorContractDetailCostOverCommandHandler : ICommandHandler<UpdateContractorContractDetailCostOverCommand, ContractorContractDetailCostOver>
{
    private readonly ILogger<UpdateContractorContractDetailCostOverCommandHandler> _logger;
    private readonly IContractorContractDetailCostOverRepository _repository;

    public UpdateContractorContractDetailCostOverCommandHandler(
        ILogger<UpdateContractorContractDetailCostOverCommandHandler> logger,
        IContractorContractDetailCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetailCostOver?>> Handle(UpdateContractorContractDetailCostOverCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorContractDetailCostOverById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorContractDetailCostOver>(ContractorContractDetailPriceErrors.ContractorContractDetailPriceWithIdNotFound);

            entity.SetCostOver(request.CostOver);
            entity.SetContractorId(request.ContractorId);
            entity.SetDescription(request.Description);

            if (request.ContractorContract is not null)
                entity.SetPercentageContract(request.Percentage);
            if (request.ContractorContractDetail is not null)
                entity.SetPercentageContractDetail(request.Percentage);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailCostOver>(SharedErrors.UnknownError);
        }
    }
}
