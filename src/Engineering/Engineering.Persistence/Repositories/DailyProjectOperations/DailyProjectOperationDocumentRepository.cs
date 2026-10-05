using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public class DailyProjectOperationDocumentRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationDocument>, IDailyProjectOperationDocumentRepository
{
    public DailyProjectOperationDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}
