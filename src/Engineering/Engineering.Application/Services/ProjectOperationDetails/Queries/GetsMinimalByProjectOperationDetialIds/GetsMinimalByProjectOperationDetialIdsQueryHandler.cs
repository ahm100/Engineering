using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsMinimalByProjectOperationDetialIds;

public class GetsMinimalByProjectOperationDetialIdsQueryHandler : IQueryHandler<GetsMinimalByProjectOperationDetialIdsQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsMinimalByProjectOperationDetialIdsQueryHandler> _logger;

    public GetsMinimalByProjectOperationDetialIdsQueryHandler(ILogger<GetsMinimalByProjectOperationDetialIdsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsMinimalByProjectOperationDetialIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMinimalByProjectOperationDetialIds(request.Ids, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}
