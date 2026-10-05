using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementReward;

public class CreateContractorStatusStatementRewardCommandHandler : ICommandHandler<CreateContractorStatusStatementRewardCommand, ContractorStatusStatementReward>
{
    private readonly ILogger<CreateContractorStatusStatementRewardCommandHandler> _logger;
    private readonly IContractorStatusStatementRewardRepository _repository;

    public CreateContractorStatusStatementRewardCommandHandler(
        ILogger<CreateContractorStatusStatementRewardCommandHandler> logger,
        IContractorStatusStatementRewardRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementReward?>> Handle(CreateContractorStatusStatementRewardCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementReward>(SharedErrors.UnknownError);
        }
    }
}
