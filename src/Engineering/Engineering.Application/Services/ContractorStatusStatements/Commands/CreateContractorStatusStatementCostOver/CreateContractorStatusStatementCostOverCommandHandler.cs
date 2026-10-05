using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementCostOver;

public class CreateContractorStatusStatementCostOverCommandHandler : ICommandHandler<CreateContractorStatusStatementCostOverCommand, ContractorStatusStatementCostOver>
{
    private readonly ILogger<CreateContractorStatusStatementCostOverCommandHandler> _logger;
    private readonly IContractorStatusStatementCostOverRepository _repository;

    public CreateContractorStatusStatementCostOverCommandHandler(
        ILogger<CreateContractorStatusStatementCostOverCommandHandler> logger,
        IContractorStatusStatementCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementCostOver?>> Handle(CreateContractorStatusStatementCostOverCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementCostOver>(SharedErrors.UnknownError);
        }
    }
}
