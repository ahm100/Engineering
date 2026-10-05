using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetNotWorkDocument = Engineering.Domain.Entities.FixAssetMachineries.FixAssetNotWorkDocument;

namespace Engineering.Persistence.Repositories.FixAssetMachineries;

public class FixAssetNotWorkDocumentRepository : BaseRepository<EngineeringDBContext, FixAssetNotWorkDocument>, IFixAssetNotWorkDocumentRepository
{
    public FixAssetNotWorkDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}