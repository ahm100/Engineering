using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Domain.Entities.RequestContractors;

namespace Engineering.Persistence.Repositories.RequestContractors;

public class RequestContractorHistoryRepository : BaseRepository<EngineeringDBContext, RequestContractorHistory>, IRequestContractorHistoryRepository
{
    public RequestContractorHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }
}
