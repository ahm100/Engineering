using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetByCode;

public record GetTransportationByCodeQuery(
    string TransportationCode,
    long? CompanyId
    ) : IQuery<Transportation?>;
