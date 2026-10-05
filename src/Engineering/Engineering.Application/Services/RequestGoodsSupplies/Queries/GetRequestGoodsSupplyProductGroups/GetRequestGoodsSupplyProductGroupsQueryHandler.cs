using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRequestGoodsSupplyProductGroups;

public class GetRequestGoodsSupplyProductGroupsQueryHandler : IQueryHandler<GetRequestGoodsSupplyProductGroupsQuery, DataResult<List<RequestGoodsSupply>>>
{
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly ILogger<GetRequestGoodsSupplyProductGroupsQueryHandler> _logger;

    public GetRequestGoodsSupplyProductGroupsQueryHandler(ILogger<GetRequestGoodsSupplyProductGroupsQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupply>>?>> Handle(GetRequestGoodsSupplyProductGroupsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredProductGroup(
                request.CostCenterId,
                request.ProjectId,
                request.ProjectOperationId,
                request.ProjectOperationDetailId,
                request.ProductType,
                ct);

            return items.Data.Any() ?
               new DataResult<List<RequestGoodsSupply>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<RequestGoodsSupply>>>(RequestGoodsSupplyErrors.RequestGoodsProductGroupNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupply>>>(SharedErrors.UnknownError);
        }
    }
}
