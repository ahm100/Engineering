using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsProjectSorting;

public class GetsProjectSortingQueryHandler : IQueryHandler<GetsProjectSortingQuery, DataResult<IQueryable<Project>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetsProjectSortingQueryHandler> _logger;

    public GetsProjectSortingQueryHandler(
        ILogger<GetsProjectSortingQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Result<DataResult<IQueryable<Project>>?>> Handle(GetsProjectSortingQuery request, CT ct)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        try
        {
            var result = _repository.GetsProjectSorting(
                request.FilterData,
                request.Status,
                request.IsActive,
                request.CompanyId,
                request.HaveCostCenter,
                request.IsOrganizationUnit,
                request.Statuses,
                ct);

            return result.Any() ?
                new DataResult<IQueryable<Project>>
                {
                    Data = result,
                } : Result.Failure<DataResult<IQueryable<Project>>>(ProjectErrors.FilteredProjectNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<IQueryable<Project>>>(SharedErrors.UnknownError);
        }
    }
}
