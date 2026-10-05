using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailCostOver;

public class DeleteContractorContractDetailCostOverCommandHandler : ICommandHandler<DeleteContractorContractDetailCostOverCommand, ContractorContractDetailCostOver>
{
    private readonly ILogger<DeleteContractorContractDetailCostOverCommandHandler> _logger;
    private readonly IContractorContractDetailCostOverRepository _repository;

    public DeleteContractorContractDetailCostOverCommandHandler(
        ILogger<DeleteContractorContractDetailCostOverCommandHandler> logger,
        IContractorContractDetailCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetailCostOver?>> Handle(DeleteContractorContractDetailCostOverCommand request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorContractDetailCostOverById(request.Id, ct);
            if (result is null)
                return Result.Failure<ContractorContractDetailCostOver>(ContractorContractDetailPriceErrors.ContractorContractDetailPriceWithIdNotFound);

            result.SoftDelete();

            await _repository.Update(result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailCostOver>(SharedErrors.UnknownError);
        }
    }
}
