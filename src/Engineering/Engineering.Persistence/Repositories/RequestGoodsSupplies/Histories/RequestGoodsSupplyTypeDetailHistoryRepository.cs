using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Histories;
using Engineering.Domain.Entities.RequestGoodsSupplies.Histories;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Histories;

public class RequestGoodsSupplyTypeDetailHistoryRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyTypeDetailHistory>, IRequestGoodsSupplyTypeDetailHistoryRepository
{
    public RequestGoodsSupplyTypeDetailHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
}