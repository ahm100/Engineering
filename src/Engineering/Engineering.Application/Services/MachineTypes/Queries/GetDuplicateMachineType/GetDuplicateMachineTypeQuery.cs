using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetDuplicateMachineType;

public record GetDuplicateMachineTypeQuery(
    string MachineTypeTitle,
    int FromWeight,
    int UntilWeight,
    int CabinTypeCode,
    long? CompanyId
    ) : IQuery<MachineType?>;

