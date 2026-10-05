using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetCurrentUserTemporaryDailies;

public class GetCurrentUserTemporaryDailiesQueryHandler : IQueryHandler<GetCurrentUserTemporaryDailiesQuery, DataResult<List<ProjectOperationTemporaryDaily>>>
{
    private readonly ILogger<GetCurrentUserTemporaryDailiesQueryHandler> _logger;
    private readonly IProjectOperationTemporaryDailyRepository _repository;

    public GetCurrentUserTemporaryDailiesQueryHandler(ILogger<GetCurrentUserTemporaryDailiesQueryHandler> logger, IProjectOperationTemporaryDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationTemporaryDaily>>?>> Handle(GetCurrentUserTemporaryDailiesQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetCurrentUserTemporaryDailies(request.CreatorId, request.CostCenterId,
                request.ProjectId, request.ProjectOperationId, request.StartDate, request.EndDate, request.Status, request.FilterData,
                request.OrderBy, request.PageIndex, request.PageSize, ct);

            return entities.Data.Any()
                    ? new DataResult<List<ProjectOperationTemporaryDaily>>
                    {
                        Data = entities.Data,
                        RowCount = entities.RowCount
                    } : Result.Failure<DataResult<List<ProjectOperationTemporaryDaily>>>(ProjectOperationTemporaryDailyErrors.FilteredTemporaryDailyNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ProjectOperationTemporaryDaily>>>(SharedErrors.UnknownError);
        }
    }
}
