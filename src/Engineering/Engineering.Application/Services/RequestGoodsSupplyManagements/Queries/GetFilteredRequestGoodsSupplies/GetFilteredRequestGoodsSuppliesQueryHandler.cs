using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetFilteredRequestGoodsSupplies;

public class GetFilteredRequestGoodsSuppliesQueryHandler : IQueryHandler<GetFilteredRequestGoodsSuppliesQuery, DataResult<List<RequestGoodsSupply>>>
{
    private readonly ILogger<GetFilteredRequestGoodsSuppliesQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetFilteredRequestGoodsSuppliesQueryHandler(ILogger<GetFilteredRequestGoodsSuppliesQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupply>>?>> Handle(GetFilteredRequestGoodsSuppliesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredRequestGoodsSupplyManagement(
                request.Ids, request.CostCenterIds, request.ProjectIds, request.ProjectManagerId,
                request.CreatorIds, request.ProductIds, request.FromDate, request.ToDate,
                request.Statuses, request.RemoveStatuses, request.Types, request.RemoveTypes,
                request.FilterData, request.companyId, request.CustomerInvoiceNumber, request.OrderBy,
                request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ? new DataResult<List<RequestGoodsSupply>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            } : Result.Failure<DataResult<List<RequestGoodsSupply>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupply>>>(SharedErrors.UnknownError);
        }
    }
}
