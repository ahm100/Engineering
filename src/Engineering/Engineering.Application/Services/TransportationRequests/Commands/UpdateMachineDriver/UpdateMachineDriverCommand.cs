using Engineering.Domain.Entities.MachineTypes;

using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateMachineDriver;

public record UpdateMachineDriverCommand(
    TransportationRequest TransportationRequest,
    MachineType MachineType,
    long? DriverId,
    string? Driver,
    string? NumberPlate,
    string? CertificateNumber
    ) : ICommand<TransportationRequest>;