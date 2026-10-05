namespace Engineering.Application.Services.TransportationRequests.Models.UpdateMachineDriver;

public record UpdateMachineDriverRequest(
    long Id,
    long MachineTypeId,
    long? DriverId,
    string? Driver,
    string NumberPlates,
    string? CertificateNumber
     ) : IHttpRequest;
