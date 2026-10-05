namespace Engineering.Application.Services.TransportationRequests.Models.Create;

public record CreateTransportationRequestWarehouseModel(
    long WarehouseId,
    long? ThirdPartyId,
    string? ThirdPartyName,
    long PackingId,
    decimal Price,
    long ShippingCostId,
    List<CreateTransportationRequestWarehouseProductModel>? WarehouseProducts
     ) : IHttpRequest;

public record CreateTransportationRequestWarehouseProductModel(
    int Quantity,
    long ProductId,
    string? PalletNumber
     ) : IHttpRequest;
