using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public class DailyProjectOperationProductRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationProduct>, IDailyProjectOperationProductRepository
{
    public DailyProjectOperationProductRepository(EngineeringDBContext context) : base(context)
    {
    }
}
