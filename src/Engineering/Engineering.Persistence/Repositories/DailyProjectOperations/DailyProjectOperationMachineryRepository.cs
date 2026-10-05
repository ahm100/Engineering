using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public class DailyProjectOperationMachineryRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationMachinery>, IDailyProjectOperationMachineryRepository
{
    public DailyProjectOperationMachineryRepository(EngineeringDBContext context) : base(context)
    {
    }
}
