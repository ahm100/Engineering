using Engineering.Application.Abstractions.Data.Projects;
using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetActiveProjectTypes;

public class GetActiveProjectTypesQueryHandler : IQueryHandler<GetActiveProjectTypesQuery, DataResult<List<ProjectType>>>
{
    private readonly IProjectTypeRepository _repository;
    private readonly ILogger<GetActiveProjectTypesQuery> _logger;

    public GetActiveProjectTypesQueryHandler(ILogger<GetActiveProjectTypesQuery> logger, IProjectTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectType>>?>> Handle(GetActiveProjectTypesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveProjectTypes(request.FilterData, request.code, request.name, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectType>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectType>>>(ProjectTypeErrors.ProjectTypesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectType>>>(SharedErrors.UnknownError);
        }
    }
}