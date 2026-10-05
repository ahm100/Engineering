using Engineering.Application.Abstractions.Data.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetOperationContractors;

public class GetOperationContractorsQueryHandler : IQueryHandler<GetOperationContractorsQuery, DataResult<List<long>>>
{
    private readonly IRequestMachineryRepository _repository;
    private readonly ILogger<GetOperationContractorsQueryHandler> _logger;

    public GetOperationContractorsQueryHandler(ILogger<GetOperationContractorsQueryHandler> logger, IRequestMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetOperationContractorsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetOperationContractors(request.requestMachineryId, ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(RequestMachineryErrors.FilteredMachineryRequestNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
