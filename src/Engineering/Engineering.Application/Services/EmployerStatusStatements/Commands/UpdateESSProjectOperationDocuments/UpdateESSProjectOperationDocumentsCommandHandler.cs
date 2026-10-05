using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDocuments;

public class UpdateESSProjectOperationDocumentsCommandHandler : ICommandHandler<UpdateESSProjectOperationDocumentsCommand, EmployerStatusStatementProjectOperation>
{
    private readonly ILogger<UpdateESSProjectOperationDocumentsCommandHandler> _logger;
    private readonly IEmployerStatusStatementProjectOperationRepository _repository;

    public UpdateESSProjectOperationDocumentsCommandHandler(
        ILogger<UpdateESSProjectOperationDocumentsCommandHandler> logger,
        IEmployerStatusStatementProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<EmployerStatusStatementProjectOperation?>> Handle(UpdateESSProjectOperationDocumentsCommand request, CT ct)
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
            return Result.Failure<EmployerStatusStatementProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
