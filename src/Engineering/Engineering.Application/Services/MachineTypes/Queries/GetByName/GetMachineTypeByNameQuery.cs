using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetByName;

public record GetMachineTypeByNameQuery(
    string MachineTypeTitle,
    long? CompanyId
    ) : IQuery<MachineType?>;

