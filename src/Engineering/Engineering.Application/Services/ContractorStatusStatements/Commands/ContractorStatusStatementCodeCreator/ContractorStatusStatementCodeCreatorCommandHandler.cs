using Engineering.Application.Abstractions.Data.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementCodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<ContractorStatusStatementCodeCreatorCommand, string?>
{
    private readonly ILogger<ContractorStatusStatementCodeCreatorCommand> _logger;
    private readonly IContractorStatusStatementRepository _repository;

    public CodeCreatorCommandHandler(ILogger<ContractorStatusStatementCodeCreatorCommand> logger, IContractorStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(ContractorStatusStatementCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }
}