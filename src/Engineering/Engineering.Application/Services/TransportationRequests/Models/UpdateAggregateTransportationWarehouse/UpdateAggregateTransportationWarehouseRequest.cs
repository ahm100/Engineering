namespace Engineering.Application.Services.TransportationRequests.Models.UpdateAggregateTransportationWarehouse;

public record UpdateAggregateTransportationWarehouseRequest(
    long Id,
    decimal? TransferPrice,
    string? Description,
    long MachineTypeId,
    long DriverId,
    string? NumberPlate,
    string? CertificateNumber,
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
    string? OrderNumber,
    decimal? LoadWeight,
    List<string>? Documents
) : IHttpRequest;
