using Engineering.Application.Services.MachineTypes.Models.UpdateMachineType;
namespace Engineering.Application.Services.MachineTypes.Commands.UpdateMachineType;

public record UpdateMachineTypeCommand(
    long Id,
    string MachineTypeTitle,
    string MachineTypeCode,
    int FromWeight,
    int UntilWeight,
    long CabinTypeId,
    bool IsActive,
    long? CompanyId
    ) : ICommand<UpdateMachineTypeResponse>;
