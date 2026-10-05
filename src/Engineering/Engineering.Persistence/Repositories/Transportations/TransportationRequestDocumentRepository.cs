using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Repositories.Transportations;

public class TransportationRequestDocumentRepository : BaseRepository<EngineeringDBContext, TransportationRequestDocument>, ITransportationRequestDocumentRepository
{
    public TransportationRequestDocumentRepository(EngineeringDBContext context) : base(context)
    {
    }
}