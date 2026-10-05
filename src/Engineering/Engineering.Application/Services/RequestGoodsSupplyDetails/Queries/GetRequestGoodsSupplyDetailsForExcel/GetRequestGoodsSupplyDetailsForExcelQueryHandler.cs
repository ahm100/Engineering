using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailsForExcel;

public class GetRequestGoodsSupplyDetailsForExcelQueryHandler : IQueryHandler<GetRequestGoodsSupplyDetailsForExcelQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetRequestGoodsSupplyDetailsForExcelQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetRequestGoodsSupplyDetailsForExcelQueryHandler(ILogger<GetRequestGoodsSupplyDetailsForExcelQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetRequestGoodsSupplyDetailsForExcelQuery request, CT ct)
    {
        try
        {
            var requestGoods = await _repository.GetRequestGoodsSupplyDetailsForExcel(request.Ids, request.CostCenterIds,
                request.ProjectIds, request.ProjectOperationIds, request.ProjectOperationDetailIds,
                request.CityId, request.ProjectManagerId, request.Statuses, request.RemoveStatuses,
                request.Types, request.CreatorIds, request.ProductIds, request.FromDate, request.ToDate,
                request.FilterData, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize,
                ct);

            var response = requestGoods.Data.Any()
                ? new DataResult<List<RequestGoodsSupplyDetail>>
                {
                    Data = requestGoods.Data,
                    RowCount = requestGoods.RowCount
                }
                : Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(RequestGoodsSupplyDetailErrors.RequestGoodsSupplyDetailProductsNotFound);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(SharedErrors.UnknownError);
        }
    }
}
