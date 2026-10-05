using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContractHeaders.CreateContractorContractHeader;

public class CreateContractorContractHeaderCommandHandler : ICommandHandler<CreateContractorContractHeaderCommand, ContractorContractHeader>
{
    private readonly ILogger<CreateContractorContractHeaderCommandHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public CreateContractorContractHeaderCommandHandler(
        ILogger<CreateContractorContractHeaderCommandHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorContractHeader?>> Handle(CreateContractorContractHeaderCommand request, CT ct)
    {
        try
        {
            var result = new ContractorContractHeader(
                request.CostCenter,
                request.ContractorId,
                request.CurrencyId,
                request.Description,
                request.Urls,
                request.CompanyId);

            await _repository.Create(result, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorContractHeader>(SharedErrors.UnknownError);
        }
    }
}
