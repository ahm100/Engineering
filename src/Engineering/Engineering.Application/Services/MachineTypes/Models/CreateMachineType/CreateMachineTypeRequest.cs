namespace Engineering.Application.Services.MachineTypes.Models.CreateMachineType;

public record CreateMachineTypeRequest(
    string MachineTypeCode,
    string MachineTypeTitle,
    int FromWeight,
    int UntilWeight,
    int CabinTypeCode,
    bool IsActive
     ) : IHttpRequest;