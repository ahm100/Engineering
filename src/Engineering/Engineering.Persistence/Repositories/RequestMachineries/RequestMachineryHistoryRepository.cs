using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryHistoryRepository : BaseRepository<EngineeringDBContext, RequestMachineryHistory>, IRequestMachineryHistoryRepository
{
    public RequestMachineryHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
}
