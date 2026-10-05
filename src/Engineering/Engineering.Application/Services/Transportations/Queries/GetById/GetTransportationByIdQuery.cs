using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetById;

public record GetTransportationByIdQuery(
    long Id
    ) : IQuery<Transportation?>;
