using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateWarehouseTransportation;

public record UpdateWarehouseTransportationCommand(
    TransportationRequest TransportationRequest,
    TransportationRequestDetail Detail,
    MachineType MachineType,
    decimal TransferPrice,
    long DriverId,
    string NumberPlate,
    string? CertificateNumber,
    string? FreightNumber,
    decimal? Volume,
    List<string>? DocumentUrls
    ) : ICommand<TransportationRequest>;