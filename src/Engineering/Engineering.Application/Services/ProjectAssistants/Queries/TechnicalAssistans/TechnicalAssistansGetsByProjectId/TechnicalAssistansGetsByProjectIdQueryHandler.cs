
using Engineering.Application.Abstractions.Data.Projects;
using ProjectTechnicalAssistant = Engineering.Domain.Entities.Projects.ProjectUsers.ProjectTechnicalAssistant;

namespace Engineering.Application.Services.ProjectAssistants.Queries.TechnicalAssistans.TechnicalAssistansGetsByProjectId;

public class TechnicalAssistansGetsByProjectIdQueryHandler : IQueryHandler<TechnicalAssistansGetsByProjectIdQuery, DataResult<List<ProjectTechnicalAssistant>>>
{
    private readonly IProjectTechnicalAssistantRepository _repository;
    private readonly ILogger<TechnicalAssistansGetsByProjectIdQueryHandler> _logger;

    public TechnicalAssistansGetsByProjectIdQueryHandler(ILogger<TechnicalAssistansGetsByProjectIdQueryHandler> logger, IProjectTechnicalAssistantRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectTechnicalAssistant>>?>> Handle(TechnicalAssistansGetsByProjectIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetImpProjectTechnicalAssistantByProjectId(request.ProjectId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectTechnicalAssistant>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectTechnicalAssistant>>>(ProjectAssistantErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectTechnicalAssistant>>>(SharedErrors.UnknownError);
        }
    }
}