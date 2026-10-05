using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperation;

public class CreateEmployerStatusStatementProjectOperationCommandHandler : ICommandHandler<CreateEmployerStatusStatementProjectOperationCommand, EmployerStatusStatementProjectOperation>
{
    private readonly ILogger<CreateEmployerStatusStatementProjectOperationCommand> _logger;
    private readonly IEmployerStatusStatementProjectOperationRepository _repository;

    public CreateEmployerStatusStatementProjectOperationCommandHandler(ILogger<CreateEmployerStatusStatementProjectOperationCommand> logger, IEmployerStatusStatementProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatementProjectOperation?>> Handle(CreateEmployerStatusStatementProjectOperationCommand request, CT ct)
    {
        try
        {
            var result = await _repository.Create(request.EmployerStatusStatementProjectOperation, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatementProjectOperation>(SharedErrors.UnknownError);
        }
    }
}