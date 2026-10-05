using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperationDetails;

public class GetFilteredDailyProjectOperationDetailsQueryHandler : IQueryHandler<GetFilteredDailyProjectOperationDetailsQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly ILogger<GetFilteredDailyProjectOperationDetailsQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetFilteredDailyProjectOperationDetailsQueryHandler(ILogger<GetFilteredDailyProjectOperationDetailsQueryHandler> logger,
                                                               IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetFilteredDailyProjectOperationDetailsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDailyProjectOperationDetailFilter(request.ProjectOperationId, request.Status, request.StartDate,
                request.EndDate, request.CreateDate, request.OperationLocationIds, request.ServiceInfoIds, request.ContractorId, request.FilterData,
                request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(DailyProjectOperationErrors.ProjectOperationDetailWithProjectOperationIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}
