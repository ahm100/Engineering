using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementFine;

public class CreateContractorStatusStatementFineCommandHandler : ICommandHandler<CreateContractorStatusStatementFineCommand, ContractorStatusStatementFine>
{
    private readonly ILogger<CreateContractorStatusStatementFineCommandHandler> _logger;
    private readonly IContractorStatusStatementFineRepository _repository;

    public CreateContractorStatusStatementFineCommandHandler(
        ILogger<CreateContractorStatusStatementFineCommandHandler> logger,
        IContractorStatusStatementFineRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementFine?>> Handle(CreateContractorStatusStatementFineCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementFine>(SharedErrors.UnknownError);
        }
    }
}
