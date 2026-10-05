using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperationDetailDaily;

public class CreateEmployerStatusStatementProjectOperationDetailDailyCommandHandler : ICommandHandler<CreateEmployerStatusStatementProjectOperationDetailDailyCommand, EmployerStatusStatementProjectOperationDetailDaily>
{
    private readonly ILogger<CreateEmployerStatusStatementProjectOperationDetailDailyCommand> _logger;
    private readonly IEmployerStatusStatementProjectOperationDetailDailyRepository _repository;

    public CreateEmployerStatusStatementProjectOperationDetailDailyCommandHandler(
        ILogger<CreateEmployerStatusStatementProjectOperationDetailDailyCommand> logger,
        IEmployerStatusStatementProjectOperationDetailDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<EmployerStatusStatementProjectOperationDetailDaily?>> Handle(CreateEmployerStatusStatementProjectOperationDetailDailyCommand request, CT ct)
    {
        try
        {
            var entity = new EmployerStatusStatementProjectOperationDetailDaily(
                request.StatementProjectOperationDetail,
                request.DailyProjectOperation);

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatementProjectOperationDetailDaily>(SharedErrors.UnknownError);
        }
    }
}