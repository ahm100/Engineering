using Engineering.Application.Abstractions.Data.OperationInfos;

namespace Engineering.Application.Services.OperationInfos.Queries.GetOperationInfoContractors;

public class GetOperationInfoContractorsQueryHandler : IQueryHandler<GetOperationInfoContractorsQuery, DataResult<List<long>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetOperationInfoContractorsQueryHandler> _logger;

    public GetOperationInfoContractorsQueryHandler(ILogger<GetOperationInfoContractorsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetOperationInfoContractorsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfoContractors(ct);

            return result.Data.Any() ?
                new DataResult<List<long>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<long>>>(OperationInfoErrors.OperationInfoContractorsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
