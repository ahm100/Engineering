namespace Engineering.Application.Services.TransportationRequests.Models.Create;

public record UpdateTransportationRequestWarehouseModel(
    long Id,
    long WarehouseId,
    long? ThirdPartyId,
    string? ThirdPartyName,
    long PackingId,
    decimal Price,
    long ShippingCostId,
    List<CreateTransportationRequestWarehouseProductModel>? WarehouseProducts,
    List<UpdateTransportationRequestWarehouseProductModel>? UpdateWarehouseProducts,
    List<long>? DeleteWarehouseProducts
     ) : IHttpRequest;

public record UpdateTransportationRequestWarehouseProductModel(
    long Id,
    int Quantity,
    long ProductId,
    string? PalletNumber
     ) : IHttpRequest;
