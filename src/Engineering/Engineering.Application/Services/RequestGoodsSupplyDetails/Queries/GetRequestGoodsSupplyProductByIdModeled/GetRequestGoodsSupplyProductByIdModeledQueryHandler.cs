using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyProductByIdModeled;

public class GetRequestGoodsSupplyProductByIdModeledQueryHandler : IQueryHandler<GetRequestGoodsSupplyProductByIdModeledQuery, GetRequestGoodsSupplyProductByIdResponse>
{
    private readonly ILogger<GetRequestGoodsSupplyProductByIdModeledQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetRequestGoodsSupplyProductByIdModeledQueryHandler(ILogger<GetRequestGoodsSupplyProductByIdModeledQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetRequestGoodsSupplyProductByIdResponse?>> Handle(GetRequestGoodsSupplyProductByIdModeledQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRequestGoodsSupplyProductByIdModeled(request.Id, ct);

            return response ?? Result.Failure<GetRequestGoodsSupplyProductByIdResponse>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetRequestGoodsSupplyProductByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
