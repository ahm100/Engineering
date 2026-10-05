using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDetail;

public class CreateContractorStatusStatementDetailCommandHandler : ICommandHandler<CreateContractorStatusStatementDetailCommand, ContractorStatusStatementDetail>
{
    private readonly ILogger<CreateContractorStatusStatementDetailCommandHandler> _logger;
    private readonly IContractorStatusStatementDetailRepository _repository;

    public CreateContractorStatusStatementDetailCommandHandler(
        ILogger<CreateContractorStatusStatementDetailCommandHandler> logger,
        IContractorStatusStatementDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorStatusStatementDetail?>> Handle(CreateContractorStatusStatementDetailCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorStatusStatementDetail>(SharedErrors.UnknownError);
        }
    }
}
