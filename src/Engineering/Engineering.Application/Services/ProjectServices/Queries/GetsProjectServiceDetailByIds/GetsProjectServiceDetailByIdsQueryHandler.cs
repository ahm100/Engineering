using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceDetailByIds;

public class GetsProjectServiceDetailByIdsQueryHandler : IQueryHandler<GetsProjectServiceDetailByIdsQuery, DataResult<List<ProjectServiceDetail>>>
{
    private readonly IProjectServiceDetailRepository _repository;
    private readonly ILogger<GetsProjectServiceDetailByIdsQueryHandler> _logger;

    public GetsProjectServiceDetailByIdsQueryHandler(ILogger<GetsProjectServiceDetailByIdsQueryHandler> logger, IProjectServiceDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectServiceDetail>>?>> Handle(GetsProjectServiceDetailByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectServiceDetailByIds(
                request.Ids,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectServiceDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectServiceDetail>>>(ProjectServiceErrors.ProjectServicesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectServiceDetail>>>(SharedErrors.UnknownError);
        }
    }
}