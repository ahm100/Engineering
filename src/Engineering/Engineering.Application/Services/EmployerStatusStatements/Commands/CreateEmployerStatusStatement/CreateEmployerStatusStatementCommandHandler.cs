using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatement;

public class CreateEmployerStatusStatementCommandHandler : ICommandHandler<CreateEmployerStatusStatementCommand, EmployerStatusStatement>
{
    private readonly ILogger<CreateEmployerStatusStatementCommand> _logger;
    private readonly IEmployerStatusStatementRepository _repository;

    public CreateEmployerStatusStatementCommandHandler(ILogger<CreateEmployerStatusStatementCommand> logger, IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatement?>> Handle(CreateEmployerStatusStatementCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.Entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}