
using Engineering.Application.Abstractions.Data.Projects;
using ProjectImplementationAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectImplementationAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Queries.ImplementationAssistans.ImplementationAssistansGetsByProjectId;

public class ImplementationAssistansGetsByProjectIdQueryHandler : IQueryHandler<ImplementationAssistansGetsByProjectIdQuery, DataResult<List<ProjectImplementationAssistant>>>
{
    private readonly IProjectImplementationAssistantRepository _repository;
    private readonly ILogger<ImplementationAssistansGetsByProjectIdQueryHandler> _logger;

    public ImplementationAssistansGetsByProjectIdQueryHandler(ILogger<ImplementationAssistansGetsByProjectIdQueryHandler> logger, IProjectImplementationAssistantRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectImplementationAssistant>>?>> Handle(ImplementationAssistansGetsByProjectIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetImplementationAssistansByProjectId(request.ProjectId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectImplementationAssistant>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectImplementationAssistant>>>(ProjectAssistantErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectImplementationAssistant>>>(SharedErrors.UnknownError);
        }
    }
}