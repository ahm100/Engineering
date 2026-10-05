using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyTypeDocumentRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyTypeDocument>, IRequestGoodsSupplyTypeDocumentRepository
{
    public RequestGoodsSupplyTypeDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}