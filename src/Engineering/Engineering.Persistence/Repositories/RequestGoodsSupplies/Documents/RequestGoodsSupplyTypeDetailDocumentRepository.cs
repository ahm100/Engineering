using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyTypeDetailDocumentRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyTypeDetailDocument>, IRequestGoodsSupplyTypeDetailDocumentRepository
{
    public RequestGoodsSupplyTypeDetailDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}