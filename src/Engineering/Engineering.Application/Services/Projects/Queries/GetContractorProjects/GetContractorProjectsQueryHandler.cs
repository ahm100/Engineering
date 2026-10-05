using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetContractorProjects;

public class GetContractorProjectsQueryHandler : IQueryHandler<GetContractorProjectsQuery, DataResult<List<Project>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetContractorProjectsQuery> _logger;

    public GetContractorProjectsQueryHandler(
        ILogger<GetContractorProjectsQuery> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Project>>?>> Handle(GetContractorProjectsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetContractorProjects(
                request.ContractorId,
                request.CostCenterId,
                request.FilterData,
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
