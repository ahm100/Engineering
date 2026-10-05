using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestHistory = Engineering.Domain.Entities.Transportations.TransportationRequestHistory;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestHistoryRepository : BaseRepository<EngineeringDBContext, TransportationRequestHistory>, ITransportationRequestHistoryRepository
{
    public TransportationRequestHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

}
