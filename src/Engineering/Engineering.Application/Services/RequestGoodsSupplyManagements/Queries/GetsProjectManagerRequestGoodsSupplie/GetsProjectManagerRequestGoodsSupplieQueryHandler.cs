using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetsProjectManagerRequestGoodsSupplie;

public class GetsProjectManagerRequestGoodsSupplieQueryHandler : IQueryHandler<GetsProjectManagerRequestGoodsSupplieQuery, DataResult<List<RequestGoodsSupply>>>
{
    private readonly ILogger<GetsProjectManagerRequestGoodsSupplieQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetsProjectManagerRequestGoodsSupplieQueryHandler(ILogger<GetsProjectManagerRequestGoodsSupplieQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupply>>?>> Handle(GetsProjectManagerRequestGoodsSupplieQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredRequestGoodsSupplies(
                request.Ids, request.CostCenterIds, request.ProjectIds, request.ProjectManagerId,
                request.CreatorIds, request.ProductIds, request.FromDate, request.ToDate,
                request.Statuses, request.RemoveStatuses, request.Types, request.FilterData,
                request.companyId, request.CustomerInvoiceNumber, request.OrderBy, request.PageIndex,
                request.PageSize, ct);

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
