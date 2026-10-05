using Engineering.Application.Abstractions.Data.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationContractors;

public class GetDailyProjectOperationContractorsQueryHandler : IQueryHandler<GetDailyProjectOperationContractorsQuery, DataResult<List<long>>>
{
    private readonly IDailyProjectOperationRepository _repository;
    private readonly ILogger<GetDailyProjectOperationContractorsQueryHandler> _logger;

    public GetDailyProjectOperationContractorsQueryHandler(ILogger<GetDailyProjectOperationContractorsQueryHandler> logger, IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetDailyProjectOperationContractorsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredContractors(request.CostCenterIds, request.ProjectIds, request.OperationInfoIds, request.ProjectOperationIds, request.ProjectOperationDetailIds, ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(DailyProjectOperationErrors.DailyProjectOperationContractorsWithFilterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
