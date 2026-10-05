using Engineering.Application.Abstractions.Data.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetsFilteredRequestContractorContractor;

public class GetsFilteredRequestContractorContractorQueryHandler : IQueryHandler<GetsFilteredRequestContractorContractorQuery, DataResult<List<long>>>
{
    private readonly IRequestContractorRepository _repository;
    private readonly ILogger<GetsFilteredRequestContractorContractorQueryHandler> _logger;

    public GetsFilteredRequestContractorContractorQueryHandler(ILogger<GetsFilteredRequestContractorContractorQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsFilteredRequestContractorContractorQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredContractor(request.CostCenterIds, request.ProjectIds, ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(RequestContractorErrors.RequestContractorsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
