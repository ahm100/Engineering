namespace Engineering.Application.Services.MachineTypes.Models.DisableMachineType;

public record DisableMachineTypeRequest(
    long Id
     ) : IHttpRequest;