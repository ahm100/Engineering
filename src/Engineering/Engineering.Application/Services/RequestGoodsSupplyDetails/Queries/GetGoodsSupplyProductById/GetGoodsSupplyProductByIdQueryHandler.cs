using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetGoodsSupplyProductById;

public class GetGoodsSupplyProductByIdQueryHandler : IQueryHandler<GetGoodsSupplyProductByIdQuery, GetGoodsSupplyProductByIdResponse>
{
    private readonly ILogger<GetGoodsSupplyProductByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyProductRepository _repository;

    public GetGoodsSupplyProductByIdQueryHandler(ILogger<GetGoodsSupplyProductByIdQueryHandler> logger, IRequestGoodsSupplyProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetGoodsSupplyProductByIdResponse?>> Handle(GetGoodsSupplyProductByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetGoodsSupplyProductById(request.Id, ct);

            return response ?? Result.Failure<GetGoodsSupplyProductByIdResponse>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetGoodsSupplyProductByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
