using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetByName;

public record GetTransportationByNameQuery(
    string TransportationName,
    long? CompanyId
    ) : IQuery<Transportation?>;
