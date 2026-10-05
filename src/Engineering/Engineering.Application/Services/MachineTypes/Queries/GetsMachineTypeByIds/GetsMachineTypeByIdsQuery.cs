using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Queries.GetsMachineTypeByIds;

public record GetsMachineTypeByIdsQuery(
    List<long> Items
    ) : IQuery<List<MachineType>>;
