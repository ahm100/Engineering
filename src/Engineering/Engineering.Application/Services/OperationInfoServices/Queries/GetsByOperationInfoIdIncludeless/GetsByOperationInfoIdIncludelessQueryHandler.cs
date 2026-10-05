using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoIdIncludeless;

public class GetsByOperationInfoIdIncludelessQueryHandler : IQueryHandler<GetsByOperationInfoIdIncludelessQuery, DataResult<List<OperationInfoService>>>
{
    private readonly IOperationInfoServiceRepository _repository;
    private readonly ILogger<GetsByOperationInfoIdIncludelessQueryHandler> _logger;

    public GetsByOperationInfoIdIncludelessQueryHandler(ILogger<GetsByOperationInfoIdIncludelessQueryHandler> logger, IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoService>>?>> Handle(GetsByOperationInfoIdIncludelessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByOperationInfoIdIncludeless(request.OprationInfoId, 0, 0, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoService>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoService>>>(OperationInfoServiceErrors.OperationInfoServiceWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoService>>>(SharedErrors.UnknownError);
        }
    }
}