using Engineering.Application.Abstractions.Data.Projects;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsByNameOrCode;

public class GetsByNameOrCodeQueryHandler : IQueryHandler<GetsByNameOrCodeQuery, DataResult<List<Project>>>
{
    private readonly IProjectRepository _repository;
    private readonly ILogger<GetsByNameOrCodeQueryHandler> _logger;

    public GetsByNameOrCodeQueryHandler(
        ILogger<GetsByNameOrCodeQueryHandler> logger,
        IProjectRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Project>>?>> Handle(GetsByNameOrCodeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByNameOrCode(
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