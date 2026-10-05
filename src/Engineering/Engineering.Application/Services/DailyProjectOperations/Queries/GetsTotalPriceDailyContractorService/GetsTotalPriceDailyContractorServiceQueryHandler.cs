using Engineering.Application.Abstractions.Data.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsTotalPriceDailyContractorService;

public class GetsTotalPriceDailyContractorServiceQueryHandler : IQueryHandler<GetsTotalPriceDailyContractorServiceQuery, decimal>
{
    private readonly ILogger<GetsTotalPriceDailyContractorServiceQueryHandler> _logger;
    private readonly IDailyProjectOperationServiceRepository _repository;

    public GetsTotalPriceDailyContractorServiceQueryHandler(
        ILogger<GetsTotalPriceDailyContractorServiceQueryHandler> logger,
        IDailyProjectOperationServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<decimal>> Handle(GetsTotalPriceDailyContractorServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsTotalPriceDailyContractorService(
                request.ProjectId,
                request.ContractorId,
                request.StartDate,
                request.EndDate,
                ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<decimal>(SharedErrors.UnknownError);
        }
    }
}
