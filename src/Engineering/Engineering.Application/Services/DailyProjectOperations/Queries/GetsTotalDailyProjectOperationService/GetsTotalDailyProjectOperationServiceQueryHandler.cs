using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsTotalDailyProjectOperationService;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsTotalDailyProjectOperationService;

public class GetsTotalDailyProjectOperationServiceQueryHandler : IQueryHandler<GetsTotalDailyProjectOperationServiceQuery, List<GetsTotalDailyProjectOperationServiceModel>>
{
    private readonly ILogger<GetsTotalDailyProjectOperationServiceQueryHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public GetsTotalDailyProjectOperationServiceQueryHandler(ILogger<GetsTotalDailyProjectOperationServiceQueryHandler> logger,
                                                       IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetsTotalDailyProjectOperationServiceModel>?>> Handle(GetsTotalDailyProjectOperationServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsTotalDailyProjectOperationService(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ContractorIds,
                request.ServiceInfoIds,
                request.MeasurUnitIds,
                request.StartDate,
                request.EndDate,
                request.FromDate,
                request.ToDate,
                request.Status,
                request.FilterData,
                request.FilterServiceInfo,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetsTotalDailyProjectOperationServiceModel>>(SharedErrors.UnknownError);
        }
    }
}
