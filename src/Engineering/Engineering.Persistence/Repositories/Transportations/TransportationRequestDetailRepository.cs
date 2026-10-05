using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestDetail = Engineering.Domain.Entities.Transportations.TransportationRequestDetail;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestDetailRepository : BaseRepository<EngineeringDBContext, TransportationRequestDetail>, ITransportationRequestDetailRepository
{
    public TransportationRequestDetailRepository(EngineeringDBContext context) : base(context)
    {
    }
}