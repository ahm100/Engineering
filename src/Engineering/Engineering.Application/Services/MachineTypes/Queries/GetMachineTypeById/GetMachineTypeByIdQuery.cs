using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetMachineTypeById;

public record GetMachineTypeByIdQuery(
    long Id
    ) : IQuery<MachineType>;