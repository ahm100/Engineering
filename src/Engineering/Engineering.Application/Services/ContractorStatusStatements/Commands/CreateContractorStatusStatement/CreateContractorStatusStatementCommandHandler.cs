using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatement;

public class CreateContractorStatusStatementCommandHandler : ICommandHandler<CreateContractorStatusStatementCommand, ContractorStatusStatement>
{
    private readonly ILogger<CreateContractorStatusStatementCommandHandler> _logger;
    private readonly IContractorStatusStatementRepository _repository;

    public CreateContractorStatusStatementCommandHandler(
        ILogger<CreateContractorStatusStatementCommandHandler> logger,
        IContractorStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatement?>> Handle(CreateContractorStatusStatementCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.Create(request.Entity, ct);
            entity.ConfigPayment(0, null, null, null);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
