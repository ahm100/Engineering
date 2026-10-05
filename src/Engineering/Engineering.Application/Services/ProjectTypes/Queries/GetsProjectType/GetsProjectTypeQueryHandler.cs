using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetsProjectType;

public class GetsProjectTypeQueryHandler : IQueryHandler<GetsProjectTypeQuery, DataResult<List<ProjectType>>>
{
    private readonly IProjectTypeRepository _repository;
    private readonly ILogger<GetsProjectTypeQueryHandler> _logger;

    public GetsProjectTypeQueryHandler(ILogger<GetsProjectTypeQueryHandler> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectType>>?>> Handle(GetsProjectTypeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectType(request.Ids, request.FilterData, request.IsActive, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectType>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectType>>>(ProjectErrors.FilteredProjectNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectType>>>(SharedErrors.UnknownError);
        }
    }
}