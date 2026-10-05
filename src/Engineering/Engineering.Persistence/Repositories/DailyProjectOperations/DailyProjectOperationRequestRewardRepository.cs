using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public class DailyProjectOperationRequestRewardRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationRequestReward>, IDailyProjectOperationRequestRewardRepository
{
    public DailyProjectOperationRequestRewardRepository(EngineeringDBContext context) : base(context)
    {
    }
}