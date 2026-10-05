using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetSupplyProductByCommercialRequestId;

public class GetSupplyProductByCommercialRequestIdQueryHandler : IQueryHandler<GetSupplyProductByCommercialRequestIdQuery, RequestGoodsSupplyProduct>
{
    private readonly ILogger<GetSupplyProductByCommercialRequestIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetSupplyProductByCommercialRequestIdQueryHandler(ILogger<GetSupplyProductByCommercialRequestIdQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(GetSupplyProductByCommercialRequestIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetSupplyProductByCommercialCommercialRequestId(request.CommercialRequestId, ct);

            return response ?? Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }
}
