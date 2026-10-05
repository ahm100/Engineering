using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public partial class RequestGoodsSupplyDetailRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyDetail>, IRequestGoodsSupplyDetailRepository
{

}
