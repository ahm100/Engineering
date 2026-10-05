using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Persistence.Repositories.RequestRewards;

public class RequestRewardThirdPartyRepository : BaseRepository<EngineeringDBContext, RequestRewardThirdParty>, IRequestRewardThirdPartyRepository
{
    public RequestRewardThirdPartyRepository(EngineeringDBContext context) : base(context)
    {
    }
}
