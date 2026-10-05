using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFilteredRequestGoodsSupplies;

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
            var requestGoods = await _repository.GetFilteredRequestGoodsSupplies(request.Ids, request.DetailIds, request.ManagementIds,
                request.CostCenterIds, request.ProjectIds, request.CityId, request.ProjectManagerId, request.ProjectOperationIds,
                request.ProjectOperationDetailIds, request.FilterData, request.CreatorIds, request.ProductIds, request.Statuses,
                request.RemoveStatuses, request.Types, request.FromDate, request.ToDate, request.FilterDescription, request.FilterPublicName,
                request.FilterOperationInfoName, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            var response = requestGoods.Data.Any()
                ? new DataResult<List<RequestGoodsSupply>>
                {
                    Data = requestGoods.Data,
                    RowCount = requestGoods.RowCount
                }
                : Result.Failure<DataResult<List<RequestGoodsSupply>>>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupply>>>(SharedErrors.UnknownError);
        }
    }
}
