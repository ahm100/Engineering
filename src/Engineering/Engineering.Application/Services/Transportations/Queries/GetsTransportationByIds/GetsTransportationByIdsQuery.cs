using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetsTransportationByIds;

public record GetsTransportationByIdsQuery(
    List<long> Items
    ) : IQuery<List<Transportation>>;
