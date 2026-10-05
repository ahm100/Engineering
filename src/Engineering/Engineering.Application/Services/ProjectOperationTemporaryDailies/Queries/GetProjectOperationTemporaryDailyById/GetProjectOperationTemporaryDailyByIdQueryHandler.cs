using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetProjectOperationTemporaryDailyById;

public class GetProjectOperationTemporaryDailyByIdQueryHandler : IQueryHandler<GetProjectOperationTemporaryDailyByIdQuery, ProjectOperationTemporaryDaily>
{
    private readonly ILogger<GetProjectOperationTemporaryDailyByIdQueryHandler> _logger;
    private readonly IProjectOperationTemporaryDailyRepository _repository;

    public GetProjectOperationTemporaryDailyByIdQueryHandler(ILogger<GetProjectOperationTemporaryDailyByIdQueryHandler> logger, IProjectOperationTemporaryDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationTemporaryDaily?>> Handle(GetProjectOperationTemporaryDailyByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.ProjectOperationTemporaryDailyId, ct);
            return result ?? Result.Failure<ProjectOperationTemporaryDaily>(ProjectOperationTemporaryDailyErrors.TemporaryDailyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationTemporaryDaily>(SharedErrors.UnknownError);
        }
    }
}
