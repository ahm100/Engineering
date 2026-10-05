using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Abstractions.Data.Transportations;

public interface ITransportationRequestCostCenterRepository : IBaseRepository<TransportationRequestCostCenter>
{
    Task<List<long>> GetByRequestId(long requestId,
        CT ct);

    Task<List<long>> GetByRequestIds(List<long> requestId,
        CT ct);
}