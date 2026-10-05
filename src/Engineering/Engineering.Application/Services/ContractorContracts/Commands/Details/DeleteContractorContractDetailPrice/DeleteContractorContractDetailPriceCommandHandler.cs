using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.DeleteContractorContractDetailPrice;

public class DeleteContractorContractDetailPriceCommandHandler : ICommandHandler<DeleteContractorContractDetailPriceCommand, ContractorContractDetailPrice>
{
    private readonly ILogger<DeleteContractorContractDetailPriceCommandHandler> _logger;
    private readonly IContractorContractDetailPriceRepository _repository;

    public DeleteContractorContractDetailPriceCommandHandler(ILogger<DeleteContractorContractDetailPriceCommandHandler> logger,
                                                             IContractorContractDetailPriceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetailPrice?>> Handle(DeleteContractorContractDetailPriceCommand request, CT ct)
    {
        try
        {
            var result = await _repository.FindById(request.Id, ct);
            if (result is null)
                return Result.Failure<ContractorContractDetailPrice>(ContractorContractDetailPriceErrors.ContractorContractDetailPriceWithIdNotFound);
            if (result.IsDeleted)
                return Result.Failure<ContractorContractDetailPrice>(ContractorContractDetailPriceErrors.IsDeleted);

            result.SoftDelete();
            result.AddHistory();

            await _repository.Update(result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailPrice>(SharedErrors.UnknownError);
        }
    }
}
