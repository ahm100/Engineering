
using Engineering.Application.Abstractions.Data.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.EmployerStatusStatementCodeCreator;

public class CodeCreatorCommandHandler : ICommandHandler<EmployerStatusStatementCodeCreatorCommand, string?>
{
    private readonly ILogger<EmployerStatusStatementCodeCreatorCommand> _logger;
    private readonly IEmployerStatusStatementRepository _repository;

    public CodeCreatorCommandHandler(ILogger<EmployerStatusStatementCodeCreatorCommand> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(EmployerStatusStatementCodeCreatorCommand request, CT ct)
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