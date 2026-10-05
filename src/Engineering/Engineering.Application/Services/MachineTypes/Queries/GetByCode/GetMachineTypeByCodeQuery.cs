using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetByCode;

public record GetMachineTypeByCodeQuery(
    string MachineTypeCode,
    long? CompanyId
    ) : IQuery<MachineType?>;