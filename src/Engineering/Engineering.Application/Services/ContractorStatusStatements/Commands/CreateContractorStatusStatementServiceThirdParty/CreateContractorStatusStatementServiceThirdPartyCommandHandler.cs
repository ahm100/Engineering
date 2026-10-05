using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementServiceThirdParty;

public class CreateContractorStatusStatementServiceThirdPartyCommandHandler : ICommandHandler<CreateContractorStatusStatementServiceThirdPartyCommand, ContractorStatusStatementServiceThirdParty>
{
    private readonly ILogger<CreateContractorStatusStatementServiceThirdPartyCommandHandler> _logger;
    private readonly IContractorStatusStatementServiceThirdPartyRepository _repository;

    public CreateContractorStatusStatementServiceThirdPartyCommandHandler(
        ILogger<CreateContractorStatusStatementServiceThirdPartyCommandHandler> logger,
        IContractorStatusStatementServiceThirdPartyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementServiceThirdParty?>> Handle(CreateContractorStatusStatementServiceThirdPartyCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementServiceThirdParty>(SharedErrors.UnknownError);
        }
    }
}
