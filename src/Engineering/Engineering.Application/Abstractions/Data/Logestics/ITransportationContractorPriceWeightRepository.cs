using Engineering.Domain.Entities.Logistics;

namespace Engineering.Application.Abstractions.Data;

public interface ITransportationContractorPriceWeightRepository : IBaseRepository<TransportationContractorPriceWeight>
{
    Task<List<TransportationContractorPriceWeight>?> GetPriceWeightByContractor(long id, CT ct);
}