using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperationDetail;

public class CreateEmployerStatusStatementProjectOperationDetailCommandHandler : ICommandHandler<CreateEmployerStatusStatementProjectOperationDetailCommand, EmployerStatusStatementProjectOperationDetail>
{
    private readonly ILogger<CreateEmployerStatusStatementProjectOperationDetailCommand> _logger;
    private readonly IEmployerStatusStatementProjectOperationDetailRepository _repository;

    public CreateEmployerStatusStatementProjectOperationDetailCommandHandler(ILogger<CreateEmployerStatusStatementProjectOperationDetailCommand> logger, IEmployerStatusStatementProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatementProjectOperationDetail?>> Handle(CreateEmployerStatusStatementProjectOperationDetailCommand request, CT ct)
    {
        try
        {
            var entity = new EmployerStatusStatementProjectOperationDetail(
                request.StatementProjectOperation,
                request.ProjectOperationDetail,
                request.Description);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatementProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}