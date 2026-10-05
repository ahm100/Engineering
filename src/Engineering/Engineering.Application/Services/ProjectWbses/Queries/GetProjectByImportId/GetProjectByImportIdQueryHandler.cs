using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectWbses.Queries.GetProjectByImportId;

public class GetProjectByImportIdQueryHandler : IQueryHandler<GetProjectByImportIdQuery, Project?>
{
    private readonly ILogger<GetProjectByImportIdQueryHandler> _logger;
    private readonly IProjectScheduleImportRepository _repository;

    public GetProjectByImportIdQueryHandler(
        ILogger<GetProjectByImportIdQueryHandler> logger,
        IProjectScheduleImportRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Project?>> Handle(
        GetProjectByImportIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectByImportId(
                request.ImportId,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Project?>(SharedErrors.UnknownError);
        }
    }
}
