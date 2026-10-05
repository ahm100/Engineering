using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoModelByIds;

public class GetsOperationInfoModelByIdsQueryHandler : IQueryHandler<GetsOperationInfoModelByIdsQuery, DataResult<List<GetOperationInfosModel>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetsOperationInfoModelByIdsQueryHandler> _logger;

    public GetsOperationInfoModelByIdsQueryHandler(ILogger<GetsOperationInfoModelByIdsQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetOperationInfosModel>>?>> Handle(GetsOperationInfoModelByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationInfosModelByIds(request.Ids, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetOperationInfosModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetOperationInfosModel>>>(OperationInfoErrors.FilteredOperationInfoNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetOperationInfosModel>>>(SharedErrors.UnknownError);
        }
    }
}