using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryDocument = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryDocument;

namespace Engineering.Persistence.Repositories.FixAssetMachineries;

public class FixAssetMachineryDocumentRepository : BaseRepository<EngineeringDBContext, FixAssetMachineryDocument>, IFixAssetMachineryDocumentRepository
{
    public FixAssetMachineryDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}