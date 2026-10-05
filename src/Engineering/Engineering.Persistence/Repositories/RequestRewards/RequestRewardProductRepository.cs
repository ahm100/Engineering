using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Repositories.RequestRewards;

public class RequestRewardProductRepository : BaseRepository<EngineeringDBContext, RequestRewardProduct>, IRequestRewardProductRepository
{
    public RequestRewardProductRepository(EngineeringDBContext context) : base(context)
    {
    }
}
