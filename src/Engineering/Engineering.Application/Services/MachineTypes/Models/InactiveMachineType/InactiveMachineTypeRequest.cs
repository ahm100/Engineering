namespace Engineering.Application.Services.MachineTypes.Models.InactiveMachineType;

public record InactiveMachineTypeRequest(
    long Id
     ) : IHttpRequest;
