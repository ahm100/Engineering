using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.CreateProjectOperationTemporaryDaily;

public class CreateProjectOperationTemporaryDailyCommandHandler : ICommandHandler<CreateProjectOperationTemporaryDailyCommand, ProjectOperationTemporaryDaily>
{
    private readonly ILogger<CreateProjectOperationTemporaryDailyCommandHandler> _logger;
    private readonly IProjectOperationTemporaryDailyRepository _repository;

    public CreateProjectOperationTemporaryDailyCommandHandler(ILogger<CreateProjectOperationTemporaryDailyCommandHandler> logger, IProjectOperationTemporaryDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationTemporaryDaily?>> Handle(CreateProjectOperationTemporaryDailyCommand request, CT ct)
    {
        try
        {
            var entity = ProjectOperationTemporaryDaily.Create(request.CostCenter, request.Project, request.ProjectOperation, request.StartDate, request.EndDate,
                request.Description, request.Status);

            if (request.Documents != null && request.Documents.Any())
                entity.AddDocuments(request.Documents);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationTemporaryDaily>(SharedErrors.UnknownError);
        }
    }
}
