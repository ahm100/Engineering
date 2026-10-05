using Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies.Documents;

public class RequestGoodsSupplyDocumentRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyDocument>, IRequestGoodsSupplyDocumentRepository
{
    public RequestGoodsSupplyDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}