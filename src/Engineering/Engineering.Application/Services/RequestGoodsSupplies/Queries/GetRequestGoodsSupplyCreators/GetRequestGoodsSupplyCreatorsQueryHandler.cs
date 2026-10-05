using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyCreators;

public class GetRequestGoodsSupplyCreatorsQueryHandler : IQueryHandler<GetRequestGoodsSupplyCreatorsQuery, DataResult<List<long>>>
{
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly ILogger<GetRequestGoodsSupplyCreatorsQueryHandler> _logger;

    public GetRequestGoodsSupplyCreatorsQueryHandler(ILogger<GetRequestGoodsSupplyCreatorsQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetRequestGoodsSupplyCreatorsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredCreators(
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.RequestGoodsSupplyIds, ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(RequestGoodsSupplyErrors.RequestGoodsCreatorNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
