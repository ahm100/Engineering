using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperations;

public class GetFilteredDailyProjectOperationsQueryHandler : IQueryHandler<GetFilteredDailyProjectOperationsQuery, DataResult<List<ProjectOperation>>>
{
    private readonly ILogger<GetFilteredDailyProjectOperationsQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetFilteredDailyProjectOperationsQueryHandler(ILogger<GetFilteredDailyProjectOperationsQueryHandler> logger,
                                                         IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperation>>?>> Handle(GetFilteredDailyProjectOperationsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDailyProjectOperationFilter(request.CostCenterId,
                                                                          request.ProjectId,
                                                                          request.ContractorId,
                                                                          request.StartDate,
                                                                          request.EndDate,
                                                                          request.FilterData,
                                                                          request.PageIndex,
                                                                          request.PageSize,
                                                                          ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperation>>>(ProjectOperationErrors.ProjectOperationWithFilterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperation>>>(SharedErrors.UnknownError);
        }
    }
}
