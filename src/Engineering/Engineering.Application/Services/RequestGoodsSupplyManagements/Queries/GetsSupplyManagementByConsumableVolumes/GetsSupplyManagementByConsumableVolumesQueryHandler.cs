using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetsSupplyManagementByConsumableVolumes;

public class GetsSupplyManagementByConsumableVolumesQueryHandler : IQueryHandler<GetsSupplyManagementByConsumableVolumesQuery, List<RequestGoodsSupplyManagement>>
{
    private readonly IRequestGoodsSupplyManagementRepository _repository;
    private readonly ILogger<GetsSupplyManagementByConsumableVolumesQueryHandler> _logger;

    public GetsSupplyManagementByConsumableVolumesQueryHandler(IRequestGoodsSupplyManagementRepository repository, ILogger<GetsSupplyManagementByConsumableVolumesQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<RequestGoodsSupplyManagement>?>> Handle(GetsSupplyManagementByConsumableVolumesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsSupplyManagementByConsumableVolumes(request.ConsumableVolumeIds, ct);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<List<RequestGoodsSupplyManagement>>(SharedErrors.UnknownError);
        }
    }
}
