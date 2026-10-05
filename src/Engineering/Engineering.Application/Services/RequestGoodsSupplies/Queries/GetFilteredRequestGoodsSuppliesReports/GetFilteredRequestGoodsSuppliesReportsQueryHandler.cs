using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFilteredRequestGoodsSuppliesReports;

public class GetFilteredRequestGoodsSuppliesReportsQueryHandler : IQueryHandler<GetFilteredRequestGoodsSuppliesReportsQuery, DataResult<List<RequestGoodsSupply>>>
{
    private readonly ILogger<GetFilteredRequestGoodsSuppliesReportsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;

    public GetFilteredRequestGoodsSuppliesReportsQueryHandler(ILogger<GetFilteredRequestGoodsSuppliesReportsQueryHandler> logger, IRequestGoodsSupplyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupply>>?>> Handle(GetFilteredRequestGoodsSuppliesReportsQuery request, CT ct)
    {
        try
        {
            var requestGoods = await _repository.GetFilteredRequestGoodsSuppliesReports(request.Ids, request.DetailIds, request.CostCenterId, request.ProjectId, request.ProjectOperationId, request.ProjectOperationDetailId,
                request.Statuses, request.ProductIds, request.Type, request.ProductType, request.ProductGroupId, request.FromDate, request.ToDate, request.CreatorId, request.CompanyId, request.FilterData,
                request.OrderBy, request.PageIndex, request.PageSize, ct);

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
