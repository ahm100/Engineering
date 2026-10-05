using Engineering.Domain.Entities.Logistics;
using Engineering.Domain.Entities.Synonyms.Warehouse.Packings;

namespace Engineering.Application.Services.TransportationRequests.Commands.Create;

public record UpdateTransportationWarehouseCommandModel(
    long Id,
    long WarehouseId,
    long? ThirdPartyId,
    string? ThirdPartyName,
    ViewPacking Packing,
    decimal Price,
    ShippingCost ShippingCost,
    List<CreateTransportationWarehouseProductCommandModel>? WarehouseProduct,
    List<UpdateTransportationWarehouseProductCommandModel>? UpdateWarehouseProduct,
    List<long>? DeleteWarehouseProduct
    );

public record UpdateTransportationWarehouseProductCommandModel(
    long Id,
    int Quantity,
    long ProductId,
    string? PalletNumber
    );
