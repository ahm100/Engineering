using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsProjectByProjectManagerId;

public class GetsProjectByProjectManagerIdQueryHandler : IQueryHandler<GetsProjectByProjectManagerIdQuery, DataResult<List<Project>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetsProjectByProjectManagerIdQueryHandler> _logger;

    public GetsProjectByProjectManagerIdQueryHandler(
        ILogger<GetsProjectByProjectManagerIdQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Project>>?>> Handle(GetsProjectByProjectManagerIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectByProjectManagerId(
                request.CostCenterIds,
                request.ProjectManagerId,
                request.FilterData,
                request.CompanyId,
                request.Statuses,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<Project>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Project>>>(ProjectErrors.FilteredProjectNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Project>>>(SharedErrors.UnknownError);
        }
    }
}
