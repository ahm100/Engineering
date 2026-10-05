using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public class DailyProjectOperationExpertRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationExpert>, IDailyProjectOperationExpertRepository
{
    public DailyProjectOperationExpertRepository(EngineeringDBContext context) : base(context)
    {
    }
}
