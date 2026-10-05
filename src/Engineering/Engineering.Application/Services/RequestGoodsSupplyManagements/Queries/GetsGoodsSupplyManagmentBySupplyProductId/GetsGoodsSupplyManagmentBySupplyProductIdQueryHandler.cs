using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsGoodsSupplyManagmentBySupplyProductId;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Queries.GetsGoodsSupplyManagmentBySupplyProductId;

public class GetsGoodsSupplyManagmentBySupplyProductIdQueryHandler : IQueryHandler<GetsGoodsSupplyManagmentBySupplyProductIdQuery, DataResult<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>>>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<GetsGoodsSupplyManagmentBySupplyProductIdQueryHandler> _logger;

    public GetsGoodsSupplyManagmentBySupplyProductIdQueryHandler(IRequestGoodsSupplyManagementRepository repository, ILogger<GetsGoodsSupplyManagmentBySupplyProductIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<DataResult<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>>?>> Handle(GetsGoodsSupplyManagmentBySupplyProductIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsGoodsSupplyManagmentBySupplyProductId(request.Id, ct);

            var response = new DataResult<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>>()
            {
                Data = result ?? new List<GetsGoodsSupplyManagmentBySupplyProductIdModel>(0)
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetsGoodsSupplyManagmentBySupplyProductIdModel>>>(SharedErrors.UnknownError);
        }
    }
}
