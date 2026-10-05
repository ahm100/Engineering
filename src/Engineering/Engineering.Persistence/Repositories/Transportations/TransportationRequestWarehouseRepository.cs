using Engineering.Application.Abstractions.Data.Transportations;
using TransportationRequestWarehouse = Engineering.Domain.Entities.Transportations.TransportationRequestWarehouse;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestWarehouseRepository : BaseRepository<EngineeringDBContext, TransportationRequestWarehouse>, ITransportationRequestWarehouseRepository
{
    public TransportationRequestWarehouseRepository(EngineeringDBContext context) : base(context)
    {
    }
}