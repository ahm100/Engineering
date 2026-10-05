using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestProjectOperationDetail = Engineering.Domain.Entities.Transportations.TransportationRequestProjectOperationDetail;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestProjectOperationDetailRepository : BaseRepository<EngineeringDBContext, TransportationRequestProjectOperationDetail>, ITransportationRequestProjectOperationDetailRepository
{
    public TransportationRequestProjectOperationDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

}