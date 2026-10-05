using Engineering.Application.Services.MachineTypes.Models.CreateMachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.CreateMachineType;

public record CreateMachineTypeCommand(
    string MachineTypeTitle,
    string MachineTypeCode,
    int FromWeight,
    int UntilWeight,
    long CabinTypeId,
    bool IsActive,
    long? CompanyId
    ) : ICommand<CreateMachineTypeResponse>;