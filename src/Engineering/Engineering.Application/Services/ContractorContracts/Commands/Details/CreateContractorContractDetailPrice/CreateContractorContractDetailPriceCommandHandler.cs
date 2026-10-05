using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailPrice;

public class CreateContractorContractDetailPriceCommandHandler : ICommandHandler<CreateContractorContractDetailPriceCommand, ContractorContractDetailPrice>
{
    private readonly ILogger<CreateContractorContractDetailPriceCommandHandler> _logger;
    private readonly IContractorContractDetailPriceRepository _repository;

    public CreateContractorContractDetailPriceCommandHandler(
        ILogger<CreateContractorContractDetailPriceCommandHandler> logger,
        IContractorContractDetailPriceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractDetailPrice?>> Handle(CreateContractorContractDetailPriceCommand request, CT ct)
    {
        try
        {
            var result = ContractorContractDetailPrice.Create(
                request.ContractorContractDetail,
                request.StartDate,
                request.EndDate,
                request.Price,
                request.CurrencyId,
                request.IsActive
                );

            await _repository.Create(result, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractDetailPrice>(SharedErrors.UnknownError);
        }
    }
}
