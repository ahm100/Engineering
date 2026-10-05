using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductById;

public class GetRequestGoodsSupplyProductByIdQueryHandler : IQueryHandler<GetRequestGoodsSupplyProductByIdQuery, RequestGoodsSupplyProduct>
{
    private readonly ILogger<GetRequestGoodsSupplyProductByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetRequestGoodsSupplyProductByIdQueryHandler(ILogger<GetRequestGoodsSupplyProductByIdQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<RequestGoodsSupplyProduct?>> Handle(GetRequestGoodsSupplyProductByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplyProductById(request.Id, ct);

            return response ?? Result.Failure<RequestGoodsSupplyProduct>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<RequestGoodsSupplyProduct>(SharedErrors.UnknownError);
        }
    }
}
