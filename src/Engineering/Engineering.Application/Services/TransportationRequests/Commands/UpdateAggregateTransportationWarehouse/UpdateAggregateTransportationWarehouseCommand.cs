using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAggregateTransportationWarehouse;

public record UpdateAggregateTransportationWarehouseCommand(
    TransportationRequest TransportationRequest,
    decimal? TransferPrice,
    string? Description,
    MachineType? MachineType,
    long? DriverId,
    string? NumberPlate,
    DateTime? PostageDate,
    long DetailId,
    string? GlobalFreightNumber,
    string? ClassifiedFreightNumber,
    decimal? Tax,
    decimal? DetailTransferPrice,
    decimal? ServicePrice,
    string? InsuranceNumber,
    decimal? InsurancePrice,
    decimal? ShippingCost,
    decimal? ProductTotalPrice,
    decimal? OutofRange,
    decimal? LoadWeight,
    string? OrderNumber,
    string? CertificateNumber,
    List<string>? Documents,
    List<UpdatePackingWarehousePrice>? WarehousePrices
    ) : ICommand<TransportationRequest>;

public record UpdatePackingWarehousePrice(
    long Id,
    decimal ShippingPrice
    );