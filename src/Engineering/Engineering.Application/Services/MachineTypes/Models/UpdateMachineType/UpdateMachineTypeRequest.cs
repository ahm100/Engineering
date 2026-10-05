namespace Engineering.Application.Services.MachineTypes.Models.UpdateMachineType;

public record UpdateMachineTypeRequest(
    long Id,
    string MachineTypeTitle,
    string MachineTypeCode,
    int FromWeight,
    int UntilWeight,
    int CabinTypeCode,
    bool IsActive
     ) : IHttpRequest;

