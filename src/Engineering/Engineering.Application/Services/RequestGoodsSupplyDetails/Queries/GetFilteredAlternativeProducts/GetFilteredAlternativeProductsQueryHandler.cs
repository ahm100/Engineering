using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetFilteredAlternativeProducts;

public class GetFilteredAlternativeProductsQueryHandler : IQueryHandler<GetFilteredAlternativeProductsQuery, DataResult<List<RequestGoodsSupplyDetail>>>
{
    private readonly ILogger<GetFilteredAlternativeProductsQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetFilteredAlternativeProductsQueryHandler(ILogger<GetFilteredAlternativeProductsQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<RequestGoodsSupplyDetail>>?>> Handle(GetFilteredAlternativeProductsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredAlternativeProducts(request.RequestIds, request.ProductIds, request.RequestGoodsSupplyDetailIds, ct);

            var response = new DataResult<List<RequestGoodsSupplyDetail>>()
            {
                Data = result.Data ?? new List<RequestGoodsSupplyDetail>(0),
                RowCount = result.RowCount
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<RequestGoodsSupplyDetail>>>(SharedErrors.UnknownError);
        }
    }
}
