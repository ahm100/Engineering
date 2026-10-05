using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Repositories.RequestRewards;

public class RequestRewardHistoryRepository : BaseRepository<EngineeringDBContext, RequestRewardHistory>, IRequestRewardHistoryRepository
{
    public RequestRewardHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
}
