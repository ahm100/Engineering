using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementServiceDaily;

public class CreateContractorStatusStatementServiceDailyCommandHandler : ICommandHandler<CreateContractorStatusStatementServiceDailyCommand, ContractorStatusStatementServiceDaily>
{
    private readonly ILogger<CreateContractorStatusStatementServiceDailyCommandHandler> _logger;
    private readonly IContractorStatusStatementServiceDailyRepository _repository;

    public CreateContractorStatusStatementServiceDailyCommandHandler(
        ILogger<CreateContractorStatusStatementServiceDailyCommandHandler> logger,
        IContractorStatusStatementServiceDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementServiceDaily?>> Handle(CreateContractorStatusStatementServiceDailyCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementServiceDaily>(SharedErrors.UnknownError);
        }
    }
}
