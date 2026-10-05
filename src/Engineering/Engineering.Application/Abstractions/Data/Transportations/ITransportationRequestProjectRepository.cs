using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Abstractions.Data.Transportations;

public interface ITransportationRequestProjectRepository : IBaseRepository<TransportationRequestProject>
{
    Task<List<long>> GetByRequestId(long requestId,
        CT ct);

    Task<List<long>> GetByRequestIds(List<long> requestIds,
        CT ct);
}