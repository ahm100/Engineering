using Engineering.Domain.Entities.Transportations.Enums;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetByType;

public record GetTransportationByTypeQuery(
    TransportationType TransportationType
    ) : IQuery<Transportation?>;
