using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsCabinTypeByIds;

public record GetsCabinTypeByIdsQuery(
    List<long> Items)
    : IQuery<List<CabinType>>;