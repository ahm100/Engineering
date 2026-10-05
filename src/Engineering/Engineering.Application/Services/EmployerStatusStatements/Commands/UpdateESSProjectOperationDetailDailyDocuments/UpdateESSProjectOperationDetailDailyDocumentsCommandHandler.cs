using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyDocuments;

public class UpdateESSProjectOperationDetailDailyDocumentsCommandHandler : ICommandHandler<UpdateESSProjectOperationDetailDailyDocumentsCommand, EmployerStatusStatementProjectOperationDetailDaily>
{
    private readonly ILogger<UpdateESSProjectOperationDetailDailyDocumentsCommandHandler> _logger;
    private readonly IEmployerStatusStatementProjectOperationDetailDailyRepository _repository;

    public UpdateESSProjectOperationDetailDailyDocumentsCommandHandler(
        ILogger<UpdateESSProjectOperationDetailDailyDocumentsCommandHandler> logger,
        IEmployerStatusStatementProjectOperationDetailDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<EmployerStatusStatementProjectOperationDetailDaily?>> Handle(UpdateESSProjectOperationDetailDailyDocumentsCommand request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var entity = request.Entity;

            if (request.Urls is not null)
                entity.AddDocuments(request.Urls, false);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EmployerStatusStatementProjectOperationDetailDaily>(SharedErrors.UnknownError);
        }
    }
}
