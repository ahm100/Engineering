using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestProjectOperation = Engineering.Domain.Entities.Transportations.TransportationRequestProjectOperation;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestProjectOperationRepository : BaseRepository<EngineeringDBContext, TransportationRequestProjectOperation>, ITransportationRequestProjectOperationRepository
{
    public TransportationRequestProjectOperationRepository(EngineeringDBContext context) : base(context)
    {
    }

}