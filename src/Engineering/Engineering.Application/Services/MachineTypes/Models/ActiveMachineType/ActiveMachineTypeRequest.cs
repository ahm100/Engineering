namespace Engineering.Application.Services.MachineTypes.Models.ActiveMachineType;

public record ActiveMachineTypeRequest(
    long Id
     ) : IHttpRequest;
