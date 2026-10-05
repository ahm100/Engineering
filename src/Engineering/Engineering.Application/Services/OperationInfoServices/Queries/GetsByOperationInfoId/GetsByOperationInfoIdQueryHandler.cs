using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsByOperationInfoId;

public class GetsByOperationInfoIdQueryHandler : IQueryHandler<GetsByOperationInfoIdQuery, DataResult<List<OperationInfoService>>>
{
    private readonly IOperationInfoServiceRepository _repository;
    private readonly ILogger<GetsByOperationInfoIdQueryHandler> _logger;

    public GetsByOperationInfoIdQueryHandler(ILogger<GetsByOperationInfoIdQueryHandler> logger, IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoService>>?>> Handle(GetsByOperationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByOprationInfoId(request.OprationInfoId, request.FilterData, request.PageIndex, request.PageSize, ct);

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