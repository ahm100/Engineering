using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateEmployerStatusStatementDocuments;

public class UpdateEmployerStatusStatementDocumentsCommandHandler : ICommandHandler<UpdateEmployerStatusStatementDocumentsCommand, EmployerStatusStatement>
{
    private readonly ILogger<UpdateEmployerStatusStatementDocumentsCommandHandler> _logger;
    private readonly IEmployerStatusStatementRepository _repository;

    public UpdateEmployerStatusStatementDocumentsCommandHandler(
        ILogger<UpdateEmployerStatusStatementDocumentsCommandHandler> logger,
        IEmployerStatusStatementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<EmployerStatusStatement?>> Handle(UpdateEmployerStatusStatementDocumentsCommand request, CT ct)
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
            return Result.Failure<EmployerStatusStatement>(SharedErrors.UnknownError);
        }
    }
}
