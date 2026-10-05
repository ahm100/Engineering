using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementService;

public class CreateContractorStatusStatementServiceCommandHandler : ICommandHandler<CreateContractorStatusStatementServiceCommand, ContractorStatusStatementService>
{
    private readonly ILogger<CreateContractorStatusStatementServiceCommandHandler> _logger;
    private readonly IContractorStatusStatementServiceRepository _repository;

    public CreateContractorStatusStatementServiceCommandHandler(
        ILogger<CreateContractorStatusStatementServiceCommandHandler> logger,
        IContractorStatusStatementServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementService?>> Handle(CreateContractorStatusStatementServiceCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementService>(SharedErrors.UnknownError);
        }
    }
}
