using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetRequestGoodsSupplyManagementProducts;

public class GetRequestGoodsSupplyManagementProductsQueryHandler : IQueryHandler<GetRequestGoodsSupplyManagementProductsQuery, DataResult<List<RequestGoodsSupplyManagement>>>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<GetRequestGoodsSupplyManagementProductsQueryHandler> _logger;

    public GetRequestGoodsSupplyManagementProductsQueryHandler(IRequestGoodsSupplyManagementRepository repository, ILogger<GetRequestGoodsSupplyManagementProductsQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyManagement>>?>> Handle(GetRequestGoodsSupplyManagementProductsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByRequestGoodSupplyId(request.Id, request.Type, request.PageIndex, request.PageSize, ct);

            var response = result.Data.Any()
                ? new DataResult<List<RequestGoodsSupplyManagement>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<RequestGoodsSupplyManagement>>>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyManagement>>>(SharedErrors.UnknownError);
        }
    }
}
