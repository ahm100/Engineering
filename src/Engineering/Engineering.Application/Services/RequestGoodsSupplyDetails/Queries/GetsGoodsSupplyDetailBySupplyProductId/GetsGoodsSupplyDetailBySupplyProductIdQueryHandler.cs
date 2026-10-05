using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductId;

public class GetsGoodsSupplyDetailBySupplyProductIdQueryHandler : IQueryHandler<GetsGoodsSupplyDetailBySupplyProductIdQuery, DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdModel>>>
{
    private readonly ILogger<GetsGoodsSupplyDetailBySupplyProductIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyDetailRepository _repository;

    public GetsGoodsSupplyDetailBySupplyProductIdQueryHandler(ILogger<GetsGoodsSupplyDetailBySupplyProductIdQueryHandler> logger, IRequestGoodsSupplyDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdModel>>?>> Handle(GetsGoodsSupplyDetailBySupplyProductIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsGoodsSupplyDetailBySupplyProductId(request.Id, ct);

            var response = new DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdModel>>()
            {
                Data = result ?? new List<GetsGoodsSupplyDetailBySupplyProductIdModel>(0)
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetsGoodsSupplyDetailBySupplyProductIdModel>>>(SharedErrors.UnknownError);
        }
    }
}
